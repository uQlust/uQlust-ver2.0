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

namespace Graph
{
    public partial class Aglomerative : Form
    {
        public HierarchicalCInput localOpt;
        public ClusterAlgorithm alg;

        public Aglomerative(HierarchicalCInput obj, ClusterAlgorithm alg, INPUTMODE mode, bool flag, List<string> profiles)
        {
            InitializeComponent();
            Settings set = new Settings();
            set.Load();
            switch (set.mode)
            {
                case INPUTMODE.RNA:
                    distanceControl1.HideAtoms = true;
                    break;
                case INPUTMODE.USER_DEFINED:
                    distanceControl1.HideRmsdLike = true;
                    break;
            }
            foreach (var item in Enum.GetValues(typeof(AglomerativeType)))
                comboBox1.Items.Add(Enum.GetName(typeof(AglomerativeType), item));

            if (obj != null)
            {
                comboBox1.SelectedItem = Enum.GetName(typeof(AglomerativeType), obj.linkageType);
                distanceControl1.reference = obj.reference1DjuryH;
                distanceControl1.referenceProfile = obj.jury1DProfileH;
                distanceControl1.distDef = obj.distance;
                distanceControl1.CAtoms = obj.atoms;
                distanceControl1.profileName = obj.hammingProfile;
            }
        }
        private void SetOptions()
        {
            localOpt = new HierarchicalCInput();

            localOpt.linkageType = (AglomerativeType)Enum.Parse(typeof(AglomerativeType), comboBox1.SelectedItem.ToString());
            alg = ClusterAlgorithm.HierarchicalCluster;
            if (distanceControl1.distDef == DistanceMeasures.RMSD || distanceControl1.distDef == DistanceMeasures.MAXSUB || distanceControl1.distDef == DistanceMeasures.GDT_TS)
                localOpt.reference1DjuryH = false;
            else
            {
                string hammingProfile = distanceControl1.profileName;
              
                localOpt.hammingProfile = hammingProfile;
                localOpt.reference1DjuryAglom = distanceControl1.reference;
                localOpt.jury1DProfileAglom = hammingProfile.Replace("_distance", "");
            }


            localOpt.distance = distanceControl1.distDef;
            localOpt.atoms = distanceControl1.CAtoms;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetOptions();
            if ((localOpt.distance == DistanceMeasures.HAMMING || localOpt.distance == DistanceMeasures.COSINE) && !File.Exists(localOpt.hammingProfile))
            {
                MessageBox.Show("Cannot find profile for hamming distance: " + localOpt.hammingProfile);
                this.DialogResult = DialogResult.None;
                return;
            }

            this.DialogResult = DialogResult.OK;
        }


    }
}
