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
    public partial class Jury1DSimple : Form,IclusterType
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

        public Jury1DSimple()
        {
            InitializeComponent();
            
        }
        public INPUTMODE GetInputType()
        {
            return set.mode;
        }
        public override string ToString()
        {
            return "1DJury";
        }
        public void HideRmsdLike()
        {

        }
        public Jury1DSimple(Form parent, Settings set, ResultWindow results, string fileName = null)
        {
            InitializeComponent();
            dialog = folderBrowserDialog1;
            if (set.mode == INPUTMODE.USER_DEFINED)
                dialog = openFileDialog1;
            this.parent = parent;
            this.Location = parent.Location;
            if (fileName != null)
            {
                opt.ReadOptionFile(fileName);
                SetProfileOptions();
            }
            this.set = set;
            this.results = results;
            if (opt.other.juryProfile != null)
            {
                tree.LoadProfiles(opt.other.juryProfile);
                label9.Text = tree.GetStringActiveProfiles();
            }
        }
        void SetProfileOptions()
        {
            if (opt.dataDir.Count > 0)
                textBox1.Text = opt.dataDir[0];
            label3.Text = opt.other.juryProfile;
            opt.clusterAlgorithm.Clear();
            opt.clusterAlgorithm.Add(ClusterAlgorithm.Jury1D);
        }
        public void SetProfileName(string name)
        {
            opt.ReadOptionFile(name);
            SetProfileOptions();
            if (opt.other.juryProfile != null)
            {
                tree.LoadProfiles(opt.other.juryProfile);
                label9.Text = tree.GetStringActiveProfiles();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res=dialog.ShowDialog();

            if(res==DialogResult.OK)
            {
                if(dialog is FolderBrowserDialog)
                    textBox1.Text = ((FolderBrowserDialog) (dialog)).SelectedPath;
                else
                    textBox1.Text = ((OpenFileDialog)(dialog)).FileName;
            }

        }

        private void Jury1DSimple_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!previous)
                parent.Close();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            opt.dataDir.Clear();
            if (set.mode == INPUTMODE.USER_DEFINED)
                opt.profileFiles.Add(textBox1.Text);
            else
                opt.dataDir.Add(textBox1.Text);

            if (opt.hierarchical.distance == DistanceMeasures.HAMMING)
                opt.hierarchical.reference1DjuryH = true;
            else
                opt.hierarchical.reference1DjuryH = false;
            results.Show();
            results.Focus();
            results.BringToFront();
            set.Save();
            results.Run(processName + "_" + counter++, opt);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            previous = true;
            parent.Show();
            this.Close();

        }

    }
}
