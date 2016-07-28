using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Graph
{
    public partial class CompareBest5Res : Form
    {
        public CompareBest5Res()
        {
            InitializeComponent();
        }

        public void StartCompare(List<Best5> items)
        {
            DataGridViewColumn aux;

            if (items == null || items.Count == 0)
                return;

            foreach (var item in items)
            {
                aux = new DataGridViewColumn();
                aux.HeaderText = item.Text;
                aux.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(aux);
            }
            for(int i=0;i<items[0].GetRowsCounter()-1;i++)
            {   
                DataGridViewRow row=items[0].GetRow(i);
                dataGridView1.Rows.Add();

                dataGridView1.Rows[dataGridView1.Rows.Count-2].Cells[0].Value = row.Cells[1].Value;
            }

            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
               
                int counter=1;
                foreach (var item in items)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null)
                    {
                        DataGridViewRow row = item.GetRow(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        if(row!=null)
                            dataGridView1.Rows[i].Cells[counter].Value = row.Cells[4].Value;
                        else
                            dataGridView1.Rows[i].Cells[counter].Value = double.NaN;
                    }                
                    else
                    {
                        dataGridView1.Rows[i].Cells[counter].Value = double.NaN;
                    }
                    counter++;
                }
            }
            int[] plusCount = new int[dataGridView1.Rows[0].Cells.Count-1];
            int[] minusCount = new int[dataGridView1.Rows[0].Cells.Count-1];


           KeyValuePair<int,double>[] data = new KeyValuePair<int,double> [dataGridView1.Rows[0].Cells.Count-1];

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dataGridView1.Rows[i].Cells.Count; j++)
                {
                    KeyValuePair<int, double> auxK = new KeyValuePair<int, double>(j, Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value.ToString()));
                    data[j - 1] = auxK;
                }
                //Array.Sort(data, delegate(KeyValuePair<int, double> u1, KeyValuePair<int, double> u2) { return u1.Value.CompareTo(u2.Value); });//od najmniejszej do największej
                Array.Sort(data, delegate(KeyValuePair<int, double> u1, KeyValuePair<int, double> u2) { return u2.Value.CompareTo(u1.Value); });//od najwiekszej do najmniejszej
                if (data[0].Value == double.NaN)
                    continue;
                bool test = false;
                for (int j = 0; j < data.Length - 1; j++)
                {
                    dataGridView1.Rows[i].Cells[data[j].Key].Style.BackColor = Color.FromArgb(255,0, 0);
                    if (data[j].Value != data[j + 1].Value)
                    {
                        test = true;
                        break;    
                    }                        
                    

                }
                if (test == false)
                {
                    dataGridView1.Rows[i].Cells[data[data.Length-1].Key].Style.BackColor = Color.FromArgb(255, 0, 0);
                    continue;
                }
                plusCount[data[0].Key - 1]++;
                for (int j = 1; j < data.Length; j++)
                    if(data[j].Value!=data[0].Value)
                        minusCount[data[j].Key - 1]++;
                    else
                        plusCount[data[j].Key - 1]++;
            }
            for (int i = 0; i < plusCount.Length; i++)
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[i + 1].Value = "plus=" + plusCount[i].ToString() + " minus=" + minusCount[i].ToString();

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res=saveFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {

                StreamWriter file = new StreamWriter(saveFileDialog1.FileName);
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    file.Write(dataGridView1.Columns[i].HeaderText + " ");
                file.WriteLine();
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Rows[i].Cells.Count; j++)
                        file.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + " ");
                    file.WriteLine();
                }


                file.Close();
            }

        }
    }
}
