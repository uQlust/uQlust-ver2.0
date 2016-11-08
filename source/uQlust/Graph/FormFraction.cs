using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uQlustCore;
namespace Graph
{
    public partial class FormFraction : Form
    {
        public DistanceMeasures dist;
        public string profileName;
        public double distThreshold;
        public int consideredClusters;
        public FormFraction(bool juryLike)
        {
            InitializeComponent();
            
            jury1D.Checked = false;
            //jury1D.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (jury1D.Checked)
                jury1DSetup1.Visible = true;
            else
                jury1DSetup1.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Rmsd.Checked)
                dist = DistanceMeasures.RMSD;
            else
                if (MaxSub.Checked)
                    dist = DistanceMeasures.MAXSUB;
                else
                    dist = DistanceMeasures.GDT_TS;

            if (jury1D.Checked)
            {
                profileName = jury1DSetup1.profileName;
                if (profileName == null || profileName.Length == 0)
                    this.DialogResult = DialogResult.None;
            }
            else
                profileName = null;

            distThreshold = Convert.ToDouble(textBox1.Text);
            consideredClusters = (int)numClusters.Value;
            this.DialogResult = DialogResult.OK;
        }

        private void Rmsd_CheckedChanged(object sender, EventArgs e)
        {
            if (Rmsd.Checked)
                textBox1.Text = "2";
            else
                textBox1.Text = "0.02";
        }

     
    }
}
