using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using uQlustCore;
using uQlustCore.Profiles;

namespace Graph
{
    public partial class ThresholdCForm : Form
    {
        public ThresholdCInput localObj=new ThresholdCInput();
        string profileFile;
        public ThresholdCForm(ThresholdCInput obj,bool flag,List<string> profiles=null)
        {
            InitializeComponent();
            if (profiles != null)
            {
                button3.Visible = true;
                profileFile = profiles[0];
            }
            else
                button3.Visible = false;
            if (flag)
                distanceControl1.FreezDist();
            Settings set = new Settings();
            set.Load();
            if (set.mode == INPUTMODE.RNA)
                distanceControl1.HideAtoms = true;
            if (obj != null)
            {
                numericUpDown1.Value = (decimal)obj.distThresh;
                minCluster.Value = (decimal)obj.bakerNumberofStruct;
                distanceControl1.profileName = obj.hammingProfile;
                distanceControl1.distDef = obj.hDistance;
                distanceControl1.CAtoms = obj.hAtoms;
            }
            
        }
        private void SetOptions()
        {
            localObj.distThresh = (int)numericUpDown1.Value;
            localObj.bakerNumberofStruct = (int)minCluster.Value;
            localObj.hDistance = distanceControl1.distDef;
            localObj.hammingProfile = distanceControl1.profileName;
            localObj.hAtoms = distanceControl1.CAtoms;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetOptions();
            if(distanceControl1.distDef==DistanceMeasures.HAMMING)
                if (distanceControl1.profileName == null || distanceControl1.profileName.Length==0)
                {
                    MessageBox.Show("Profile for hamming distance has been not defined!");
                    this.DialogResult = DialogResult.None;
                    return;
                }
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProfileTree t;
            if (distanceControl1.distDef == DistanceMeasures.HAMMING)
            {
                t = ProfileAutomatic.AnalyseProfileFile(profileFile, SIMDIST.DISTANCE);
                if (t != null)
                {
                    string profileName = "automatic_distance.profile";
                    t.SaveProfiles(profileName);
                    distanceControl1.profileName = profileName;
                }
                else
                    MessageBox.Show("Profile cannot be generated");
            }

        }
    }
}
