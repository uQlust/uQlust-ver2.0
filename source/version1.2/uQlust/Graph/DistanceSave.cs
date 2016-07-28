using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using uQlustCore;
using uQlustCore.Distance;
using uQlustCore.Interface;
using System.Threading;

namespace Graph
{
    public partial class DistanceSave : Form, IProgressBar
    {
        string directory;
        string markStructure=null;
        string saveFile = "";
        Exception exc = null;
        int maxV = 1;
        int currentV = 0;
        public DistanceSave(string directory)
        {
            this.directory=directory;
            
            if (File.Exists(directory + ".pdb"))
                markStructure = directory + ".pdb";
            InitializeComponent();            
            textBox3.Text = directory;
            if (markStructure != null)
                textBox2.Text = markStructure;
            else
                textBox2.Text = "NONE";

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
            return null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = saveFileDialog1.ShowDialog();

            if(res==DialogResult.OK)
            {
                textBox1.Text = saveFileDialog1.FileName;
            }
        }
        private void SaveAll()
        {
            DistanceMeasure dist;
            Settings set = new Settings();
            set.Load();

            string[] files;

            if (set.extension.Length > 0)
                files = Directory.GetFiles(directory, set.extension);
            else
                files = Directory.GetFiles(directory);

            List<string> fileList = new List<string>(2);
            StreamWriter r = new StreamWriter(saveFile);
            maxV = files.Length;
            foreach (var item in files)
            {
                fileList.Clear();
                fileList.Add(item);
                fileList.Add(markStructure);
                if (distanceControl1.distDef == DistanceMeasures.HAMMING)
                {
                    dist = new JuryDistance(fileList, null, false, distanceControl1.profileName, distanceControl1.referenceProfile);
                }
                else
                    if (distanceControl1.distDef == DistanceMeasures.RMSD)
                    {
                        dist = new Rmsd(fileList, null, false, distanceControl1.CAtoms, null);
                    }
                    else
                    {
                        dist = new MaxSub(fileList, null, false, null);
                    }


                dist.InitMeasure();
                try
                {


                        int val = dist.GetDistance(Path.GetFileName(fileList[1]), Path.GetFileName(fileList[0]));
                        currentV++;
                        if (val < int.MaxValue)
                            r.WriteLine(fileList[0] + " " + (double)val / 100);
                        else
                            r.WriteLine(fileList[0] + " NaN");


                }
                catch (Exception ex)
                {
                    exc = ex;
                }
            }
            r.Close();

            currentV = maxV;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text.Length == 0)
            {
                MessageBox.Show("Name of the output file not provided");
                this.DialogResult = DialogResult.None;
                return;

            }
            if (!File.Exists(textBox2.Text))
            {
                MessageBox.Show("Reference structure not provided");
                this.DialogResult = DialogResult.None;
                return;

            }
            directory = textBox3.Text;
            markStructure = textBox2.Text;
            saveFile = textBox1.Text;
            if (markStructure != null)
            {
                if(!distanceControl1.CheckIntegrity())
                {
                    MessageBox.Show("Profile not defined");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if (textBox1.Text.Length > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    Progress pr = new Progress(this, null);
                    pr.Start();
                    pr.Show();
                    Thread startProg = new Thread(SaveAll);
                    startProg.Start();


                    this.Close();
                }
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
                markStructure = textBox2.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDialog1.ShowDialog();
                

            if (res == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
                
            }

        }
    }
}
