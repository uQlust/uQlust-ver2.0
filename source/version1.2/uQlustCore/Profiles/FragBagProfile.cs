using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using uQlustCore.PDB;
using uQlustCore;
using uQlustCore.Distance;
using System.Threading;
using System.Text.RegularExpressions;
namespace uQlustCore.Profiles
{
    public class FragBagProfile:ContactProfile
    {
        protected string fragBagProfile = "FragBag profile ";
        List<float [,]> fragBagLibrary=new List<float[,]>();
        float[] distData;
        int[] index;
        int[] zeroCount;
        List<Residue> res;
        int count = 0;

        ManualResetEvent[] resetEvents = null;
    public FragBagProfile()
        {
            dirSettings.Load();
            destination = new List<INPUTMODE>();
            destination.Add(INPUTMODE.PROTEIN);
            profileName = "FragBag";
            contactProfile = "FragBag profile ";
            AddInternalProfiles();
            ReadLibrary();
            maxV = 1;

        }
        public override void AddInternalProfiles()
        {
            profileNode node = new profileNode();

            node.profName = profileName;
            node.internalName = profileName;
            for (int i = 0; i < 10; i++)
                node.AddStateItem(i.ToString(), i.ToString());

            manager.AddNodeToList(node, typeof(FragBagProfile).FullName);

        }
        private void ThreadLibrary(object o)
        {
            int[] inx = (int [])o;

            float[] center = new float[3];
            float[,] chunk = null;


            for (int j = inx[0]; j < inx[1]; j++)
            {
                if (fragBagLibrary[j].GetLength(0) + inx[2] > res.Count)
                    continue;
                if (chunk == null || chunk.GetLength(0) != fragBagLibrary[j].GetLength(0))
                    chunk = new float[fragBagLibrary[j].GetLength(0), fragBagLibrary[j].GetLength(1)];

                for (int n = 0; n < fragBagLibrary[j].GetLength(0); n++)
                {
                    chunk[n, 0] = res[inx[2] + n].Atoms[0].Position.X;
                    chunk[n, 1] = res[inx[2] + n].Atoms[0].Position.Y;
                    chunk[n, 2] = res[inx[2] + n].Atoms[0].Position.Z;
                }
                Optimization.CenterMol(chunk, center);
                //float rmsd = Optimization.Rmsd(fragBagLibrary[j], chunk, false);
                distData[j] = Optimization.Rmsd(fragBagLibrary[j], chunk, false);
            }
            resetEvents[inx[3]].Set();
        }
        protected override void MakeProfiles(string strName, MolData molDic, StreamWriter wr)
        {
            int tNumb = dirSettings.numberOfCores;
            resetEvents = new ManualResetEvent[tNumb];
            if (molDic != null)
            {
                int[] profile = new int[distData.Length];
                res=molDic.mol.Chains[0].Residues;
                for(int i=0;i<res.Count;i++)
                {
                    for (int j = 0; j < fragBagLibrary.Count; j++)
                    {
                        distData[j] = float.MaxValue;
                        index[j] = j;
                    }

                    for (int n = 0; n < tNumb; n++)
                    {
                        int[] pp = new int[4];
                        pp[0] = (int)(n * fragBagLibrary.Count / Convert.ToDouble(tNumb));
                        pp[1] = (int)((n + 1) * fragBagLibrary.Count / Convert.ToDouble(tNumb));
                        pp[2]=i;
                        pp[3] = n;
                        resetEvents[n] = new ManualResetEvent(false);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadLibrary), (object)pp);

                    }
                    for (int n = 0; n < tNumb; n++)
                        resetEvents[n].WaitOne();


                    Array.Sort(distData, index);
                    profile[index[0]]++;
                }

                wr.WriteLine(">" + strName);
                wr.Write(fragBagProfile);
                foreach(var item in profile)
                    wr.Write(item+" ");
                wr.WriteLine();
                count++;
                for (int i = 0; i < profile.Length; i++)
                    if (profile[i] == 0)
                        zeroCount[i]++;
            }
        }
        public override void JoinFiles(string fileName)
        {
            StreamWriter wr;
            Dictionary<int, int>[] columns=null;
                if (File.Exists(GetProfileFileName(fileName)))
                    wr = File.AppendText(GetProfileFileName(fileName));
                else
                    wr = new StreamWriter(GetProfileFileName(fileName));

                if (wr == null)
                    throw new Exception("Cannot open file: " + GetProfileFileName(fileName));

                for (int i = 0; i < threadNumbers; i++)
                {
                    string fileN = GetProfileFileName(fileName) + "_" + i;
                    using (StreamReader rr = new StreamReader(fileN))
                    {
                        if (rr == null)
                            throw new Exception("Cannot open file: " + fileN);

                        string line = rr.ReadLine();
                        while (line != null)
                        {
                            if(!line.Contains(">"))
                            {
                                line=line.Trim();
                                line = Regex.Replace(line, @"\s+", " ");
                                string []aux=line.Split(' ');                             
                                if (columns == null)
                                {
                                    columns = new Dictionary<int, int>[aux.Length - 2];
                                    for (int n = 0; n < columns.Length; n++)
                                        columns[n] = new Dictionary<int, int>();
                                }

                                for (int n = 0; n < aux.Length-2; n++)                                    
                                    if (!columns[n].ContainsKey(Convert.ToInt32(aux[n + 2])))
                                        columns[n].Add(Convert.ToInt32(aux[n + 2]), 0);


                            }
                            line = rr.ReadLine();
                        }
                        rr.Close();
                    }
                }
                double[] devStd = new double[columns.Length];
                double[,] mean = new double[columns.Length,3];
                for (int n = 0; n < columns.Length;n++ )
                    if(columns[n].Keys.Count>10)
                    {
                        double sum = 0, sum2 = 0 ;
                        mean[n, 2] = double.MinValue;
                        mean[n, 1] = double.MaxValue;
                        foreach (var item in columns[n])
                        {
                            sum += item.Key;
                            sum2 += item.Key * item.Key;
                            if (item.Key > mean[n, 2])
                                mean[n, 2] = item.Key;
                            if (item.Key < mean[n, 1])
                                mean[n, 1] = item.Key;
                        }
                        mean[n,0]=sum/columns[n].Count;                        
                        devStd[n] = Math.Sqrt(sum2 / columns[n].Count - mean[n,0] * mean[n,0]);
                    }

                    for (int i = 0; i < threadNumbers; i++)
                    {
                        string fileN = GetProfileFileName(fileName) + "_" + i;
                        using (StreamReader rr = new StreamReader(fileN))
                        {
                            if (rr == null)
                                throw new Exception("Cannot open file: " + fileN);

                            string line = rr.ReadLine();
                            while (line != null)
                            {
                                if (!line.Contains(">"))
                                {
                                    line = line.Trim();
                                    line = Regex.Replace(line, @"\s+", " ");

                                    string[] aux = line.Split(' ');
                                    for (int n = 0; n < zeroCount.Length; n++)
                                    {
                                        if(devStd[n]>0)
                                        {
                                            bool flag = false;
                                            double step = (mean[n, 2] - mean[n, 1])/10;
                                            for(int m=1;m<=10;m++)
                                                if(Convert.ToInt32(aux[n+2])<(mean[n,1]+step*m) )
                                                {
                                                    aux[n + 2] = m.ToString();
                                                    flag = true;
                                                    break;
                                                }
                                            if (!flag)
                                                aux[n + 2] = "0";
                                        }
                                        if (zeroCount[n] == count)
                                            aux[n + 2] = " ";
                                    }

                                    line = String.Join(" ", aux);
                                    line = line.Trim();
                                    line = Regex.Replace(line, @"\s+", " ");

                                }
                                wr.WriteLine(line);
                                line = rr.ReadLine();
                            }
                            rr.Close();
                            File.Delete(fileN);
                        }
                    }
                wr.Close();

        }

    
        public void ReadLibrary()
        {
            string[] files;
            if(Directory.Exists("fragBag"))
                files = Directory.GetFiles("./fragBag");
            else
                if (Directory.Exists("C:\\Projects\\UQlast\\fragBag"))
                    files = Directory.GetFiles("C:\\Projects\\UQlast\\fragBag");
                else
                    throw new Exception("Directory frag bag not exists. Profile fragbag cannot be used!");

            
            foreach(var item in files)
            {
                if (!item.Contains("400_11"))
                    continue;
                using(StreamReader s = new StreamReader(item))
                {
                    string line = s.ReadLine();
                    float[] cent = new float[3];
                    while(line!=null)
                    {
                        if(line.Contains("----"))
                        {
                            line = s.ReadLine();
                            List<float[]> str = new List<float[]>();
                            while(line!=null && !line.Contains("***"))
                            {
                                line = line.Trim();
                                line = Regex.Replace(line, @"\s+", " ");
                                string[] aux = line.Split(' ');
                                float [] p = new float [3];
                                for(int i=0;i<3;i++)
                                    p[i]=(float)Convert.ToDouble(aux[i]);

                                str.Add(p);
                                line = s.ReadLine();
                            }
                            float[,] auxTab = new float[str.Count, 3];
                            for(int i=0;i<str.Count;i++)
                            {
                                for (int j = 0; j < str[i].Length; j++)
                                    auxTab[i, j] = str[i][j];
                            }
                            
                            Optimization.CenterMol(auxTab, cent);
                            fragBagLibrary.Add(auxTab);
                        }
                        line = s.ReadLine();
                    }
                }                
            }
            fragBagLibrary.Sort(
                delegate(float [,] p1, float [,]p2)
                {
                    return p1.GetLength(0).CompareTo(p2.GetLength(0));
            }
            );
            if (fragBagLibrary.Count == 0)
                throw new Exception("Frag Bag Library empty!");
            distData = new float[fragBagLibrary.Count];
            index = new int [fragBagLibrary.Count];
            zeroCount = new int [fragBagLibrary.Count];
        }
    }
}
