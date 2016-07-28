using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Xml.Serialization;
using uQlustCore.PDB;

namespace uQlustCore
{
    public interface ICluster
    {
        ClusterOutput RunCluster();
    }
    public interface IVisCluster
    {
        void SClusters();
    }
    [Serializable]
    public class ClusterOutput
    {
        public List<KeyValuePair<string, double>> juryLike;
        public HClusterNode hNode;
        public List<List<string>> clusters;
        public string clusterType;
        public string measure;
        public string time;
        public string alignFile;
        public string dirName;
        public string name;
        public long peekMemory;
        public string loadableProfile = "";

        static public void Save(string fileName,Object o)
        {
            Stream stream = File.Open(fileName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream,o);
            stream.Close();
        }
        public void SaveTxt(string fileName)
        {
            StreamWriter stream = new StreamWriter(fileName);
            if (juryLike != null)
            {                
                foreach(var item in juryLike)                
                    stream.WriteLine(item.Key+" "+item.Value);                
            }
            if(clusters!=null)
            {
                int count=1;
                foreach(var item in clusters)
                {
                    stream.WriteLine("===========CLUSTER "+count++);
                    foreach (var it in item)
                        stream.WriteLine(it);
                    stream.WriteLine("=======================================");
                }
            }
            stream.Close();
        }
        static public ClusterOutput Load(string fileName)
        {
            ClusterOutput outObject;
            Stream stream = File.Open(fileName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            outObject=(ClusterOutput) bFormatter.Deserialize(stream);
            return outObject;
        }
        static public ClusterOutput LoadExternal(string fileName,string dirName)
        {
            ClusterOutput aux = new ClusterOutput() ;
            string line;
            StreamReader r = new StreamReader(fileName);
            aux.clusters = new List<List<string>>();
            aux.dirName = dirName;
            while (!r.EndOfStream)
            {
                line = r.ReadLine();
                if(!line.Contains("#") && line.Contains(":"))
                {
                    line = line.Replace("  ", "");
                    line = line.Replace("  ", "");
                    line=line.Replace(" : ", ":");
                    line=line.TrimEnd(' ');
                    string[] tmp = line.Split(' ');
                    List<string> auxList = new List<string>();
                 
                    string[] tmp2 = tmp[1].Split(':');
                    string[] loc = tmp2[1].Split('/');
                    
                    //aux.dirName = "/home/raad/work/casp10/" + loc[loc.Length - 2];
                    aux.name = loc[loc.Length - 2];
                    auxList.Add(loc[loc.Length - 1]+".pdb");
                    for (int i = 4; i < tmp.Length; i++)
                    {
                        loc = tmp[i].Split('/');
                        if (!auxList.Contains(loc[loc.Length - 1]))
                            auxList.Add(loc[loc.Length - 1]+".pdb");
                    }

                    aux.clusters.Add(auxList);
                }

            }
            return aux;
        }
        static public ClusterOutput LoadExternalPconsD(string fileName,string dirName)
        {
            ClusterOutput aux = new ClusterOutput();
            string[] tmp = null; ;
            string line;
            StreamReader r = new StreamReader(fileName);
            aux.clusters = new List<List<string>>();
            List<KeyValuePair<string,double>> auxList = new List<KeyValuePair<string,double>>();
            line = r.ReadLine();
            while (!r.EndOfStream)
            {
                
                tmp = line.Split(' ');
                if (tmp.Length>0)
                {
                    KeyValuePair<string, double> auxK = new KeyValuePair<string, double>(tmp[0], Convert.ToDouble(tmp[1]));
                    auxList.Add(auxK);
            
                }
                line = r.ReadLine();
            }
            aux.dirName = dirName;
            
            tmp =fileName.Split('\\');
            aux.name = Path.GetFileName(tmp[tmp.Length - 1]);
            aux.clusters = null;
            aux.juryLike = auxList;
            return aux;
        }
        static public ClusterOutput LoadExternalPleiades(string fileName,string dirName)
        {
            ClusterOutput aux = new ClusterOutput();
            string line;
            StreamReader r = new StreamReader(fileName);
            aux.clusters = new List<List<string>>();
            List<KeyValuePair<int, int>> clustSize = new List<KeyValuePair<int, int>>();
            aux.dirName = dirName;
            List<string> auxList = null;
            int clSize = 0,index=0;
            while (!r.EndOfStream)
            {
                line = r.ReadLine();
                if (line.Contains("Cluster"))
                {
                    if (auxList != null)
                    {
                        aux.clusters.Add(auxList);
                        clustSize.Add(new KeyValuePair<int,int>(clSize,index));
                        index++;
                    }

                    string[] aa = line.Split(',');
                    aa = aa[0].Split(':');
                    clSize = Convert.ToInt32(aa[1]);
                    auxList = new List<string>();
                }
                else
                    if (line.Contains("/") && !line.Contains("#")) 
                    {
                        line = line.TrimStart(' ');
                        line = line.Replace("\t", " ");
                        line = line.Replace("  ", " ");
                        line = line.Replace("  ", " ");
                        string[] tmp = line.Split(' ');

                        string[] loc = tmp[1].Split('/');
                        string[] xx = Path.GetFileName(fileName).Split('_');
                        //aux.dirName = Path.GetDirectoryName(fileName)+Path.DirectorySeparatorChar+xx[0];
                        //aux.dirName = "/home/raad/work/casp10/" + loc[loc.Length - 2];
                        aux.name = loc[loc.Length - 2];
                        auxList.Add(loc[loc.Length - 1]);
                    }

            }
            if (auxList.Count > 0)
            {
                aux.clusters.Add(auxList);
                clustSize.Add(new KeyValuePair<int, int>(clSize, index));
            }

            clustSize.Sort(delegate(KeyValuePair<int, int> first, KeyValuePair<int, int> second) { return second.Key.CompareTo(first.Key); });

            ClusterOutput final = new ClusterOutput();
            final.clusters = new List<List<string>>();
            final.name = aux.name;
            final.dirName = aux.dirName;
            foreach (var item in clustSize)
                final.clusters.Add(aux.clusters[item.Value]);

            return final;
        }


        public List<List<string>> GetClusters(int clustNum=0)
        {
            List<List<string>> aux=new List<List<string>>();
            if (clusters != null)
                return clusters;

            if (juryLike != null)
            {
                List<string> lista = new List<string>(juryLike.Count);
                foreach (var item in juryLike)
                    lista.Add(item.Key);
                aux.Add(lista);

                return aux;

            }
            if (hNode != null)
                return hNode.GetClusters(clustNum);


            return null;
        }

    }

    public enum INPUTMODE
    {
        PROTEIN,
        RNA
    };
    [Serializable]
    public class Settings
    {
        public string gFileName = "Settings.clu";
        public string dir = "";
      
        public int numberOfCores { get; set; }
        public string extension { get; set; }
        public string profilesDir { get; set; }
        public INPUTMODE mode { get; set; }
        public Settings()
        {
            mode = INPUTMODE.PROTEIN;
            numberOfCores = 1;
            extension = "*";
            dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string dirTmp = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "generatedProfiles";
            if (Directory.Exists(dirTmp))
                profilesDir = dirTmp;
            else
                profilesDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            if (dir != null)
            {
                gFileName = dir + Path.DirectorySeparatorChar + gFileName;
            }
        }
        public Settings(string fileName)
        {
            Load(gFileName);
        }

        public void Load()
        {
            Load(gFileName);
        }

        public void Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                mode = INPUTMODE.PROTEIN;
                numberOfCores = 1;
                extension = "*.pdb";
                return;
            }
                //throw new Exception("File "+fileName+" cannot be found");
            XmlSerializer ser = new XmlSerializer(typeof(Settings));
             StreamReader reader = new StreamReader(fileName);
            Settings localSettings = (Settings)ser.Deserialize(reader);                  
            profilesDir = localSettings.profilesDir;
            extension = localSettings.extension;
            numberOfCores = localSettings.numberOfCores;
            mode = localSettings.mode;
            reader.Close();
        }
        public void Save()
        {
            StreamWriter writer = new StreamWriter(gFileName);
            XmlSerializer ser = new XmlSerializer(this.GetType());
            ser.Serialize(writer, this);
            writer.Close();
        }

    }

}

