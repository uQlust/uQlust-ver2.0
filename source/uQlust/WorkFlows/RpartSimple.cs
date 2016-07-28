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
    public partial class RpartSimple : Form, IclusterType
    {
        protected Options opt=new Options();
        protected ResultWindow results;
        public string processName;
        static int counter = 0;
        bool previous = false;
        CommonDialog dialog;
        Settings set;
        Form parent;
        ProfileTree tree = new ProfileTree();


        public RpartSimple()
        {
            InitializeComponent();
        }
        public INPUTMODE GetInputType()
        {
            return set.mode;
        }
        public override string ToString()
        {
            return "Rpart";
        }
        public void HideRmsdLike()
        {

        }
        public RpartSimple(Form parent, Settings set, ResultWindow results, string fileName = null)
        {
            InitializeComponent();
            this.parent = parent;
            dialog = folderBrowserDialog1;
            if (set.mode == INPUTMODE.USER_DEFINED)
                dialog = openFileDialog1;
            this.Location = parent.Location;
            if (fileName != null)
            {
                opt.ReadOptionFile(fileName);
                SetProfileOptions();
            }
            this.set = set;
            this.results = results;
            if (opt.hash.profileName!=null)
            {
                tree.LoadProfiles(opt.hash.profileName);
                label9.Text = tree.GetStringActiveProfiles();
            }
        }
        public void ShowLabels()
        {
            label4.Visible = true;
            label5.Visible = true;
        }
        void SetProfileOptions()
        {
            if (opt.dataDir.Count > 0)
                textBox1.Text = opt.dataDir[0];
            label3.Text = opt.hash.profileName;
            relevantC.Value = opt.hash.relClusters;
            percentData.Value = opt.hash.perData;
            opt.clusterAlgorithm.Clear();
            opt.clusterAlgorithm.Add(ClusterAlgorithm.HashCluster);        
        }
        public void SetProfileName(string name)
        {
            opt.ReadOptionFile(name);
            SetProfileOptions();
            if (opt.hash.profileName!=null)
            {
                tree.LoadProfiles(opt.hash.profileName);
                label9.Text = tree.GetStringActiveProfiles();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res=dialog.ShowDialog();

            if(res==DialogResult.OK)
            {
                if(set.mode==INPUTMODE.USER_DEFINED)
                    textBox1.Text = ((OpenFileDialog)(dialog)).FileName;
                else
                    textBox1.Text = ((FolderBrowserDialog)(dialog)).SelectedPath;
            }
        }
        void button2_Click(object sender, EventArgs e)
        {
            opt.dataDir.Clear();
            if (set.mode == INPUTMODE.USER_DEFINED)
                opt.profileFiles.Add(textBox1.Text);
            else
                opt.dataDir.Add(textBox1.Text);

            opt.hash.relClusters = (int)relevantC.Value;
            opt.hash.perData = (int)percentData.Value;
            set.Save();
            results.Show();
            results.Focus();
            results.BringToFront();
            results.Run(processName + "_" + counter++, opt);

        }

        void button4_Click(object sender, EventArgs e)
        {
            previous = true;
            parent.Show();
            this.Close();
        }        
        private void RpartSimple_FormClosed(object sender, FormClosedEventArgs e)
        {
                if(!previous)
                    parent.Close();           
        }

      
    }
}
