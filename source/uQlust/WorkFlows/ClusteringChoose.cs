using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using uQlustCore;

namespace WorkFlows
{

    public partial class ClusteringChoose : Form
    {
        static string initialPath = "workFlows" + Path.DirectorySeparatorChar + "userDefined" + Path.DirectorySeparatorChar;
        static Dictionary<INPUTMODE, Dictionary<string, string>> profiles = new Dictionary<INPUTMODE, Dictionary<string, string>>()
        {           
            {INPUTMODE.USER_DEFINED,new Dictionary <string,string>(){             
                                        {"Rpart",initialPath+"uQlust_config_file_Rpart.txt"},
                                        {"Hash",initialPath+"uQlust_config_file_Hash.txt"},
                                        {"1DJury",initialPath+"uQlust_config_file_1DJury.txt"},
                                        {"uQlustTree",initialPath+"uQlust_config_file_Tree.txt"}}}
        };
        public Settings set;
        ResultWindow results;// = new ResultWindow();
        bool previus = false;

        Form parent;
        public ClusteringChoose(Settings set, Form parent)
        {
            InitializeComponent();
            this.set = set;
            this.parent = parent;
            this.results = Rna_Protein_UserDef.results;
            switch (set.mode)
            {
                case INPUTMODE.PROTEIN:
                    this.Text = "Proteins clustering";
                    break;
                case INPUTMODE.RNA:
                    this.Text = "Rna clustering";
                    break;
                case INPUTMODE.USER_DEFINED:
                    this.Text = "User define profiles clustering";
                    break;
            }

        }

        void button4_Click(object sender, EventArgs e)
        {
            previus = true;
            parent.Show();
            this.Close();
        }
        string GetProcessName(object o)
        {
            return "WorkFlow_"+set.mode.ToString()+"_"+o.ToString();
        }
        void button1_Click(object sender, EventArgs e)
        {
            //RpartSimple rpart = new RpartSimple(this,set,results,profiles[set.mode]["Rpart"]);
            if (set.mode == INPUTMODE.USER_DEFINED)
            {
                RpartSimple rpart = new RpartSimple(this, set, results, profiles[set.mode]["Rpart"]);
                rpart.processName = GetProcessName(rpart);
                rpart.Show();
            }
            else
            {
                RpartSimple rpart = new RpartSimple(this, set, results);
                rpart.processName = GetProcessName(rpart);
                ProfileType type = new ProfileType(this, rpart);
                type.Show();
            }
            //rpart.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //HashSimple hash = new HashSimple(this,set,results,profiles[set.mode]["Hash"]);
            if (set.mode == INPUTMODE.USER_DEFINED)
            {
                HashSimple hash = new HashSimple(this, set, results, profiles[set.mode]["Hash"]);
                hash.processName = GetProcessName(hash);
                hash.Show();
            }
            else
            {
                HashSimple hash = new HashSimple(this, set, results);
                hash.processName = GetProcessName(hash);
                ProfileType type = new ProfileType(this, hash);
                type.Show();
            }
            //hash.Show();
            this.Hide();

        }

        private void ClusteringChoose_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!previus)
                parent.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //uQlustTreeSimple tree=new uQlustTreeSimple(this,set,results,profiles[set.mode]["uQlustTree"]);
            if (set.mode == INPUTMODE.USER_DEFINED)
            {
                uQlustTreeSimple tree = new uQlustTreeSimple(this, set, results, profiles[set.mode]["uQlustTree"]);
                tree.processName = GetProcessName(tree);
                tree.Show();
            }
            else
            {
                uQlustTreeSimple tree = new uQlustTreeSimple(this, set, results);
                tree.processName = GetProcessName(tree);
                ProfileType type = new ProfileType(this, tree);
                type.Show();
            }
            //tree.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Jury1DSimple jury = new Jury1DSimple();
            if (set.mode == INPUTMODE.USER_DEFINED)
            {
                Jury1DSimple hash =new Jury1DSimple(this, set, results, profiles[set.mode]["1DJury"]);
                hash.processName = GetProcessName(hash);
                hash.Show();
            }
            else
            {
                Jury1DSimple hash = new Jury1DSimple(this, set, results);
                hash.processName = GetProcessName(hash);
                ProfileType type = new ProfileType(this, hash);
                type.Show();
            }
            //hash.Show();
            this.Hide();

        }

    }
}
