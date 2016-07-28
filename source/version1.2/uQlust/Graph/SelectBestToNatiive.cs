using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using uQlustCore.Distance;
using uQlustCore;


namespace Graph
{
    public partial class SelectBestToNatiive : Form
    {
        public List<string> bestStructures=new List<string>();
        public List<string> bestJuryStructures = new List<string>();
        public List<string> structures = null;
        
        public SelectBestToNatiive(List <string> structures)
        {
            InitializeComponent();
            Settings set = new Settings();
            set.Load();
            if (set.mode == INPUTMODE.RNA)
                selectBest1.HideAtoms = true;

            this.structures = new List<string>(structures);
        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] aux;
            if(checkBox1.Checked)
            {
                if(jury1DSetup1.profileName==null || jury1DSetup1.profileName.Length==0)
                {
                    MessageBox.Show("Profile name for 1djury must be specified!");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                jury1D jury = new jury1D();
                jury.PrepareJury(structures, "",jury1DSetup1.profileName);
                List <string> prep=new List<string>();
                foreach (var item in structures)
                {
                    aux = item.Split(Path.DirectorySeparatorChar);
                    prep.Add(aux[aux.Length - 1]);
                }
                ClusterOutput oc=jury.JuryOptWeights(prep);

                for(int i=0;i<selectBest1.bestNumber;i++)                
                    bestJuryStructures.Add(oc.juryLike[i].Key);                

            }
            this.DialogResult = DialogResult.OK;
            if (selectBest1.getFileName != null && File.Exists(selectBest1.getFileName))
            {
                DistanceMeasures measure = selectBest1.measure;

                structures.Add(selectBest1.getFileName);

                DistanceMeasure dist = null;
                switch (measure)
                {
                    case DistanceMeasures.HAMMING:
                        dist = new JuryDistance(structures, "", false, selectBest1.hammingProfile);
                        break;
                    case DistanceMeasures.MAXSUB:
                        dist = new MaxSub(structures, "", false);
                        break;
                    case DistanceMeasures.RMSD:
                        dist = new Rmsd(structures, "", false, selectBest1.CAtoms);
                        break;
                }

                List<KeyValuePair<string, int>> distList = new List<KeyValuePair<string, int>>();
                aux = selectBest1.getFileName.Split(Path.DirectorySeparatorChar);
                string native = aux[aux.Length - 1];
                foreach (var item in structures)
                {
                    aux = item.Split(Path.DirectorySeparatorChar);
                    int val = dist.GetDistance(native, aux[aux.Length - 1]);
                    distList.Add(new KeyValuePair<string, int>(aux[aux.Length - 1], val));
                }

                distList.Sort((firstPair, nextPair) =>
                {
                    return firstPair.Value.CompareTo(nextPair.Value);
                });

                for (int i = 0; i < selectBest1.bestNumber; i++)
                {
                    bestStructures.Add(distList[i].Key);
                }
            }
        }

       
    }
}
