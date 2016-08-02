using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uQlustCore;

namespace WorkFlows
{
    public partial class uQlustTreeSimple : Form, IclusterType
    {
        protected Options opt = new Options();
        protected ResultWindow results;
        Settings set;
        public string processName;
        bool previous = false;
        static int counter = 0;
        CommonDialog dialog;
        Form parent;
        ProfileTree tree = new ProfileTree();

        public uQlustTreeSimple(Form parent, Settings set,ResultWindow results, string fileName = null)
        {
            InitializeComponent();
            distanceControl1.profileInfo = false;
            distanceControl1.hideReference = true;
            this.parent = parent;
            this.Location = parent.Location;
            dialog = folderBrowserDialog1;
            if (set.mode == INPUTMODE.USER_DEFINED)
            {
                dialog = openFileDialog1;
                label1.Text = "Choose user defined file with profiles";
            }
            this.set = set; 
           
            if (fileName != null)
            {
                opt.ReadOptionFile(fileName);
                SetProfileOptions();
            }
           
            this.results = results;
        }
        public void HideRmsdLike()
        {
            distanceControl1.HideRmsdLike=true;
            distanceControl1.HideCosine = false;
        }
        public override string ToString()
        {
            return "uQlustTree";
        }
        public INPUTMODE GetInputType()
        {
            return set.mode;
        }
        void SetProfileOptions()
        {
            if (set.mode == INPUTMODE.USER_DEFINED)
            {
                if (opt.profileFiles.Count > 0)
                    textBox1.Text = opt.profileFiles[0];
            }
            else
                if (opt.dataDir.Count > 0)
                    textBox1.Text = opt.dataDir[0];

            opt.clusterAlgorithm.Clear();
            opt.clusterAlgorithm.Add(ClusterAlgorithm.uQlustTree);
            label3.Text = opt.hash.profileName;
            relevantC.Value = opt.hash.relClusters;
            distanceControl1.distDef = opt.hierarchical.distance;
            distanceControl1.profileName = opt.hierarchical.hammingProfile;
            if (opt.hash.combine)
                radioButton1.Checked = true;
            else            
                Hash.Checked = true;
            
            switch (set.mode)
            {
                case INPUTMODE.USER_DEFINED:
                    distanceControl1.HideRmsdLike = true;
                    distanceControl1.distDef = DistanceMeasures.COSINE;
                    break;
                case INPUTMODE.PROTEIN:
                case INPUTMODE.RNA:
                    distanceControl1.HideCosine = true;
                    break;

            }
            if (opt.hash.profileName != null)
            {
                tree.LoadProfiles(opt.hash.profileName);
                label9.Text = tree.GetStringActiveProfiles();
            }
        }
        public void SetProfileName(string name)
        {
            opt.ReadOptionFile(name);
            SetProfileOptions();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            opt.dataDir.Clear();
            opt.profileFiles.Clear();
            if(set.mode==INPUTMODE.USER_DEFINED)
                opt.profileFiles.Add(textBox1.Text);
            else
                opt.dataDir.Add(textBox1.Text);

            opt.hash.relClusters = (int)relevantC.Value;
            opt.hash.perData = 90;
            if (radioButton1.Checked)
                opt.hash.combine = true;
            else
                opt.hash.combine = false;
            opt.hierarchical.distance = distanceControl1.distDef;
            if (opt.hierarchical.distance == DistanceMeasures.HAMMING || opt.hierarchical.distance==DistanceMeasures.COSINE)
                opt.hierarchical.reference1DjuryH = true;
            else
                opt.hierarchical.reference1DjuryH = false;
            results.Show();
            results.Focus();
            results.BringToFront();
            set.Save();
            
            results.Run(processName+"_"+counter++, opt);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            previous = true;
            parent.Show();
            this.Close();

        }

        private void uQlustTree_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!previous)
                parent.Close();     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = dialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (set.mode == INPUTMODE.USER_DEFINED)
                    textBox1.Text = ((OpenFileDialog)(dialog)).FileName;
                else
                    textBox1.Text = ((FolderBrowserDialog)(dialog)).SelectedPath;
            }
        }
    }
}
