using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using uQlustCore;
using uQlustCore.Distance;
namespace Graph
{
  
    public delegate void UpdateList(Best5 p);
    public partial class Best5 : Form
    {
        public UpdateList upList = null;
        double avrSum = 0;
        double avrCount = 0;
    Dictionary<string, double> pl_smart = new Dictionary<string, double>()
    {
			{"1abv.pdb",12.31},{"1af7.pdb",4.42},{"1ah9.pdb",2.99},{"1aoy.pdb",4.42},{"1b4bA.pdb",5.43},{"1b72A.pdb",2.71},
            {"1bm8.pdb",8.15},{"1bq9A.pdb",5.73},{"1cewI.pdb",3.35},{"1cqkA.pdb",1.73},{"1csp.pdb",2.36},{"1cy5A.pdb",1.67},
            {"1dcjA.pdb",9.93},{"1di2A.pdb",2.41},{"1dtjA.pdb",1.91},{"1egxA.pdb",2.56},{"1fadA.pdb",3.62},{"1fo5A.pdb",3.84},
            {"1g1cA.pdb",2.60},{"1gjxA.pdb",7.59},{"1gnuA.pdb",7.11},{"1gpt.pdb",4.19},{"1gyvA.pdb",3.37},{"1hbkA.pdb",3.64},
            {"1itpA.pdb",7.65},{"1jnuA.pdb",2.61},{"1kjs.pdb",5.26},{"1kviA.pdb",2.05},{"1mkyA3.pdb",4.95},{"1mla.pdb",2.79},
            {"1mn8A.pdb",8.08},{"1n0uA4.pdb",4.26},{"1ne3A.pdb",3.96},{"1no5A.pdb",10.18},{"1npsA.pdb",2.23},{"1o2fB.pdb",5.90},
            {"1of9A.pdb",3.53},{"1ogwA.pdb",2.41},{"1orgA.pdb",2.50},{"1pgx.pdb",3.44},{"1r69.pdb",1.93},{"1sfp.pdb",5.03},
            {"1shfA.pdb",1.51},{"1sro.pdb",3.56},{"1ten.pdb",1.71},{"1tfi.pdb",4.96},{"1thx.pdb",2.13},{"1tif.pdb",7.18},
            {"1tig.pdb",4.13},{"1vcc.pdb",8.04},{"256bA.pdb",3.24},{"2a0b.pdb",2.23},{"2cr7A.pdb",4.45},{"2f3nA.pdb",1.90},
            {"2pcy.pdb",4.48},{"2reb.pdb",6.00}
    };
    Dictionary<string, double> pl = new Dictionary<string, double>()
    {
			{"1abv.pdb",11.05},{"1af7.pdb",4.42},{"1ah9.pdb",3.33},{"1aoy.pdb",4.17},{"1b4bA.pdb",5.32},{"1b72A.pdb",2.75},
            {"1bm8.pdb",6.99},{"1bq9A.pdb",6.69},{"1cewI.pdb",3.54},{"1cqkA.pdb",1.73},{"1csp.pdb",2.36},{"1cy5A.pdb",1.55},
            {"1dcjA.pdb",10.07},{"1di2A.pdb",2.43},{"1dtjA.pdb",1.91},{"1egxA.pdb",2.56},{"1fadA.pdb",3.53},{"1fo5A.pdb",3.70},
            {"1g1cA.pdb",2.65},{"1gjxA.pdb",7.59},{"1gnuA.pdb",8.29},{"1gpt.pdb",4.24},{"1gyvA.pdb",3.36},{"1hbkA.pdb",3.63},
            {"1itpA.pdb",7.95},{"1jnuA.pdb",2.72},{"1kjs.pdb",5.26},{"1kviA.pdb",2.05},{"1mkyA3.pdb",5.17},{"1mla.pdb",3.01},
            {"1mn8A.pdb",8.13},{"1n0uA4.pdb",4.26},{"1ne3A.pdb",5.07},{"1no5A.pdb",10.33},{"1npsA.pdb",2.06},{"1o2fB.pdb",5.72},
            {"1of9A.pdb",3.54},{"1ogwA.pdb",2.52},{"1orgA.pdb",2.52},{"1pgx.pdb",3.25},{"1r69.pdb",1.90},{"1sfp.pdb",5.23},
            {"1shfA.pdb",1.47},{"1sro.pdb",3.43},{"1ten.pdb",2.00},{"1tfi.pdb",4.57},{"1thx.pdb",2.32},{"1tif.pdb",7.18},
            {"1tig.pdb",4.13},{"1vcc.pdb",8.28},{"256bA.pdb",3.46},{"2a0b.pdb",2.23},{"2cr7A.pdb",2.82},{"2f3nA.pdb",1.82},
            {"2pcy.pdb",4.70},{"2reb.pdb",5.96}
    };
    Dictionary<string, double> calibur = new Dictionary<string, double>()
    {
			{"1abv.pdb",11.97},{"1af7.pdb",4.45},{"1ah9.pdb",2.85},{"1aoy.pdb",4.76},{"1b4bA.pdb",5.75},{"1b72A.pdb",3.10},
            {"1bm8.pdb",7.07},{"1bq9A.pdb",7.50},{"1cewI.pdb",3.68},{"1cqkA.pdb",1.68},{"1csp.pdb",2.38},{"1cy5A.pdb",1.62},
            {"1dcjA.pdb",9.96},{"1di2A.pdb",2.62},{"1dtjA.pdb",2.12},{"1egxA.pdb",2.60},{"1fadA.pdb",3.66},{"1fo5A.pdb",3.77},
            {"1g1cA.pdb",2.65},{"1gjxA.pdb",8.18},{"1gnuA.pdb",8.37},{"1gpt.pdb",4.64},{"1gyvA.pdb",3.44},{"1hbkA.pdb",3.48},
            {"1itpA.pdb",7.87},{"1jnuA.pdb",2.68},{"1kjs.pdb",5.89},{"1kviA.pdb",2.10},{"1mkyA3.pdb",5.33},{"1mla.pdb",2.82},
            {"1mn8A.pdb",7.08},{"1n0uA4.pdb",4.53},{"1ne3A.pdb",4.07},{"1no5A.pdb",10.70},{"1npsA.pdb",2.28},{"1o2fB.pdb",6.07},
            {"1of9A.pdb",3.61},{"1ogwA.pdb",1.28},{"1orgA.pdb",2.66},{"1pgx.pdb",3.01},{"1r69.pdb",1.91},{"1sfp.pdb",5.21},
            {"1shfA.pdb",1.46},{"1sro.pdb",3.54},{"1ten.pdb",1.84},{"1tfi.pdb",5.08},{"1thx.pdb",2.26},{"1tif.pdb",6.95},
            {"1tig.pdb",3.58},{"1vcc.pdb",6.43},{"256bA.pdb",2.98},{"2a0b.pdb",2.78},{"2cr7A.pdb",7.48},{"2f3nA.pdb",1.94},
            {"2pcy.pdb",4.57},{"2reb.pdb",5.90}
    };
public Best5()
        {
            InitializeComponent();
        }
        public Best5(List <ClusterOutput> outputs,bool flag=false)
        {
            InitializeComponent();
            if (outputs.Count == 0)
                return;
            this.Text = outputs[0].name.Split('_')[0];
            int countPP = 0, countMP = 0, countPPS = 0, countMPS = 0, countPC = 0, countMC = 0;
            if (outputs.Count > 0)
            {
                //dataGridView1.Rows.Add(outputs.Count+1);
                int i = 0;
                foreach (var item in outputs)
                {
                    StatClust cl = Calc(item,flag);
                    if (cl == null)
                        continue;
                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[i].Cells[0].Value = item.name;
                    dataGridView1.Rows[i].Cells[1].Value = item.dirName;
                
                    if (cl != null)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = cl.size;
                        dataGridView1.Rows[i].Cells[3].Value = cl.reference;
                        dataGridView1.Rows[i].Cells[4].Value = String.Format("{0:0.00}", cl.rmsd);
                        if (pl.ContainsKey(cl.native))
                        {
                            if (pl[cl.native] > cl.rmsd)
                            {
                                dataGridView1.Rows[i].Cells[5].Value = "+";
                                countPP++;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[5].Value = "-";
                                countMP++;
                            }
                            if (pl_smart[cl.native] > cl.rmsd)
                            {

                                dataGridView1.Rows[i].Cells[6].Value = "+";
                                countPPS++;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[6].Value = "-";
                                countMPS++;
                            }
                            if (calibur[cl.native] > cl.rmsd)
                            {

                                dataGridView1.Rows[i].Cells[7].Value = "+";
                                countPC++;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[7].Value = "-";
                                countMC++;
                            }

                        }
                        i++;
                    }
                }
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].Cells[5].Value = "Plus="+countPP+" Minus="+countMP;
                dataGridView1.Rows[i].Cells[6].Value = "Plus=" + countPPS + " Minus=" + countMPS;
                dataGridView1.Rows[i].Cells[7].Value = "Plus=" + countPC + " Minus=" + countMC;
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i+1].Cells[5].Value = "Avr=" + (avrSum/avrCount).ToString();
            }
        }
        public DataGridViewRow GetRow(int i)
        {
            return dataGridView1.Rows[i];
        }
        public DataGridViewRow GetRow(string dirName)
        {
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if(dataGridView1.Rows[i].Cells[1].Value!=null && ((string)dataGridView1.Rows[i].Cells[1].Value).Contains(dirName))
                    return dataGridView1.Rows[i];
            }
            return null;
        }

        public int GetRowsCounter()
        {
            return dataGridView1.Rows.Count;
        }


        private StatClust Calc(ClusterOutput outp,bool flag)
        {
            List <string> five=new List<string>();
             List<string> listFiles = new List<string>();
            //if (outp == null || outp.clusters == null)
            //    return null;

            if(outp==null || outp.dirName==null)
                return null;
            string []aux=outp.dirName.Split(Path.DirectorySeparatorChar);
            string native=aux[aux.Length-1]+".pdb";

            string dirM = "";
            for (int i = 0; i < aux.Length - 1; i++)
                dirM += aux[i] + Path.DirectorySeparatorChar;
            native = dirM + native;
            if(!File.Exists(native))
                return null;

            five.Add(native);
            jury1D jury;

            if(outp.clusters!=null)
            {
                
                outp.clusters.Sort((a,b)=>b.Count.CompareTo(a.Count));
                int end = outp.clusters.Count;
                if (outp.clusters.Count > 5)
                    end = 5;
                
               

                for (int i = 0; i < end; i++)
                {
                     listFiles.Clear();
                    foreach (var item in outp.clusters[i])
                    {
                        listFiles.Add(outp.dirName + Path.DirectorySeparatorChar+item);

                    }
                    
                    //
                    //
                    //Sift sift = new Sift(listFiles);
                    //ClusterOutput oo = sift.Shape();
                    if (flag)
                    {
                        DistanceMeasure distRmsd = new Rmsd(listFiles, "", false, uQlustCore.PDB.PDBMODE.ONLY_CA);
                        distRmsd.InitMeasure();
                        string strName = distRmsd.GetReferenceStructure(outp.clusters[i]);

                        five.Add(outp.dirName + Path.DirectorySeparatorChar + strName);
                    }
                    else
                    {
                        jury = new jury1D();
                        //jury.PrepareJury(listFiles, "", "Z:\\dcd\\SS3_SA9_jury_internal.profiles");
                        ClusterOutput oo = jury.JuryOptWeights(outp.clusters[i]);
                        if (oo == null)
                          continue;
                        five.Add(outp.dirName + Path.DirectorySeparatorChar + oo.juryLike[0].Key);

                    }
                    
                    //five.Add(outp.dirName + Path.DirectorySeparatorChar + outp.clusters[i][0]);
                }
                                                                               
            }
            if (outp.hNode != null)
            {
                int end;
                List<List<string>> cli=outp.hNode.GetClusters(10);
               
                cli.Sort((a, b) => b.Count.CompareTo(a.Count));

                outp.clusters = new List<List<string>>();
                end = cli.Count;
                if (cli.Count > 5)
                    end = 5;
                for (int i = 0; i < end; i++)
                {
                   // listFiles.Clear();
                    //foreach (var item in cli[i])
                    //{
                        //listFiles.Add(

                    //}

                    listFiles.Clear();
                    foreach (var item in cli[i])
                    {
                        listFiles.Add(outp.dirName + Path.DirectorySeparatorChar + item);

                    }

                    jury = new jury1D();
                    jury.PrepareJury(listFiles, "", "C:\\data\\dcd\\SS8_SA3_jury_internal.profiles");
                  
                    outp.clusters.Add(cli[0]);  //jury
                   
                    ClusterOutput oo = jury.JuryOptWeights(cli[i]);
                    if (oo == null)
                        continue;
                    five.Add(outp.dirName + Path.DirectorySeparatorChar + cli[i][0]);
                    //five.Add(outp.dirName + Path.DirectorySeparatorChar + strName);
                }

            }
            if (outp.juryLike != null)
            {
                int end = outp.juryLike.Count;
                if (outp.juryLike.Count > 5)
                    end = 5;

                for (int i = 0; i < end; i++)
                    five.Add(outp.dirName + Path.DirectorySeparatorChar + outp.juryLike[i].Key);                

            }
  //          DistanceMeasure dist;
 //           dist = new Rmsd(five, "", false, PDB.PDBMODE.ONLY_CA);
          //  dist = new MaxSub(five, "", false);

            Dictionary<string, double> cc = ReadScore(aux[aux.Length - 1]);


            if (cc == null)
                return null ;

           StatClust stCLust = new StatClust();
           string[] tt1 = native.Split(Path.DirectorySeparatorChar);
           stCLust.native = tt1[tt1.Length - 1];
           for (int i = 1; i < five.Count; i++)
           {
               
               string[] tt2 = five[i].Split(Path.DirectorySeparatorChar);
             //  double rmsd = dist.GetDistance(tt1[tt1.Length-1], tt2[tt2.Length-1]) /100.0;
               double rmsd=0;
               if (cc.ContainsKey(tt2[tt2.Length-1]))
                  rmsd = cc[tt2[tt2.Length - 1]];

               
               if (rmsd > stCLust.rmsd)
               {
                   if (outp.juryLike != null)
                       stCLust.size = outp.juryLike.Count;
                   else
                        stCLust.size = outp.clusters[i - 1].Count;
                   stCLust.rmsd = rmsd;
                   string[] dd = five[i].Split(Path.DirectorySeparatorChar);
                   stCLust.reference = dd[dd.Length-1];
               }

           }
           if (stCLust.rmsd < 1000)
           {
               avrSum += stCLust.rmsd;
               avrCount++;
           }
           if (outp.hNode != null)
           {
               outp.clusters.Clear();
               outp.clusters = null;
           }

           return stCLust;
        }

        private void Best5_Load(object sender, EventArgs e)
        {
            
        }
        private Dictionary<string, double> ReadScore(string target)
        {
            StreamReader rr=null;
            string name;
            Dictionary<string, double> gdt = new Dictionary<string, double>();
            name = target + ".SUMMARY.lga_sda.txt";
            name="Z:\\casp10_data\\"+name;
            if (!File.Exists(name))
            {
                name = target + "-D1.SUMMARY.lga_sda.txt";

                name = "Z:\\casp10_data\\" + name;
                if (!File.Exists(name))
                    return null;
            }
            rr = new StreamReader(name);
            string line = rr.ReadLine();
            line = rr.ReadLine();
            while (line != null)
            {
                line=line.Replace("  ", " ");
                line = line.Replace("  ", " ");
                line = line.Replace("  ", " ");
                string[] aux = line.Split(' ');
                double vv = Convert.ToDouble(aux[6]);
                aux = aux[0].Split('.');
                gdt.Add(aux[0], vv);
                line = rr.ReadLine();
            }

            rr.Close();

            return gdt;
        }
        private void Best5_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (upList != null)
                upList(this);
        }
            
    }
    class StatClust
    {
        public int size;
        public double rmsd;
        public string reference;
        public string native;

        public StatClust()
        {
            rmsd = double.MinValue;
        }
    };
}
