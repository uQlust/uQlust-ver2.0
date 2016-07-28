using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using uQlustCore;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using uQlustCore.Interface;
using System.Data;
using uQlustCore.Distance;

namespace uQlustCore
{
    class HashClusterDendrog:HashCluster,IProgressBar
    {
         DistanceMeasures dMeasure;
         DistanceMeasure dist=null;
         AglomerativeType linkageType;
         PDB.PDBMODE atoms;
         bool jury1d;
         string profileName;
         string refJuryProfile;
         string dirName;
         hierarchicalCluster hk = null;
         public HashClusterDendrog(DCDFile dcd, HashCInput input,DistanceMeasures dMeasure, AglomerativeType linkageType,PDB.PDBMODE atoms,bool jury1d,string alignFileName,
                                              string profileName=null,string refJuryProfile=null):base(dcd,input)
        {
            this.dMeasure=dMeasure;
            this.linkageType=linkageType;
            this.atoms = atoms;
            this.jury1d = jury1d;
            this.profileName = profileName;
            this.refJuryProfile = refJuryProfile;

        }
         public HashClusterDendrog(string dirName, string alignFile, HashCInput input, DistanceMeasures dMeasure, AglomerativeType linkageType, PDB.PDBMODE atoms, bool jury1d, string alignFileName,
                                              string profileName = null, string refJuryProfile = null):base(dirName,alignFile,input)
        {
            this.dMeasure=dMeasure;
            this.linkageType=linkageType;
            this.atoms = atoms;
            this.jury1d = jury1d;
            this.profileName = profileName;
            this.refJuryProfile = refJuryProfile;
            this.dirName = dirName;
        }
        public void InitHashClusterDendrog()
         {
             base.InitHashCluster();
         }
        public new double ProgressUpdate()
        {
            double progress = 0;

            progress = 0.5*base.ProgressUpdate();

            if (hk != null)
                progress += 0.5 * hk.ProgressUpdate() ;

            return progress;
        }
        public new Exception GetException()
        {
            return null;
        }
        public new List<KeyValuePair<string, DataTable>> GetResults()
        {
            return null;
        }

        public string UsedMeasure()
         {
             if (dist != null)
                 return dist.ToString();

             return "NONE";
         }
         public override string ToString()
         {        
             return "HashDendrog";
         }
         public ClusterOutput RunHashDendrogCombine()
         {
             ClusterOutput output = DendrogUsingMeasures(stateAlignKeys);
             return output;
         }

         public ClusterOutput DendrogUsingMeasures(List<string> structures)
         {
             jury1D juryLocal = new jury1D();
             juryLocal.PrepareJury(al);
             ClusterOutput outC = null;
             Dictionary<string, List<int>> dic;
             //Console.WriteLine("Start after jury " + Process.GetCurrentProcess().PeakWorkingSet64);
             maxV = 2;
             currentV = 0;
             dic = PrepareKeys(structures,false);
             currentV++;
             int number = dic.Count;
             if (input.relClusters < 1000)
                 number = dic.Count - 50;
             
             //dic = HashEntropyCombine(dic, structures,input.reqClusters);
             //Console.WriteLine("Entropy ready after jury " + Process.GetCurrentProcess().PeakWorkingSet64);
             DebugClass.WriteMessage("Entropy ready");
             //Alternative way to start of UQclust Tree must be finished
             dic = FastCombineKeys(dic, structures, false);
             currentV = maxV;
             //Console.WriteLine("Combine ready after jury " + Process.GetCurrentProcess().PeakWorkingSet64);
             DebugClass.WriteMessage("Combine Keys ready");
             Dictionary<string, string> translateToCluster = new Dictionary<string, string>(dic.Count);
             List<string> structuresToDenrogram = new List<string>(dic.Count);
             List<string> structuresFullPath = new List<string>(dic.Count);
             DebugClass.WriteMessage("Number of clusters: "+dic.Count);
             int cc = 0;
             foreach (var item in dic)
             {
                 if (item.Value.Count > 2)
                 {
                     List<string> cluster = new List<string>(item.Value.Count);
                     foreach (var str in item.Value)
                         cluster.Add(structures[str]);


                     ClusterOutput output = juryLocal.JuryOptWeights(cluster);

                     structuresToDenrogram.Add(output.juryLike[0].Key);
                     if(alignFile==null)
                        structuresFullPath.Add(dirName + Path.DirectorySeparatorChar + output.juryLike[0].Key);
                     else
                         structuresFullPath.Add(output.juryLike[0].Key);
                     translateToCluster.Add(output.juryLike[0].Key, item.Key);
                 }
                 else
                 {
                     structuresToDenrogram.Add(structures[item.Value[0]]);
                     if(alignFile==null)
                        structuresFullPath.Add(dirName + Path.DirectorySeparatorChar + structures[item.Value[0]]);
                     else
                         structuresFullPath.Add(structures[item.Value[0]]);
                     translateToCluster.Add(structures[item.Value[0]], item.Key);
                 }
                 cc++;
             }
             DebugClass.WriteMessage("Jury finished");
             
             if (dMeasure == DistanceMeasures.HAMMING)
             {
                 if (refJuryProfile == null || !jury1d)
                     throw new Exception("Sorry but for jury measure you have to define 1djury profile to find reference structure");
                 else
                 {
                     dist = new JuryDistance(structuresFullPath, alignFile, true, profileName, refJuryProfile);
                 }
             }
             else
                 if (dMeasure == DistanceMeasures.RMSD)
                 {
                     dist = new Rmsd(structuresFullPath,"", jury1d, atoms, refJuryProfile);
                 }
                 else
                 {
                     dist = new MaxSub(structuresFullPath,"", jury1d, refJuryProfile);
                 }

            // return new ClusterOutput();
             DebugClass.WriteMessage("Start hierarchical");
             //Console.WriteLine("Start hierarchical " + Process.GetCurrentProcess().PeakWorkingSet64);
            
             hk = new hierarchicalCluster(dist, linkageType, dirName);
             dist.InitMeasure();
            
             //Now just add strctures to the leaves
             

             outC = hk.HierarchicalClustering(structuresToDenrogram);
             DebugClass.WriteMessage("Stop hierarchical");
             List<HClusterNode> hLeaves = outC.hNode.GetLeaves();

             foreach(var item in hLeaves)
             {
                 if (translateToCluster.ContainsKey(item.setStruct[0]))
                 {
                     foreach (var str in dic[translateToCluster[item.setStruct[0]]])
                         if (item.setStruct[0] != structures[str])
                            item.setStruct.Add(structures[str]);
                 }
                 else
                     throw new Exception("Cannot add structure. Something is wrong");
             }
             outC.hNode.RedoSetStructures();

             return outC;
         }
         

    }
}
