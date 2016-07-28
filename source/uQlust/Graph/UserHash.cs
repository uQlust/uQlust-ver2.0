using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using uQlustCore;

namespace Graph
{
    public partial class UserHash : UserControl
    {

        ProfileNameChanged profKeyChanged=null;
        public List <INPUTMODE> inputMode;
        public UserHash()
        {
            InitializeComponent();
            jury1DSetup1.profileNameChanged = profKeyChanged;

        }

        public bool reg
        {
            set
            {
                label2.Enabled = value;
                numericUpDown2.Enabled = value;
                label3.Enabled = value;
                numericUpDown3.Enabled = value;
                label5.Enabled = value;
                jury1DSetup2.Enabled = value;

                checkBox1.Checked = value;
            }
            get 
            {
                return checkBox1.Checked;
            }
        }
        public int winSize
        {
            get
            {
                return Convert.ToInt16(numericUpDown2.Value);
            }
            set
            {
                numericUpDown2.Value = value;
            }
            
        }
        private void regularizationOnOff(bool val)
        {
            checkBox1.Visible = val;
            label2.Visible = val;
            numericUpDown2.Visible = val;
            label3.Visible = val;
            numericUpDown3.Visible = val;
            label5.Visible = val;
            jury1DSetup2.Visible = val;
        }
        public void regularizationOn()
        {
            regularizationOnOff(true);
        }
        public void regularizationOff()
        {
            regularizationOnOff(false);
        }
        public int distThres
        {
            get
            {
                return Convert.ToInt16(numericUpDown3.Value);
            }
            set
            {
                numericUpDown3.Value = value;
            }
        }
    
        public int nodesNumber
        {
            get
            {
                return (int)numericUpDown3.Value;
            }
            set 
            {
                numericUpDown3.Value = value;
            }
        }
   
        public string profRegFile
        {
            get
            {
                return jury1DSetup2.profileName;
            }
            set 
            {
                jury1DSetup2.profileName = value;
                if(inputMode==null)
                    inputMode = jury1DSetup2.inputMode;
            }
        }
        public string profRefFile
        {
            get
            {
                return jury1DSetup1.profileName;
            }
            set
            {
                jury1DSetup1.profileName = value;
                if (inputMode == null)
                    inputMode = jury1DSetup1.inputMode;
            }
        }

        public void UpdateMode()
        {
            if (jury1DSetup1.Enabled)
                inputMode = jury1DSetup1.inputMode;
            else
                if (jury1DSetup2.inputMode!=null)
                    inputMode = jury1DSetup2.inputMode;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                label2.Enabled = true;
                label3.Enabled = true;
                label5.Enabled = true;
                jury1DSetup2.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
            }
            else
            {
                label2.Enabled = false;
                label3.Enabled = false;
                label5.Enabled = false;
                jury1DSetup2.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;

            }
        }
      
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)            
                jury1DSetup1.Enabled = true;                           
            else                        
                jury1DSetup1.Enabled = false;            
        }
        public HashCInput GetHash()
        {
            HashCInput aux = new HashCInput();
           

            aux.jury = radioButton1.Checked;
            aux.regular = checkBox1.Checked;
            aux.wSize = (int)numericUpDown2.Value;
            aux.regThreshold = (int)numericUpDown3.Value;
            aux.profileNameReg = jury1DSetup2.profileName;
            aux.profileName = jury1DSetup1.profileName;
            aux.reqClusters = (int)numericUpDown1.Value;

            return aux;

        }
        public void SetHash(HashCInput aux)
        {
            radioButton1.Checked = aux.jury;
            radioButton2.Checked = !aux.jury;
            checkBox1.Checked=aux.regular;
            numericUpDown2.Value=aux.wSize;
            numericUpDown3.Value=aux.regThreshold;
            jury1DSetup2.profileName=aux.profileNameReg;
            jury1DSetup1.profileName=aux.profileName;
            if (aux.reqClusters < 10)
                aux.reqClusters = 10;
            numericUpDown1.Value = aux.reqClusters;
        }
    }
}
