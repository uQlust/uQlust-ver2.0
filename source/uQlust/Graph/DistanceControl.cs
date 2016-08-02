using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using uQlustCore;
using uQlustCore.PDB;

namespace Graph
{
    public delegate void ReferenceBoxChanged(bool change);
    public partial class DistanceControl : UserControl
    {
        public ReferenceBoxChanged refChanged = null;
        public List<INPUTMODE> inputMode = null;

        public ProfileNameChanged changedProfile
        {
            set
            {
                jury1DSetup1.profileNameChanged = value;
                inputMode=jury1DSetup1.inputMode;
            }

        }
        private bool ProfileInfo=true;
        public bool profileInfo
        {
            set 
            {
                ProfileInfo = value;
                label1.Visible = value;
                label2.Visible = value;
                label16.Visible = value;
            }
            get
            {
                return ProfileInfo;
            }
        }

        private bool HideSetup;
        public bool hideSetup
        {
            set
            {
                HideSetup = value;
                button5.Visible = !value;
                label1.Visible = !value;
                label16.Visible = !value;
            }
            get
            {
                return HideSetup;
            }
        }
        private bool HideRef;
        public bool hideReference
        {
            set
            {
                HideRef = value;
                if (HideRef)
                    HideReference();
            }
            get
            {
                return HideRef;
            }

        }
        public bool reference
        {
            get
            {
                return referenceBox.Checked;                
            }
            set
            {
                jury1DSetup1.Visible = value;
                referenceBox.Checked = value;
            }
        }
        
        public string referenceProfile
        {
            get
            {
                return jury1DSetup1.profileName;
            }
            set
            {
                jury1DSetup1.profileName = value;
                inputMode = jury1DSetup1.inputMode;
            }
        }
        private bool hideatoms;
        public bool HideAtoms
        {
            set
            {
                hideatoms = value;
                if (value == true)
                    groupBox1.Visible = false;
                else
                    groupBox1.Visible = true;
            }
            get
            {
                return hideatoms;
            }
        }
        bool hideHamming;
        public bool HideHamming
        {
            set
            {
                hideHamming=value;
                radio1DJury.Enabled = !hideHamming;
            }
            get { return hideHamming; }
        }
        bool hideRmsdLike;
        public bool HideRmsdLike
        {
            set
            {
                hideRmsdLike = value;
                radioRmsd.Enabled = !hideRmsdLike;
                radioMaxSub.Enabled = !hideRmsdLike;
                radioGDT_TS.Enabled = !hideRmsdLike;
                groupBox1.Enabled = !hideRmsdLike;
            }
            get
            {
                return hideRmsdLike;
            }
        }
        bool hideCosine;
        public bool HideCosine
        {
            set
            {
                hideCosine = value;
                radioEucl.Enabled = !hideCosine;
            }
            get
            {
                return hideCosine;
            }
        }
        public DistanceMeasures distDef 
        {
            get
            {
                if (radio1DJury.Checked)
                    return DistanceMeasures.HAMMING;
                else
                    if (radioEucl.Checked)
                        return DistanceMeasures.COSINE;
                    else
                        if (radioRmsd.Checked)
                            return DistanceMeasures.RMSD;
                        else
                            if (radioGDT_TS.Checked)
                                return DistanceMeasures.GDT_TS;
                            else
                                return DistanceMeasures.MAXSUB;
            }
            set
            {
                switch (value)
                {
                    case DistanceMeasures.HAMMING:
                    case DistanceMeasures.COSINE:
                        
                        if(value==DistanceMeasures.COSINE)
                            radioEucl.Checked = true;
                        else
                            radio1DJury.Checked = true;
                        if(value==DistanceMeasures.HAMMING)
                            referenceBox.Checked = true;
                        if(profileFileName!=null && profileFileName.Length>0)
                            label16.Text = LabelActiveProfiles(profileFileName);
                        radioMaxSub.Checked = false;
                        radioGDT_TS.Checked = false;
                        radioRmsd.Checked = false;
                        groupBox1.Enabled = false;
                        label1.Visible = true;
                        label2.Visible = true;
                        label16.Visible = true;
                        break;
                    case DistanceMeasures.MAXSUB:
                        radio1DJury.Checked = false;
                        radioMaxSub.Checked = true;
                        radioGDT_TS.Checked = false;
                        radioEucl.Checked = false;
                        radioRmsd.Checked = false;
                        button5.Visible = false;
                        label16.Visible = false;
                        label1.Visible = false;
                        label2.Visible = false;
                        break;
                    case DistanceMeasures.GDT_TS:
                        radio1DJury.Checked = false;
                        radioMaxSub.Checked = false;
                        radioGDT_TS.Checked = true;
                        radioEucl.Checked = false;
                        radioRmsd.Checked = false;
                        button5.Visible = false;
                        label16.Visible = false;
                        label1.Visible = false;
                        label2.Visible = false;
                        break;
                    case DistanceMeasures.RMSD:
                        radio1DJury.Checked = false;
                        radioMaxSub.Checked = false;
                        radioGDT_TS.Checked = false;
                        radioEucl.Checked = false;
                        radioRmsd.Checked = true;
                        button5.Visible = false;
                        label16.Visible = false;
                        label1.Visible = false;
                        label2.Visible = false;
                        groupBox1.Enabled = true;
                        break;

                }

            }
        }
        public void FreezeCAtoms()
        {
            radioButton5.Enabled = false;
            radioButton6.Enabled = false;
        }
        public void FreezDist()
        {
            radioRmsd.Enabled = false;
            radioMaxSub.Enabled = false;
            radioGDT_TS.Enabled = false;
        }
        public void UnfreezDist()
        {
            radioRmsd.Enabled = true;
            radioMaxSub.Enabled = true;
            radioGDT_TS.Enabled = true;
        }        
        public uQlustCore.PDB.PDBMODE CAtoms
        {
            get
            {
                if (HideAtoms == true)
                {
                    return uQlustCore.PDB.PDBMODE.ALL_ATOMS;
                }
                else
                {
                    if (radioButton5.Checked)
                        return uQlustCore.PDB.PDBMODE.ONLY_CA;
                    else
                        return uQlustCore.PDB.PDBMODE.ALL_ATOMS;
                }
            }
            set
            {
                if (value == uQlustCore.PDB.PDBMODE.ONLY_CA)
                {
                    radioButton5.Checked = true;
                    radioButton6.Checked = false;
                }
                else
                {
                    radioButton5.Checked = false;
                    radioButton6.Checked = true;
                }

            }
        }
        private string profileFileName;
        public string profileName
        {
            get
            {
                return profileFileName;
            }
            set
            {
                if (value != null)
                {
                    this.profileFileName = value;
                    label2.Text = value;
                    label16.Text = LabelActiveProfiles(value);

                }

            }


        }
        public DistanceControl()
        {
            InitializeComponent();
        }
        public void HideReference()
        {
            this.Size = new Size(this.Size.Width, 123) ;
            referenceBox.Visible = false;
            jury1DSetup1.Visible = false;

        }
        public bool CheckIntegrity()
        {
            if(radio1DJury.Checked || radioEucl.Checked)
            {
                if (profileFileName==null || profileFileName.Length == 0)
                    return false;
            }
            if (reference)
                if (jury1DSetup1.profileName == null || jury1DSetup1.profileName.Length == 0)
                    return false;
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProfileForm profDef;

            profDef = new ProfileForm(profileFileName, "",filterOPT.DISTANCE);


            DialogResult res = profDef.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    profileName = profDef.fileName;
                    label16.Text = LabelActiveProfiles(profileFileName);
                }
                catch
                {
                    MessageBox.Show("Profiles could not be saved!");
                }
            }
        }
        private string LabelActiveProfiles(string fileName)
        {
            string outStr = "";
            ProfileTree tree = new ProfileTree();
            if (fileName.Length > 0)
                try
                {
                    tree.LoadProfiles(fileName);
                }
                catch(Exception)
                {
                    label2.Text = "";
                    return null;
                }
            else
                return null;
            inputMode = tree.GetModes();
            List<profileNode> active = tree.GetActiveProfiles();
            outStr = "Active profiles: ";
            if(active!=null)
                for (int i = 0; i < active.Count; i++)
                {
                    outStr += active[i].profName;
                    if (i < active.Count - 1)
                        outStr += ", ";
                }

            return outStr;
        }

        private void radio1DJury_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            if (radio1DJury.Checked || radioEucl.Checked)
            {
                if (!hideSetup)
                {
                    button5.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label16.Visible = true;
                }
                if(profileFileName!=null && profileFileName.Length>0)
                    label16.Text = LabelActiveProfiles(profileFileName);

                if(radio1DJury.Checked)
                    referenceBox.Checked = true;
            }

        }

        private void radioRmsd_CheckedChanged(object sender, EventArgs e)
        {
            if(HideAtoms==false)
                groupBox1.Visible = true;
            else
                groupBox1.Visible = false;
            button5.Visible = false;
            label16.Visible = false;
            label1.Visible = false;
            label2.Visible = false;

        }

        private void radioMaxSub_CheckedChanged(object sender, EventArgs e)
        {            
            groupBox1.Visible = false;
            button5.Visible = false;
            label16.Visible = false;
            label1.Visible = false;
            label2.Visible = false;

        }

        private void referenceBox_CheckedChanged(object sender, EventArgs e)
        {
            if (refChanged != null)
                refChanged(referenceBox.Checked);

            jury1DSetup1.Visible = referenceBox.Checked;
                       

        }

        private void jury1DSetup1_Load(object sender, EventArgs e)
        {

        }


    
    }
}
