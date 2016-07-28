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

namespace WorkFlows
{
    public partial class Rna_Protein_UserDef : Form
    {
        bool previous = false;    
        Form parent;
        static public ResultWindow results = new ResultWindow();
        public Settings set = new Settings();
        public Rna_Protein_UserDef(Form parent=null)
        {
            InitializeComponent();
            set.Load();
            this.parent = parent;
        }     

        private void button1_Click(object sender, EventArgs e)
        {
            switch(((Button)sender).Text)
            {
                case "Protein":
                    set.mode = INPUTMODE.PROTEIN;
                    break;
                case "RNA":
                    set.mode = INPUTMODE.RNA;
                    break;
                case "User defined":
                    set.mode = INPUTMODE.USER_DEFINED;
                    break;

            }
            ClusteringChoose cluster = new ClusteringChoose(set,this);
            cluster.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            previous = true;
            if(parent!=null)
                parent.Show();
            this.Close();
        }

        private void Rna_Protein_UserDef_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!previous && parent!=null)
                parent.Close();       

        }


    }
}
