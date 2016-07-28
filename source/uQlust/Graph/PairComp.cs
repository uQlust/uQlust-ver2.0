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
    public partial class PairComp : Form
    {
        bool smallValue = true;
        public PairComp(List<Best5> items,bool smallValue)
        {
            InitializeComponent();
            this.smallValue=smallValue;

            DataGridViewColumn aux;
            foreach (var item in items)
            {
                aux = new DataGridViewColumn();
                aux.HeaderText = item.Text;
                aux.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                aux.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(aux);
            }
            dataGridView1.Rows.Add(1);
            int columnNumber = 1;
            foreach (var item1 in items)
            {
                for(int i=0;i<dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells.Count;i++)
                    if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[columnNumber].Value != null &&
                         dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[columnNumber].Value.ToString().Length > 0)
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[columnNumber++].Value = item1.Name;
                        break;
                    }

            }

            Dictionary<string, double> itemDic = new Dictionary<string, double>();

            foreach (var item1 in items)
            {
                itemDic.Clear();
                for (int i = 0; i < item1.GetRowsCounter() - 1; i++)
                {
                    DataGridViewRow row = item1.GetRow(i);
                    if(row.Cells[1].Value!=null && !itemDic.ContainsKey(row.Cells[1].Value.ToString()))
                        itemDic.Add(row.Cells[1].Value.ToString(), Convert.ToDouble(row.Cells[4].Value));
                }

                dataGridView1.Rows.Add(1);
                DataGridViewRow rowX = dataGridView1.Rows[dataGridView1.Rows.Count-2];
                rowX.Cells[0].Value = item1.GetRow(0).Cells[0].Value;
                columnNumber = 1;
                foreach (var item2 in items)
                {

                    if (item1 == item2)
                    {
                        rowX.Cells[columnNumber++].Value = "---";
                        continue;
                    }
                    
                    int counterG = 0, counterB = 0;
                    for (int i = 0; i < item2.GetRowsCounter() - 2; i++)
                    {
                        DataGridViewRow row = item2.GetRow(i);


                        if (itemDic.ContainsKey(row.Cells[1].Value.ToString()))
                        {
                            if (itemDic[row.Cells[1].Value.ToString()] > 100 || Convert.ToDouble(row.Cells[4].Value) > 100)
                                continue;

                            if (smallValue)
                            {
                                if (itemDic[row.Cells[1].Value.ToString()] < Convert.ToDouble(row.Cells[4].Value))
                                    counterG++;
                                else
                                    if (itemDic[row.Cells[1].Value.ToString()] > Convert.ToDouble(row.Cells[4].Value))
                                        counterB++;
                            }
                            else
                                if (itemDic[row.Cells[1].Value.ToString()] > Convert.ToDouble(row.Cells[4].Value))
                                    counterG++;
                                else
                                    if (itemDic[row.Cells[1].Value.ToString()] < Convert.ToDouble(row.Cells[4].Value))
                                        counterB++;
                        }
                    }
                    rowX.Cells[columnNumber++].Value = "good=" + counterG + " bad=" + counterB;
                }
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res=saveFileDialog1.ShowDialog();
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
