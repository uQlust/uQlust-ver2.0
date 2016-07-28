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
    public partial class HashClusterForm : Form
    {
        string profileFile;
        public HashCInput localInput=new HashCInput();
        public HashClusterForm(HashCInput input,List<string> profilesF=null)
        {
            InitializeComponent();
            if (profilesF != null)
            {
                profileFile = profilesF[0];
                button3.Visible = true;
            }
            else
                button3.Visible = false;
            checkBox1.Checked = input.regular;
            numericUpDown2.Value = input.wSize;
            numericUpDown3.Value = input.regThreshold;
            radioButton3.Checked = input.combine;
            jury1DSetup1.profileName = input.profileName;
            jury1DSetup2.profileName = input.profileNameReg;
            relevantC.Value = input.relClusters;
            percentData.Value = input.perData;
            radioButton4.Checked = input.fcolumns;
            switch (input.selectionMethod)
            {
                case COL_SELECTION.ENTROPY:
                    comboBox1.SelectedIndex = 1;
                    break;
                case COL_SELECTION.META_COL:
                    comboBox1.SelectedIndex = 0;
                    break;
            }


            if (input.jury)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            localInput.combine = radioButton3.Checked;
            localInput.regular = checkBox1.Checked;
            localInput.regThreshold = (int)numericUpDown3.Value;
            localInput.wSize = (int)numericUpDown2.Value;
            localInput.profileName = jury1DSetup1.profileName;
            localInput.fcolumns = radioButton4.Checked;
            localInput.relClusters = (int)relevantC.Value;
            localInput.perData = (int)percentData.Value;
            localInput.profileNameReg = jury1DSetup2.profileName;

            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    localInput.selectionMethod = COL_SELECTION.ENTROPY;
                    break;
                case 0:
                    localInput.selectionMethod = COL_SELECTION.META_COL;
                    break;
            }
            if (radioButton1.Checked)
                localInput.jury = true;
            else
                localInput.jury = false;
            if(checkBox1.Checked)
                if (jury1DSetup2.profileName == null || jury1DSetup2.profileName.Length==0)
                {
                    MessageBox.Show("Profile for regularization is not defined!");
                    this.DialogResult = DialogResult.None;
                    return;
                }

            if (jury1DSetup1.ProductName == null || jury1DSetup1.ProductName.Length == 0)
            {
                MessageBox.Show("Profile for reference structure is not defined!");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                this.DialogResult = DialogResult.OK;

            
        }
        private void ColumnSelection(bool flag)
        {
            //radioButton4.Enabled = flag;
        }


        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label2.Enabled = true;
                label3.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                label5.Enabled = true;
                jury1DSetup2.Enabled = true;
            }
            else
            {
                label2.Enabled = false;
                label3.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                label5.Enabled = false;
                jury1DSetup2.Enabled = false;
            }
       }
        private void BoxHamming(bool flag)
        {
            //radioButton3.Enabled = flag;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                label6.Enabled = true;
                label7.Enabled = true;
                relevantC.Enabled = true;
                percentData.Enabled = true;
                ColumnSelection(false);
            }
            else
            {
                ColumnSelection(true);
                label6.Enabled = false;
                label7.Enabled = false;
                relevantC.Enabled = false;
                percentData.Enabled = false;

            }


        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                BoxHamming(false);
                comboBox1.Enabled = true;
                relevantC.Enabled = true;
                percentData.Enabled = true;
                label6.Enabled = true;
                label7.Enabled = true;
            }
            else
            {
                BoxHamming(true);
                comboBox1.Enabled = false;
                relevantC.Enabled = false;
                percentData.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProfileTree t = ProfileAutomatic.AnalyseProfileFile(profileFile, SIMDIST.SIMILARITY);
            string profileName = "automatic_similarity.profile";
            t.SaveProfiles(profileName);
            jury1DSetup1.profileName = profileName;
            jury1DSetup2.profileName = profileName;
        }


    }
}
