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
    public partial class ProfileType : Form
    {
        static string initialPathProtein = "workFlows" + Path.DirectorySeparatorChar + "protein" + Path.DirectorySeparatorChar;
        static string initialPathProteinFrag = "workFlows" + Path.DirectorySeparatorChar + "proteinFragLib" + Path.DirectorySeparatorChar;
        static string initialPathRna = "workFlows" + Path.DirectorySeparatorChar + "rna" + Path.DirectorySeparatorChar;
        static string initialPathRnaFrag = "workFlows" + Path.DirectorySeparatorChar + "rnaFragLib" + Path.DirectorySeparatorChar;
        static Dictionary<INPUTMODE, Dictionary<string, Dictionary<string, string>>> profiles = new Dictionary<INPUTMODE, Dictionary<string, Dictionary<string, string>>>()
        {
            {INPUTMODE.PROTEIN,new Dictionary <string,Dictionary<string,string>>(){ 
                                        {"Equal",new Dictionary<string,string>(){{"Rpart",initialPathProtein+"uQlust_config_file_Rpart.txt"},
                                                                                 {"Hash",initialPathProtein+"uQlust_config_file_Hash.txt"},
                                                                                 {"1DJury",initialPathProtein+"uQlust_config_file_1DJury.txt"},
                                                                                 {"uQlustTree",initialPathProtein+"uQlust_config_file_Tree.txt"}}},
                                        {"UnEqual",new Dictionary<string,string>(){{"Rpart",initialPathProteinFrag+"uQlust_config_file_Rpart.txt"},
                                                                                 {"Hash",initialPathProteinFrag+"uQlust_config_file_Hash.txt"},
                                                                                 {"1DJury",initialPathProteinFrag+"uQlust_config_file_1DJury.txt"},
                                                                                 {"uQlustTree",initialPathProteinFrag+"uQlust_config_file_Tree.txt"}

                                        }}}},
            {INPUTMODE.RNA,new Dictionary <string,Dictionary<string,string>>(){ 
                                        {"Equal",new Dictionary<string,string>(){{"Rpart",initialPathRna+"uQlust_config_file_Rpart.txt"},
                                                                                 {"Hash",initialPathRna+"uQlust_config_file_Hash.txt"},
                                                                                 {"1DJury",initialPathRna+"uQlust_config_file_1DJury.txt"},
                                                                                 {"uQlustTree",initialPathRna+"uQlust_config_file_Tree.txt"}}},
                                        {"UnEqual",new Dictionary<string,string>(){{"Rpart",initialPathRnaFrag+"uQlust_config_file_Rpart.txt"},
                                                                                 {"Hash",initialPathRnaFrag+"uQlust_config_file_Hash.txt"},
                                                                                 {"1DJury",initialPathRnaFrag+"uQlust_config_file_1DJury.txt"},
                                                                                 {"uQlustTree",initialPathRnaFrag+"uQlust_config_file_Tree.txt"}

                                        }}}                                        
                                        
                                        
                                        }
        
        
        
        };                                    

        bool previous = false;
        Form parent;
        IclusterType clusterAlg;
        public ProfileType(Form parent, IclusterType clusterAlg)
        {
            InitializeComponent();
            this.parent = parent;
            this.clusterAlg = clusterAlg;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            previous = true;
            parent.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clusterAlg.SetProfileName(profiles[clusterAlg.GetInputType()]["Equal"][clusterAlg.ToString()]);
            clusterAlg.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clusterAlg.SetProfileName(profiles[clusterAlg.GetInputType()]["UnEqual"][clusterAlg.ToString()]);
            clusterAlg.HideRmsdLike();
            clusterAlg.Show();
            this.Hide();
        }

        private void ProfileType_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!previous)
                parent.Close();    
        }
    }
}
