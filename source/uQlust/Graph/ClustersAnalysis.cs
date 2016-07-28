using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using uQlustCore;

namespace Graph
{
    public delegate void Analizecluster(List<ClusterOutput> clOut, string name);
    public partial class ClustersAnalysis : Form
    {
        Dictionary<string, ClusterOutput> outputs;        
        public Analizecluster analizeCluster=null;
        public ClustersAnalysis(Dictionary<string, ClusterOutput> outputs,bool multi=false)
        {
            InitializeComponent();
            if (outputs == null)
                return;
            this.outputs = outputs;
            
            dataGridView1.MultiSelect = multi;
            if (outputs != null && outputs.Count > 0)
            {
                dataGridView1.Rows.Add(outputs.Count);
                int i = 0;
                foreach (var item in outputs.Keys)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    dataGridView1.Rows[i].Cells[0].Value = outputs[item].name;
                    dataGridView1.Rows[i].Cells[1].Value = outputs[item].clusterType;
                    dataGridView1.Rows[i].Cells[2].Value = outputs[item].measure;
                    dataGridView1.Rows[i++].Cells[3].Value = outputs[item].dirName;
                }
            }
        }     

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = (string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value;
//            ClusterStat stat = new ClusterStat(outputs[name],dirName,null,name);

            List<ClusterOutput> list = new List<ClusterOutput>();
            
              for(int i=0;i<dataGridView1.SelectedRows.Count;i++)
              {
                  string nameS = (string)dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[0].Value;
                  list.Add(outputs[nameS]);
              }

//            stat.Show();
            if(analizeCluster!=null)
                analizeCluster(list, name);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)            
                dataGridView1.SelectedRows[i].Selected = true;
            
        }

        private void deselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.SelectedRows[i].Selected = false;

        }

        private void analyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1_CellDoubleClick(sender, null);
        }
    }
}


