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
    public partial class HierarchicalCForm : Form
    {
        public HierarchicalCInput localOpt;
        public HashCInput localHash;
        public ClusterAlgorithm alg;
        private int SizeHeight;
        private HierarchicalCInput objCopy;
        string profileFile;
        public HierarchicalCForm(HierarchicalCInput obj,ClusterAlgorithm alg,bool flag,List<string> profiles)
        {
            this.alg = alg;
            objCopy = obj;
            InitializeComponent();
            if (profiles != null)
            {
                button3.Visible = true;
                profileFile = profiles[0];
            }
            else
                button3.Visible = false;

            userHash1.regularizationOff();
            SizeHeight = this.Size.Height;
            distanceControl1.refChanged = ReferanceCheckChanged;
            //distanceControl1.FreezeCAtoms();
            Settings set = new Settings();
            set.Load();
            if (set.mode == INPUTMODE.RNA)
                distanceControl1.HideAtoms = true;
            
            foreach (var item in Enum.GetValues(typeof(AglomerativeType)))
                comboBox1.Items.Add(Enum.GetName(typeof(AglomerativeType),item));

            if (obj != null)
            {                
                comboBox1.SelectedItem=Enum.GetName(typeof(AglomerativeType),obj.linkageType);                
                numericBox1.Text = obj.numInitNodes.ToString();
                DBIndex.Value = (decimal)obj.indexDB;
                threshold.Value = (decimal)obj.numberOfStruct;
                maxRepeat.Value = (decimal)obj.repeatTime;
                maxK.Value = (decimal)obj.maxK;
                
                switch (alg)
                {
                    case ClusterAlgorithm.FastHCluster:
                        AglomRadio.Checked = false;
                        FastHClusterRadio.Checked = true;
                        KmeansRadio.Checked = false;
                        userHash1.Visible = false;
                        radioButton3.Checked = false;
                        groupBox1.Visible = true;
                        if (radioButton1.Checked)
                            if (distanceControl1.reference)
                                jury1DSetup3.Visible = false;
                            else
                                jury1DSetup3.Visible = true;
                        this.Size = new Size(this.Size.Width, SizeHeight / 2 + SizeHeight / 4);
                        distanceControl1.reference = obj.reference1DjuryFast;
                        distanceControl1.referenceProfile = obj.consensusProfile;
                        userHash1.profRefFile = obj.jury1DProfileFast;
                        break;
                    case ClusterAlgorithm.HKmeans:
                        AglomRadio.Checked = false;
                        FastHClusterRadio.Checked = false;
                        KmeansRadio.Checked = true;
                        groupBox1.Visible = false;
                        radioButton3.Checked = false;
                        userHash1.Visible = false;
                        this.Size = new Size(this.Size.Width, SizeHeight / 2 + SizeHeight/3);
                        distanceControl1.reference = obj.reference1DjuryKmeans;
                        distanceControl1.referenceProfile = obj.jury1DProfileKmeans;
                        break;
                    case ClusterAlgorithm.uQlustTree:
                        userHash1.Visible = true;
                        AglomRadio.Checked = false;
                        FastHClusterRadio.Checked = false;
                        KmeansRadio.Checked = false;
                        groupBox1.Visible = false;
                        radioButton3.Checked = true;
                        this.Size = new Size(this.Size.Width, SizeHeight);
                        distanceControl1.reference = obj.reference1DjuryH;
                        distanceControl1.referenceProfile = obj.jury1DProfileH;
                        break;
                    default://Aglomerative
                        AglomRadio.Checked = true;
                        FastHClusterRadio.Checked = false;
                        KmeansRadio.Checked = false;
                        groupBox1.Visible = false;
                        userHash1.Visible = false;
                        radioButton3.Checked = false;
                        distanceControl1.reference = obj.reference1DjuryAglom;
                        distanceControl1.referenceProfile = obj.jury1DProfileAglom;
                        break;
                }
                distanceControl1.distDef = obj.distance;
                distanceControl1.CAtoms = obj.atoms;
                distanceControl1.profileName = obj.hammingProfile;
                
                jury1DSetup3.profileName = obj.consensusProfile;
                checkBox2.Checked = obj.ItMeans;
                numericUpDown1.Value = obj.ItNum;
                if (obj.HConsensus)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                if (flag)
                {
                    distanceControl1.FreezDist();
                    distanceControl1.distDef = DistanceMeasures.HAMMING;
                }
               // distanceControl1.CAtoms = PDB.PDBMODE.ONLY_CA;
                userHash1.SetHash(obj.hash);
            }
        }
        private void SetOptions()
        {
            localOpt = new HierarchicalCInput();
            localOpt.linkageType = (AglomerativeType) Enum.Parse(typeof(AglomerativeType), comboBox1.SelectedItem.ToString());
            localOpt.numInitNodes = Convert.ToInt32(numericBox1.Text);
            localOpt.indexDB = (float)DBIndex.Value;
            localOpt.numberOfStruct = (int)threshold.Value;
            localOpt.repeatTime = (int)maxRepeat.Value;
            localOpt.maxK = (int)maxK.Value;
            if (AglomRadio.Checked)
            {
                alg = ClusterAlgorithm.HierarchicalCluster;
                localOpt.reference1DjuryAglom = distanceControl1.reference;
                localOpt.jury1DProfileAglom= distanceControl1.referenceProfile;
            }
            else
                if (FastHClusterRadio.Checked)
                {
                    alg = ClusterAlgorithm.FastHCluster;
                    localOpt.reference1DjuryFast = distanceControl1.reference;
                    localOpt.consensusProfile = distanceControl1.referenceProfile;
                    if (distanceControl1.reference)
                    {
                        localOpt.jury1DProfileFast = distanceControl1.referenceProfile;

                    }
                }
                else
                    if (KmeansRadio.Checked)
                    {
                        alg = ClusterAlgorithm.HKmeans;
                        localOpt.reference1DjuryKmeans = distanceControl1.reference;
                        localOpt.jury1DProfileKmeans = distanceControl1.referenceProfile;
                    }
                    else
                    {
                        alg = ClusterAlgorithm.uQlustTree;
                        localOpt.reference1DjuryH = distanceControl1.reference;
                        localOpt.jury1DProfileH = distanceControl1.referenceProfile;
                    }

            localOpt.distance = distanceControl1.distDef;
            localOpt.atoms = distanceControl1.CAtoms;
            localOpt.hammingProfile = distanceControl1.profileName;
            //localOpt.usedJuryForRef = distanceControl1.reference;
            localOpt.consensusProfile = jury1DSetup3.profileName;
            localOpt.ItMeans = checkBox2.Checked;
            if (radioButton1.Checked)
                localOpt.HConsensus = false;
            else
                localOpt.HConsensus = true ;

            localOpt.ItNum = (int)numericUpDown1.Value;

            localOpt.hash = userHash1.GetHash();

        }

        private void button1_Click(object sender, EventArgs e)
        {            
            SetOptions();
            if(!radioButton3.Checked)
                if (distanceControl1.reference)
                    if (distanceControl1.referenceProfile == null || distanceControl1.referenceProfile.Length==0)
                    {
                        MessageBox.Show("Profile for reference structure in distance measure has been not defined!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
            if(distanceControl1.distDef==DistanceMeasures.HAMMING)
                if (distanceControl1.profileName== null || distanceControl1.profileName.Length == 0)
                {
                    MessageBox.Show("Profile for Hamming distance has been not defined!");
                    this.DialogResult = DialogResult.None;
                    return;
                }
            if(FastHClusterRadio.Checked)
                if (jury1DSetup3.profileName == null || jury1DSetup3.profileName.Length==0)
                {
                    MessageBox.Show("Profile for 1Djury in dividing has been not defined!");
                    this.DialogResult = DialogResult.None;
                    return;
                }
            if(radioButton3.Checked)
            {
                if(userHash1.reg)
                    if(userHash1.profRegFile==null || userHash1.profRegFile.Length==0)
                    {
                        MessageBox.Show("Profile for regularization has been not defined!");
                        this.DialogResult = DialogResult.None;
                        return;

                    }
                if (userHash1.profRefFile == null || userHash1.profRefFile.Length == 0)
                {
                    MessageBox.Show("Profile for reference structure has been not defined!");
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
        private void Visibility(bool hKFlag,bool fHFlag,bool aFlag,bool hashFlag)
        {
            distanceControl1.Visible = true; 
            DBIndex.Visible = hKFlag;
            label12.Visible = hKFlag;
            threshold.Visible = hKFlag;
            label13.Visible = hKFlag;
            maxK.Visible = hKFlag;
            label5.Visible = hKFlag;
            label6.Visible = hKFlag;
            maxRepeat.Visible = hKFlag;
            label2.Visible = aFlag;
            comboBox1.Visible = aFlag;
            numericBox1.Visible = fHFlag;
            label19.Visible = fHFlag;
            groupBox1.Visible = fHFlag;
            userHash1.Visible = hashFlag;
        }
        private void KmeansRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (KmeansRadio.Checked)
                Visibility(true, false, false,false);
            this.Size = new Size(this.Size.Width, SizeHeight / 2 + SizeHeight / 3);
            distanceControl1.reference=objCopy.reference1DjuryKmeans;
            distanceControl1.referenceProfile=objCopy.jury1DProfileKmeans;

        }
        private void ReferanceCheckChanged(bool change)
        {
            if (change)
            {
                if (radioButton1.Checked)                
                    jury1DSetup3.Visible = false;
            }
            else
                if (radioButton1.Checked)
                    jury1DSetup3.Visible = true;
        }
        private void FastHClusterRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (FastHClusterRadio.Checked)
            {
                Visibility(false, true, false,false);
                if (radioButton1.Checked)
                    if (distanceControl1.reference)
                        jury1DSetup3.Visible = false;
                    else
                        jury1DSetup3.Visible = true;

                this.Size = new Size(this.Size.Width, SizeHeight / 2 + SizeHeight / 3);
                distanceControl1.reference = objCopy.reference1DjuryFast;
                distanceControl1.referenceProfile = objCopy.jury1DProfileFast;

            }
        }

        private void AglomRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (AglomRadio.Checked)
            {
                Visibility(false, false, true, false);
                this.Size = new Size(this.Size.Width, SizeHeight / 2 + SizeHeight/5);
                distanceControl1.reference = objCopy.reference1DjuryAglom;
                distanceControl1.referenceProfile = objCopy.jury1DProfileAglom;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked )
            {
                if(distanceControl1.reference)
                    jury1DSetup3.Visible = false;
                else
                    jury1DSetup3.Visible = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                jury1DSetup3.Visible = true;

            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                label1.Enabled = true;
                numericUpDown1.Enabled = true;
            }
            else
            {
                label1.Enabled = false;
                numericUpDown1.Enabled = false;
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)            
                Visibility(false, false, true, true);
            this.Size = new Size(this.Size.Width, SizeHeight);
            distanceControl1.reference=objCopy.reference1DjuryH;
            distanceControl1.referenceProfile = objCopy.jury1DProfileH;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProfileTree t = ProfileAutomatic.AnalyseProfileFile(profileFile, SIMDIST.SIMILARITY);
            if (t != null)
            {
                string profileName = "automatic_similarity.profile";
                t.SaveProfiles(profileName);
                userHash1.profRefFile = profileName;
                userHash1.profRegFile = profileName;
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
