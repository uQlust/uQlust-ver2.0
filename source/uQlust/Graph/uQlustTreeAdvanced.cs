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
using uQlustCore.Profiles;


namespace Graph
{
    public partial class uQlustTreeAdvanced : Form
    {
        public Options localOpt;
        public HashCInput localHash;
        public ClusterAlgorithm alg;             
        string profileFile;
        public uQlustTreeAdvanced(Options obj, ClusterAlgorithm alg, INPUTMODE mode, bool flag, List<string> profiles)
        {
            InitializeComponent();
            if (profiles != null)
            {
                button3.Visible = true;
                profileFile = profiles[0];
            }
            else
                button3.Visible = false;

            Settings set = new Settings();
            set.Load();

            switch (set.mode)
            {
                case INPUTMODE.RNA:
                    distanceControl1.HideAtoms = true;
                    break;
                case INPUTMODE.USER_DEFINED:
                    distanceControl1.HideRmsdLike=true;
                    break;
            }
            foreach (var item in Enum.GetValues(typeof(AglomerativeType)))
                comboBox1.Items.Add(Enum.GetName(typeof(AglomerativeType), item));

            if (obj != null)
            {
                comboBox1.SelectedItem = Enum.GetName(typeof(AglomerativeType), obj.hierarchical.linkageType);
                relevantC.Value = obj.hash.relClusters;
                percentData.Value = obj.hash.perData;
                distanceControl1.reference = obj.hierarchical.reference1DjuryH;
                distanceControl1.referenceProfile = obj.hash.profileName;
                distanceControl1.distDef = obj.hierarchical.distance;
                distanceControl1.CAtoms = obj.hierarchical.atoms;
                distanceControl1.profileName = obj.hierarchical.hammingProfile;

                jury1DSetup1.profileName = obj.hash.profileName;

                if (obj.hash.jury)
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
            }
        }
        private void SetOptions()
        {
            localOpt = new Options();
            HashCInput hash = new HashCInput();
            
            localOpt.hierarchical.linkageType = (AglomerativeType)Enum.Parse(typeof(AglomerativeType), comboBox1.SelectedItem.ToString());
            alg = ClusterAlgorithm.uQlustTree;
            if (distanceControl1.distDef == DistanceMeasures.RMSD || distanceControl1.distDef == DistanceMeasures.MAXSUB || distanceControl1.distDef == DistanceMeasures.GDT_TS)
                localOpt.hierarchical.reference1DjuryH = false;
            else
            {
                string hammingProfile = jury1DSetup1.profileName;
                hammingProfile=hammingProfile.Replace(".profiles", "_distance.profile");
                localOpt.hierarchical.hammingProfile = hammingProfile;

                localOpt.hierarchical.reference1DjuryH = true;
                localOpt.hierarchical.jury1DProfileH = jury1DSetup1.profileName;
            }
                    

            localOpt.hierarchical.distance = distanceControl1.distDef;
            localOpt.hierarchical.atoms = distanceControl1.CAtoms;
            localOpt.hierarchical.consensusProfile = jury1DSetup1.profileName;
            if (radioButton1.Checked)
                localOpt.hash.jury = true;
            else
                localOpt.hash.jury = false;

            hash.perData = (int)percentData.Value;
            hash.relClusters = (int)relevantC.Value;
            hash.profileName = jury1DSetup1.profileName;
            if (radioButton3.Checked)
            {
                hash.combine = true;
                hash.fcolumns = false;
            }
            else
            {
                hash.combine = false;
                hash.fcolumns = true;
            }
            hash.selectionMethod = COL_SELECTION.ENTROPY;
            localOpt.hash = hash;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetOptions();
            if ((localOpt.hierarchical.distance==DistanceMeasures.HAMMING || localOpt.hierarchical.distance==DistanceMeasures.COSINE) && !File.Exists(localOpt.hierarchical.hammingProfile) )
            {
                MessageBox.Show("Cannot find profile for hamming distance: " + localOpt.hierarchical.hammingProfile);
                this.DialogResult = DialogResult.None;
                return;
            }
            if (localOpt.hash.profileName==null || localOpt.hash.profileName.Length == 0)
            {
                MessageBox.Show("Profile for generating micro cluster not defined!");
                this.DialogResult = DialogResult.None;
                return;

            }


            this.DialogResult = DialogResult.OK;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProfileTree t = ProfileAutomatic.AnalyseProfileFile(profileFile, SIMDIST.SIMILARITY);
            if (t != null)
            {
                string profileName = "automatic_similarity.profile";
                t.SaveProfiles(profileName);
                jury1DSetup1.profileName = profileName;
                distanceControl1.referenceProfile = profileName;
                if (distanceControl1.distDef == DistanceMeasures.HAMMING)
                {
                    t = ProfileAutomatic.AnalyseProfileFile(profileFile, SIMDIST.DISTANCE);
                    profileName = "automatic_distance.profile";
                    t.SaveProfiles(profileName);
                    distanceControl1.profileName = profileName;

                }

            }
            else
                MessageBox.Show("Profile cannot be generated");

        }

    }
}