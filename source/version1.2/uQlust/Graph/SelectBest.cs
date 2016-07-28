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
    public partial class SelectBest : UserControl
    {
        private bool hideReference
        {
            set
            {
                distanceControl1.hideReference = value;
            }
            get 
            {
                return distanceControl1.hideReference;
            }
        }
        public bool HideAtoms
        {
            set
            {
                distanceControl1.HideAtoms = value;
            }
            get
            {
                return distanceControl1.HideAtoms;
            }
        }
        public string hammingProfile
        {           
            get
            {
                return distanceControl1.profileName;
            }
        }
        public uQlustCore.PDB.PDBMODE CAtoms
        {
            get
            {
                return distanceControl1.CAtoms;
            }
        }
        public string getFileName
        {
            set
            {
                textBox1.Text = value;
            }
            get
            {
                return textBox1.Text;
            }
        }
        public int bestNumber
        {
            set 
            {
                numericUpDown1.Value = value;
            }
            get
            {
                return (int)numericUpDown1.Value;
            }
        }
        public DistanceMeasures measure
        {
            set
            {
                distanceControl1.distDef = value;
            }
            get
            {
                return distanceControl1.distDef;
            }
        }
        public SelectBest()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
                textBox1.Text = openFileDialog1.FileName;     
        }
    }
}
