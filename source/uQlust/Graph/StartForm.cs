using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkFlows;
using uQlustCore.Profiles;
using uQlustCore;

namespace Graph
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            InternalProfilesManager.RemoveProfilesFile();
            List<InternalProfileBase> profiles=InternalProfilesManager.InitProfiles();
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.gFileName = "Settings.clu";
            Options.defaultFileName = "default.options";
            Rna_Protein_UserDef.results.Hide();
            AdvancedVersion adv = new AdvancedVersion(this);
            this.Hide();
            adv.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.gFileName = "EasySettings.clu";
            Options.defaultFileName = "EasyDefault.options";
            int num_proc;
            num_proc = System.Environment.ProcessorCount;

            Settings set = new Settings();
            set.Load();
            set.numberOfCores = num_proc;
            set.Save();

            Rna_Protein_UserDef rna_prot = new Rna_Protein_UserDef(this);
            this.Hide();
            rna_prot.Show();
            
        }
    }
}
