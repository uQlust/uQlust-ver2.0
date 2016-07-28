using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using uQlustCore.Distance;
using uQlustCore;
using System.Threading;
using uQlustCore.Interface;

namespace Graph
{
    public struct ViewData
    {
        public string structures;
        public int size;
        public double distance;
    }
    public partial class ClusterStat : Form, IProgressBar
    {
        ClusterOutput output;
        string dirName;
        string alignFile;
        int maxV=1;
        int currentV=0;
        Exception exc = null;
        DataTable tableRes=null;
        DataTable tableDist = null;
        List<List<string>> selected=null;
        DistanceMeasure dist = null;
        string native = "";

        float bestStr = 0;
        Dictionary<string, float> distList = new Dictionary<string, float>();

        public ClusterStat()
        {
            InitializeComponent();
        }
        public double ProgressUpdate()
        {
            return (double)currentV / maxV;
        }
        public Exception GetException()
        {
            return exc;
        }
        public List<KeyValuePair<string, DataTable>> GetResults()
        {
            DataTable tab;

            tab = tableRes.Copy();
            for (int i = 0; i < tableRes.Columns.Count; i++)
                if (tableRes.Columns[i].Caption.Contains("Hidden"))
                    tab.Columns.Remove(tableRes.Columns[i].Caption);

            List<KeyValuePair<string, DataTable>> t = new List<KeyValuePair<string, DataTable>>();
            t.Add(new KeyValuePair<string,DataTable>("Clusters info",tab));
            if (tableDist != null)
                t.Add(new KeyValuePair<string,DataTable>("Distances to reference structure",tableDist));
             return t;
        }
        public ClusterStat(ClusterOutput output,string winName)
        {
            this.output = output;
            this.alignFile = output.alignFile;// output[0].loadableProfile;
            InitializeComponent();
            Settings set = new Settings();
            set.Load();
            if (set.mode == INPUTMODE.RNA)
                distanceControl1.HideAtoms = true;
            dataGridView2.Columns[1].ValueType=typeof(Double);
            this.Text = winName;
            if (output == null)
                return;


            if (output!=null && output.hNode != null)
                button2.Visible = true;
            else
            {
                button3.Enabled = true;
                if(output.clusters!=null)
                    selected = output.clusters;
                else
                    if (output.juryLike != null)
                    {
                        selected = new List<List<string>>();
                        List<string> n = new List<string>();
                        foreach (var item in output.juryLike)
                            n.Add(item.Key);
                        selected.Add(n);
                    }
            }
            string fileName = output.dirName + ".pdb";
            if (File.Exists(fileName))
                textBox1.Text = fileName;
            if (Directory.Exists(output.dirName))
            {
                dirName = output.dirName;
                textBox2.Text = dirName;
            }
            fileName=output.dirName+Path.DirectorySeparatorChar+Path.GetFileName(output.dirName);
            if (File.Exists(fileName))
                textBox3.Text = fileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res=openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)            
                textBox1.Text = openFileDialog1.FileName;            
        }
        private double CalcDevCluster(DistanceMeasure dd, List<string> cluster, string refStruct)
        {            
            double sum = 0;
            double v1 = 0, v2 = 0;

            if(dd==null)
                dd = PrepareDistanceMeasure(cluster, distanceControl1.distDef, dirName);

            foreach (var item in cluster)
            {
                int x = dd.GetDistance(refStruct, item) / 100;
                v1 += x;
                v2 += x * x;

            }
            v1 = v1 / cluster.Count;
            v1 *= v1;
            v2 = v2 / cluster.Count;
            sum = Math.Sqrt(v2 - v1);

            return sum;
        }
        private double CalcDev(List<KeyValuePair<string, double>> tab)
        {
            double sum = 0;
            double avr=0;
            for (int i = 0; i < tab.Count; i++)
                avr += tab[i].Value;
            avr /= tab.Count;

            for (int i = 0; i < tab.Count; i++)
            {
                sum += (avr - tab[i].Value) * (avr - tab[i].Value);
            }
            sum /= tab.Count;
            sum = Math.Sqrt(sum);

            return sum;

        }
/*        private Dictionary<string,double> SableDist(List<string> refStructures)
        {
            StreamReader sableFile = new StreamReader(textBox3.Text);
            string line = sableFile.ReadLine();
            string prefix=refStructures[0];
            protInfo pr_SS=new protInfo();
            protInfo pr_SA = new protInfo();
            string seq, ss, sa;
            if(prefix.Contains("\\"))
            {
                string []aux=prefix.Split('\\');
                prefix=aux[aux.Length-2];
            }
            while (line !=null)
            {               
                if(line.StartsWith("Query:"))
                {
                    string[] aux = line.Split(' ');
                    aux = aux[1].Split('.');
                    if (prefix.Contains(aux[0]))
                    {                        
                        pr_SS.sequence = sableFile.ReadLine();
                        string ww=sableFile.ReadLine();
                        pr_SS.profile = new List<byte>();
                        for (int i = 0; i < ww.Length;i++ )
                            pr_SS.profile.Add(ww[i].ToString());
                        line = sableFile.ReadLine();
                        pr_SA.sequence = pr_SS.sequence;
                        pr_SA.profile = new List<string>(Regex.Replace(sableFile.ReadLine(),@"\s+"," ").Trim().Split(' '));
                        for (int i = 0; i < pr_SA.profile.Count; i++)
                            pr_SA.profile[i] = (Convert.ToInt16(pr_SA.profile[i]) / 10).ToString();

                    }
                }
                line = sableFile.ReadLine();
            }

            sableFile.Close();
            Settings set=new Settings();
            set.Load();
            Alignment al=new Alignment(refStructures,set,"H:\\profiels\\SS3_SA9_internal.profiles");
            al.MyAlign(null);
            al.AddStructureToAlignment("sable_res","SS", ref pr_SS);
            al.AddStructureToAlignment("sable_res", "SA", ref pr_SA);

            Dictionary<string, Dictionary<string, protInfo>> allProf = new Dictionary<string, Dictionary<string, protInfo>>();
            allProf.Add("SS", new Dictionary<string, protInfo>());
            allProf["SS"].Add("sable_res", pr_SS);
            allProf.Add("SA", new Dictionary<string, protInfo>());
            allProf["SA"].Add("sable_res", pr_SA);
            Dictionary<string,List<string>> protCombineStates = al.CombineProfiles("sable_res", allProf);


            JuryDistance dist = new JuryDistance(al, false);

            Dictionary<string, double> distRes = new Dictionary<string, double>();

            foreach (var item in refStructures)
            {
                distRes.Add(Path.GetFileName(item), dist.GetDistance("sable_res",Path.GetFileName(item)));
                currentV++;
            }


            return distRes;

        }*/
        private DistanceMeasure PrepareDistanceMeasure(List <string> cluster,DistanceMeasures measure,string dirName)
        {
            DistanceMeasure distRes=null;
            List<string> clustFiles = new List<string>();
            foreach (var item in cluster)
                clustFiles.Add(dirName + Path.DirectorySeparatorChar + item);
            switch (measure)
            {
                case DistanceMeasures.HAMMING:
                    distRes = new JuryDistance(clustFiles, alignFile, true, distanceControl1.profileName);
                    break;
                case DistanceMeasures.MAXSUB:
                    {
                        distRes = new MaxSub(clustFiles, alignFile, distanceControl1.reference);
                    }
                    break;
                case DistanceMeasures.RMSD:
                    distRes = new Rmsd(clustFiles, alignFile, distanceControl1.reference, distanceControl1.CAtoms, distanceControl1.referenceProfile);
                    break;
            }
            distRes.InitMeasure();
            return distRes;
        }
        public void CalcStat()
        {
            
            List<string> fileNames = new List<string>();
            List<string> refStructures = new List<string>();
            DistanceMeasures measure = distanceControl1.distDef;
            DistanceMeasure distTemp = null;
            double distRes=0;
            maxV = selected.Count;

            try
            {

                tableRes = new DataTable();

                tableRes.Columns.Add("Cluster Size", typeof(int));
                tableRes.Columns.Add("Reference structure", typeof(string));
                tableRes.Columns.Add("Distance", typeof(double));
                tableRes.Columns.Add("Hidden1", typeof(int));
                tableRes.Columns.Add("Hidden2", typeof(string));


                if (checkBoxSable.Checked)
                {
                    tableRes.Columns.Add("Dist to Sable", typeof(double));
                    maxV *= 2;
                }
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < selected.Count; i++)
                        maxV += selected[i].Count;
                }
                fileNames.Add(textBox1.Text);

                jury1D jury = new jury1D();
                if (distanceControl1.reference)
                    jury.PrepareJury(dirName, alignFile, distanceControl1.referenceProfile);

                refStructures.Clear();
                selected.Sort(delegate(List<string> first, List<string> second)
                { return first.Count.CompareTo(second.Count); });

                selected.Reverse();
                //    dataGridView1.Rows.Add(selected.Count);

                for (int i = 0; i < selected.Count; i++)
                {
                    string refD = selected[i][0];
                    ClusterOutput juryO = null;
                    if (distanceControl1.reference)
                    {

                        if (selected[i].Count > 5)
                        {
                            juryO = jury.JuryOptWeights(selected[i]);
                            if (juryO == null)
                                continue;
                            refD = juryO.juryLike[0].Key;
                            if (!fileNames.Contains(dirName + Path.DirectorySeparatorChar + juryO.juryLike[0].Key))
                            {
                                fileNames.Add(dirName + Path.DirectorySeparatorChar + juryO.juryLike[0].Key);
                                refStructures.Add(dirName + Path.DirectorySeparatorChar + refD);
                            }
                        }
                        else
                            if (!fileNames.Contains(dirName + Path.DirectorySeparatorChar + selected[i][0]))
                            {
                                fileNames.Add(dirName + Path.DirectorySeparatorChar + selected[i][0]);
                                refStructures.Add(dirName + Path.DirectorySeparatorChar + selected[i][0]);
                            }

                    }
                    else
                    {

                        dist = PrepareDistanceMeasure(selected[i], measure, dirName);
                        distTemp = dist;
                        refD = dist.GetReferenceStructure(selected[i]);
                        fileNames.Add(dirName + Path.DirectorySeparatorChar + refD);
                        refStructures.Add(dirName + Path.DirectorySeparatorChar + refD);
                    }

                    if (fileNames.Count == 2)
                    {
                        switch (measure)
                        {
                            case DistanceMeasures.HAMMING:
                                dist = new JuryDistance(fileNames, alignFile, false, distanceControl1.profileName);
                                break;
                            case DistanceMeasures.MAXSUB:
                                dist = new MaxSub(fileNames, null, false);
                                break;
                            case DistanceMeasures.RMSD:
                                dist = new Rmsd(fileNames, null, false, distanceControl1.CAtoms);
                                break;

                        }
                        dist.InitMeasure();
                        distRes = Convert.ToDouble(String.Format("{0:0.00}", dist.GetDistance(native, refD) / 100.0));
                    }
                    fileNames.RemoveAt(fileNames.Count - 1);
                    if (checkBoxSable.Checked && refStructures.Count > 0)
                        tableRes.Rows.Add(selected[i].Count, refD, distRes, i, dirName, 0.0);
                    else
                        tableRes.Rows.Add(selected[i].Count, refD, distRes, i, dirName);
                    currentV++;
                }
                if (checkBoxSable.Checked && refStructures.Count > 0)
                {
                    Dictionary<string, double> res = null;// SableDist(refStructures);
                    for (int i = 0; i < refStructures.Count; i++)
                    {
                        if (res.ContainsKey(Path.GetFileName(refStructures[i])))
                        {
                            DataRow dr = tableDist.Rows[i];                            
                            dr[5] = Convert.ToDouble(String.Format("{0:0.00}", res[Path.GetFileName(refStructures[i])]));
                        }
                    }
                }
                if (checkBox1.Checked)
                {
                    CalculateDistToAll();
                }

            }
            catch(Exception ex)
            {
                exc = ex;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {           
            DistanceMeasures measure = distanceControl1.distDef;
            
            List<string> refStructures = new List<string>();
            dirName = textBox2.Text;

            try
            {
                    if (textBox1.Text != null && textBox1.Text.Length > 5 && File.Exists(textBox1.Text))
                    {
                        string[] tmp = textBox1.Text.Split(Path.DirectorySeparatorChar);
                        native = tmp[tmp.Length - 1];
                    }

                    /*List<string> full = new List<string>();
                    for (int i = 0; i < selected[0].Count; i++)
                        full.Add(selected[0][i]);
                    ClusterOutput juryFull = jury.JuryOptWeights(full);*/
                
                currentV = 0;
                ResultsForm fr = new ResultsForm();
                Progress pr = new Progress(this, fr);
                pr.Start();
                pr.Show();
                Thread startProg = new Thread(CalcStat);
                startProg.Start();

                
/*                if (checkBoxSable.Checked && refStructures.Count > 0)
                {
                   Dictionary<string,double> res= SableDist(refStructures);
                   for (int i = 0; i < refStructures.Count;i++ )
                   {
                       if (res.ContainsKey(Path.GetFileName(refStructures[i])))
                       {
                           dataGridView1.Columns[3].HeaderText = "Dist to sable";
                           dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(String.Format("{0:0.00}", res[Path.GetFileName(refStructures[i])]));
                       }
                   }
                }
                if (checkBox1.Checked)
                {
                    CalculateDistToAll();
                }
              
               
                label2.Enabled = true;
                comboBox1.Enabled = true;
                button4.Enabled = true;*/
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (output.hNode!=null)
            {
                visHierar formSelect = new visHierar(output.hNode, "Select clusters", output.measure);
                formSelect.ShowCloseButton();
                DialogResult res=formSelect.ShowDialog();

                if (res == DialogResult.OK)
                {
                    if (formSelect.listNodes != null)
                    {
                        if (selected == null)
                            selected = new List<List<string>>();
                        selected.Clear();
                        foreach(var item in formSelect.listNodes)
                            selected.Add(item.Key.setStruct);

                        button3.Enabled = true;
                    }
                }
            }
        }
        private void CalculateDistToAll()
        {
            tableDist = new DataTable();
            tableDist.Columns.Add("Reference structure", typeof(string));
            tableDist.Columns.Add("Distance", typeof(double));

            List <string> fileNames=new List<string>();
            if (textBox1.Text != null && textBox1.Text.Length > 5 && File.Exists(textBox1.Text))
            {
                fileNames.Add(textBox1.Text);
                string[] tmp = textBox1.Text.Split(Path.DirectorySeparatorChar);
                native = tmp[tmp.Length - 1];
            }

            bestStr = float.MaxValue;
            foreach (var item in selected)
            {
                foreach (var lItem in item)
                {
                    fileNames.Add(dirName+Path.DirectorySeparatorChar+lItem);
                    switch (distanceControl1.distDef)
                    {
                        case DistanceMeasures.HAMMING:
                            dist = new JuryDistance(fileNames, alignFile, false, distanceControl1.profileName);
                            break;
                        case DistanceMeasures.MAXSUB:
                            dist = new MaxSub(fileNames, alignFile, false);
                            break;
                        case DistanceMeasures.RMSD:
                            dist = new Rmsd(fileNames, alignFile, false, distanceControl1.CAtoms);                           
                            break;
                    }
                    dist.InitMeasure();
                    float distF = float.MaxValue;
                    if (distanceControl1.distDef != DistanceMeasures.HAMMING)
                    {
                        if (!dist.pdbs.molDic.ContainsKey(native) || !dist.pdbs.molDic.ContainsKey(lItem))
                            continue;

                        if (dist.pdbs.molDic[native].mol.Chains[0].chainSequence.Length > dist.pdbs.molDic[lItem].mol.Chains[0].chainSequence.Length)
                            distF = dist.GetDistance(native, lItem) / 100.0f;
                        else
                            distF = dist.GetDistance(lItem, native) / 100.0f;
                    }
                    else
                        distF=dist.GetDistance(native, lItem) / 100.0f;

                    tableDist.Rows.Add(lItem, Convert.ToDouble(String.Format("{0:0.00}", distF)));
                    distList.Add(lItem, distF);
                    if (bestStr > distF)
                        bestStr = distF;
                    fileNames.RemoveAt(fileNames.Count - 1);
                    currentV++;
                }
            }

        }
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
           
        }
        private void CalcDistToSabelPred()
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
                dirName = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }

        }

        private void checkBoxSable_CheckedChanged(object sender, EventArgs e)
        {
            button6.Enabled = checkBoxSable.Checked;
        }


    }
}
