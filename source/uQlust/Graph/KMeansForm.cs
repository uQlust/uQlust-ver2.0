using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graph;
using uQlustCore.Distance;
using uQlustCore;
using uQlustCore.Profiles;


namespace Graph
{
    
    public partial class KMeansForm : Form
    {
        public KmeansInput localObj = new KmeansInput();
        string profileFile;
        INPUTMODE inputmode;
        public KMeansForm(KmeansInput obj, bool flag,INPUTMODE inputmode,List<string> profiles=null)
        {
            InitializeComponent();
            this.inputmode = inputmode;
            if (profiles != null)
            {
                button3.Visible = true;
                profileFile = profiles[0];
            }
            else
                button3.Visible = false;
            distanceControl1.changedProfile = juryChanged;
            Settings set = new Settings();
            set.Load();
            if (set.mode == INPUTMODE.RNA)
                distanceControl1.HideAtoms = true;

            jury1DSetup1.profileNameChanged = juryChanged;
            if (flag)
                distanceControl1.FreezDist();


            if (obj != null)
            {
                kmeansMaxK.Value = (decimal)obj.maxK;
                if (obj.kMeans_init == Initialization.Jury1D)
                {
                    juryRadio.Checked = true;
                    randomRadio.Checked = false;
                }
                else
                {
                    juryRadio.Checked = false;
                    randomRadio.Checked = true;
                }

                distanceControl1.distDef = obj.kDistance;
                distanceControl1.CAtoms = obj.kAtoms;
                distanceControl1.profileName = obj.hammingProfile;
                jury1DSetup1.profileName = obj.jury1DProfile;
                numericUpDown1.Value = obj.maxIter;
                distanceControl1.reference = obj.reference1Djury;
                distanceControl1.referenceProfile = obj.jury1DProfile;
            }
        }
        private void juryChanged(string newName)
        {
            jury1DSetup1.profileName = newName;
            distanceControl1.referenceProfile = newName;
        }
        private void SetOptions()
        {
            localObj.maxK = (int)kmeansMaxK.Value;
            if (juryRadio.Checked)
                localObj.kMeans_init = Initialization.Jury1D;
            else
                localObj.kMeans_init = Initialization.RANDOM;

            localObj.kAtoms = distanceControl1.CAtoms;
            localObj.kDistance = distanceControl1.distDef;
            localObj.hammingProfile = distanceControl1.profileName;
            localObj.jury1DProfile = jury1DSetup1.profileName;
            localObj.maxIter = (int)numericUpDown1.Value;
            localObj.reference1Djury = distanceControl1.reference;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SetOptions();
            if (distanceControl1.reference)
            {
                if (distanceControl1.referenceProfile == null || distanceControl1.referenceProfile.Length == 0)
                {
                    MessageBox.Show("Profile for reference structure has been not defined!");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if(!distanceControl1.inputMode.Contains(inputmode))
                {
                    MessageBox.Show("Profile " + distanceControl1.referenceProfile + " cannot be used in " + inputmode + " mode");
                    this.DialogResult = DialogResult.None;
                    return;

                }
            }
            if (juryRadio.Checked)
            {
                if (jury1DSetup1.profileName == null || jury1DSetup1.profileName.Length == 0)
                {
                    MessageBox.Show("Profile for 1Djury for initialization has been not defined!");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if (!jury1DSetup1.inputMode.Contains(inputmode))
                {
                    MessageBox.Show("Profile " + jury1DSetup1.profileName + " cannot be used in " + inputmode + " mode");
                    this.DialogResult = DialogResult.None;
                    return;

                }

            }
            if (distanceControl1.distDef == DistanceMeasures.HAMMING)
            {
                if (distanceControl1.profileName == null || distanceControl1.profileName.Length == 0)
                {
                    MessageBox.Show("Profile for hamming distance in dividing has been not defined!");
                    this.DialogResult = DialogResult.None;
                    return;

                }
                if (!distanceControl1.inputMode.Contains(inputmode))
                {
                    MessageBox.Show("Profile " + distanceControl1.profileName + " cannot be used in " + inputmode + " mode");
                    this.DialogResult = DialogResult.None;
                    return;

                }

            }
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void juryRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (juryRadio.Checked)
            {
                jury1DSetup1.Enabled = true;
            }
            else
            {
                jury1DSetup1.Enabled = false;
            }
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
