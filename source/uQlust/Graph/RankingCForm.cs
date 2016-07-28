using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graph;
using uQlustCore;
using uQlustCore.Profiles;

namespace Graph
{
    public partial class RankingCForm : Form
    {
        public RankingCInput localObj = new RankingCInput();
        public ClusterAlgorithm alg;
        string profileFile = "";
        INPUTMODE inputMode;
        public RankingCForm(RankingCInput obj,ClusterAlgorithm alg,bool flag,INPUTMODE inputMode,List<string> profFiles=null)
        {
            this.alg = alg;
            this.inputMode = inputMode;
            InitializeComponent();
            if (profFiles != null)
            {
                profileFile = profFiles[0];
                button3.Visible = true;
            }
            else
                button3.Visible = false;
            Settings set = new Settings();
            set.Load();
            if (set.mode == INPUTMODE.RNA)
                distanceControl1.HideAtoms = true;

            if (flag)
            {
                distanceControl1.FreezDist();
                sift.Enabled = false;
                if (sift.Checked)
                {
                    jury1d.Checked = true;
                
                }
            }
            switch(alg)
            {    
                case ClusterAlgorithm.Jury1D:            
                    jury1d.Checked = true;
                    jury3d.Checked = false;
                    sift.Checked = false;
                    break;
                case ClusterAlgorithm.Jury3D:
                    jury1d.Checked = false;
                    jury3d.Checked = true;
                    sift.Checked = false;
                    break;
                case ClusterAlgorithm.Sift:
                    jury1d.Checked = false;
                    jury3d.Checked = false;
                    sift.Checked = true;
                    break;
                default:
                    jury1d.Checked = true;
                    jury3d.Checked = false;
                    sift.Checked = false;
                    break;
             }
            if (obj != null)
            {
                distanceControl1.distDef = obj.oDistance;
                distanceControl1.CAtoms = obj.oAtoms;
                distanceControl1.profileName = obj.hammingProfile;
                jury1DSetup1.profileName = obj.juryProfile;
                distanceControl1.referenceProfile = obj.referenceProfile;
                distanceControl1.reference = obj.reference1Djury;               
            }
            if(jury1d.Checked)
                distanceControl1.Enabled = false;

            
        }
        private void SetOptions()
        {
            if (jury1d.Checked)
                alg = ClusterAlgorithm.Jury1D;
            else
                if (jury3d.Checked)
                    alg = ClusterAlgorithm.Jury3D;
                else
                    alg = ClusterAlgorithm.Sift;

            localObj.oDistance = distanceControl1.distDef;
            localObj.oAtoms = distanceControl1.CAtoms;
            localObj.hammingProfile = distanceControl1.profileName;
            localObj.juryProfile = jury1DSetup1.profileName;
            localObj.reference1Djury = distanceControl1.reference;
            localObj.referenceProfile=distanceControl1.referenceProfile;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetOptions();
            if (jury1d.Checked)
            {
                if (jury1DSetup1.profileName == null || jury1DSetup1.profileName.Length == 0)
                {
                    MessageBox.Show("Profile for 1Djury has been not defined!");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if(!jury1DSetup1.inputMode.Contains(inputMode))
                {
                    MessageBox.Show("Profile "+jury1DSetup1.profileName+"cannot be used in "+inputMode+
                        " mode");
                    this.DialogResult = DialogResult.None;
                    return;

                }
            }
            if(jury3d.Checked)
                if (distanceControl1.distDef == DistanceMeasures.HAMMING)
                {
                    if (distanceControl1.profileName == null || distanceControl1.profileName.Length == 0)
                    {
                        MessageBox.Show("Profile for hamming distance has been not defined!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    if (!jury1DSetup1.inputMode.Contains(inputMode))
                    {
                        MessageBox.Show("Profile " + jury1DSetup1.profileName + "cannot be used in " + inputMode +
                            " mode");
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


        private void jury3d_CheckedChanged(object sender, EventArgs e)
        {
            distanceControl1.Enabled = true;
            jury1DSetup1.Enabled = false;
            
        }

        private void sift_CheckedChanged(object sender, EventArgs e)
        {
            distanceControl1.Enabled = false;
            jury1DSetup1.Enabled = false;
        }

        private void jury1d_Click(object sender, EventArgs e)
        {
            distanceControl1.Enabled = false;
            jury1DSetup1.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(jury1DSetup1.Enabled)
            {
                ProfileTree t = ProfileAutomatic.AnalyseProfileFile(profileFile, SIMDIST.SIMILARITY);

                if (t != null)
                {

                    string profileName = "automatic_similarity.profile";
                    t.SaveProfiles(profileName);
                    jury1DSetup1.profileName = profileName;
                }
                else
                    MessageBox.Show("Could not create automatic profile");
            }
            if(distanceControl1.Enabled)
            {
                if(distanceControl1.distDef==DistanceMeasures.HAMMING)
                {
                    ProfileTree t = ProfileAutomatic.AnalyseProfileFile(profileFile, SIMDIST.DISTANCE);
                    string profileName = "automatic_distance.profile";
                    t.SaveProfiles(profileName);
                    distanceControl1.profileName = profileName;
                }
                if(distanceControl1.reference)
                {
                    ProfileTree t = ProfileAutomatic.AnalyseProfileFile(profileFile, SIMDIST.SIMILARITY);
                    string profileName = "automatic_similarity.profile";
                    t.SaveProfiles(profileName);
                    distanceControl1.referenceProfile = profileName;

                }
            }
        }
    }
}
