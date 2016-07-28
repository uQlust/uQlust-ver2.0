using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using uQlustCore;
using uQlustCore.Interface;

namespace Graph
{

    public partial class ClusterComparision : Form, IProgressBar
    {
        Dictionary<string, uQlustCore.ClusterOutput> dicOuput;
        Dictionary<string, int> gridPosition = new Dictionary<string, int>();
        Exception exc = null;
        DataTable resTable;
        long currentV=0;
        long maxV = 1;

        public double ProgressUpdate()
        {
            return (double)currentV / maxV;
        }
        public List<KeyValuePair<string, DataTable>> GetResults()
        {
            List<KeyValuePair<string, DataTable>> t = new List<KeyValuePair<string, DataTable>>();
            t.Add(new KeyValuePair<string,DataTable>("RandIndex values",resTable));
            return t;
        }
        public Exception GetException()
        {
            return exc;
        }

        public ClusterComparision( Dictionary<string, uQlustCore.ClusterOutput> dicOutput)
        {
            this.dicOuput = dicOutput;
            InitializeComponent();
            if (dicOutput != null)
            {
                gridPosition.Clear();
                foreach (var item in dicOutput.Keys)
                {
                    dataGridView1.Rows.Add(1);
                    gridPosition.Add(item, dataGridView1.Rows.Count - 1);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = item;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = dicOutput[item].clusterType;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = dicOutput[item].measure;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = false;
                    if (dicOutput[item].hNode!=null)
                    {
                        List<List<string>> sel = new List<List<string>>();                       
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Value = sel;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].ReadOnly = true;
                        DataGridViewCell cell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4];
                        DataGridViewCheckBoxCell chkCell = cell as DataGridViewCheckBoxCell;
                        chkCell.Value = false;
                      
                        chkCell.FlatStyle = FlatStyle.Standard;
                        chkCell.Style.BackColor = Color.DarkGray;
                        chkCell.Style.ForeColor = Color.LightGray;
                        
                    }
                    else
                    {
                        if (dicOutput[item].clusters != null)
                        {                            
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = dicOutput[item].clusters.Count.ToString();
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Value = dicOutput[item].clusters;
                        }
                    }

                }
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dataGridView1.Rows.Count;i++)
                for (int j = i + 1; j < dataGridView1.Rows.Count; j++)
                {
                    string name=dataGridView1.Rows[i].Cells[0].Value + "+" + dataGridView1.Rows[j].Cells[0].Value;
                    if((bool)dataGridView1.Rows[i].Cells[4].Value && (bool)dataGridView1.Rows[j].Cells[4].Value)
                        if(!listBox1.Items.Contains(name))
                            listBox1.Items.Add(name);
                }
            if (listBox1.Items.Count > 0)
                CalcBtn.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                string str=dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                ClusterOutput output=dicOuput[str];
                if(output.hNode!=null)
                {
                    visHierar select = new visHierar(output.hNode, "Select clusters",output.measure,null);
                    select.ShowCloseButton();
                    DialogResult res=select.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        ((List<List<string>>)(dataGridView1.Rows[e.RowIndex].Cells[5].Value)).Clear();
                        foreach (var item in select.listNodes)                       
                            ((List<List<string>>)(dataGridView1.Rows[e.RowIndex].Cells[5].Value)).Add(item.Key.setStruct);
                        dataGridView1.Rows[e.RowIndex].Cells[3].Value = select.listNodes.Count;
                        dataGridView1.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    }

                }
            }
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {           
                if(!dataGridView1.Rows[e.RowIndex].Cells[4].ReadOnly || !dataGridView1.Columns["Column5"].Visible)
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = !(bool)dataGridView1.Rows[e.RowIndex].Cells[4].Value;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >=0)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            if (listBox1.Items.Count == 0)
                CalcBtn.Enabled = false;
        }
        public void CalcIndex()
        {
            try
            {

                uQlustCore.RandIndex calc = new uQlustCore.RandIndex();

                if (!radioButton1.Checked)
                {
                    maxV = 0;
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        string[] tmp = ((string)listBox1.Items[i]).Split('+');
                        maxV += calc.Size((List<List<string>>)(dataGridView1.Rows[gridPosition[tmp[0]]].Cells[5].Value), (List<List<string>>)(dataGridView1.Rows[gridPosition[tmp[1]]].Cells[5].Value));
                    }
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        string[] tmp = ((string)listBox1.Items[i]).Split('+');
                        calc.ClusterDistance((List<List<string>>)(dataGridView1.Rows[gridPosition[tmp[0]]].Cells[5].Value), (List<List<string>>)(dataGridView1.Rows[gridPosition[tmp[1]]].Cells[5].Value), (string)listBox1.Items[i], ref currentV, 0);
                    }
                }
                else
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                        for (int j = (int)numericUpDown1.Value; j < (int)numericUpDown2.Value; j++, currentV++)
                        {
                            string[] tmp = ((string)listBox1.Items[i]).Split('+');
                            List<List<string>> cl1 = null;
                            List<List<string>> cl2 = null;

                            if (dicOuput[tmp[0]].hNode != null)
                                cl1 = dicOuput[tmp[0]].hNode.GetClusters(j);
                            else
                                if (dicOuput[tmp[0]].clusters != null)
                                    cl1 = dicOuput[tmp[0]].clusters;

                            if (dicOuput[tmp[1]].hNode != null)
                                cl2 = dicOuput[tmp[1]].hNode.GetClusters(j);
                            else
                                if (dicOuput[tmp[1]].clusters != null)
                                    cl2 = dicOuput[tmp[1]].clusters;

                            maxV += calc.Size(cl1, cl2);

                        }

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        string[] tmp = ((string)listBox1.Items[i]).Split('+');
                        List<List<string>> cl1 = null;
                        List<List<string>> cl2 = null;

                        for (int j = (int)numericUpDown1.Value; j < (int)numericUpDown2.Value; j++, currentV++)
                        {

                            if (dicOuput[tmp[0]].hNode != null)
                                cl1 = dicOuput[tmp[0]].hNode.GetClusters(j);
                            else
                                if (dicOuput[tmp[0]].clusters != null)
                                    cl1 = dicOuput[tmp[0]].clusters;

                            if (dicOuput[tmp[1]].hNode != null)
                                cl2 = dicOuput[tmp[1]].hNode.GetClusters(j);
                            else
                                if (dicOuput[tmp[1]].clusters != null)
                                    cl2 = dicOuput[tmp[1]].clusters;


                            if (cl1 == null)
                                throw new Exception(tmp[0] + " is a type of clustering that is not supported by this tool");

                            if (cl2 == null)
                                throw new Exception(tmp[1] + " is a type of clustering that is not supported by this tool");

                            calc.ClusterDistance(cl1, cl2, (string)listBox1.Items[i], ref currentV, j);
                        }
                    }
                }
                resTable = calc.resT;
                currentV = maxV;
            }
            catch(Exception ex)
            {
                exc = ex;
            }
        }


        private void CalcBtn_Click(object sender, EventArgs e)
        {

            Thread start = new Thread(CalcIndex);
            ResultsForm frRes = new ResultsForm();
            Progress prog = new Progress(this, frRes);
            prog.Start();
            prog.Show();
            prog.Focus();
            prog.BringToFront();
            start.Start();                     
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                dataGridView1.Columns["Column5"].Visible = true;
            else
                dataGridView1.Columns["Column5"].Visible = false;
        }
    }
    public class SelectionOpt
    {
        public int start, stop;
        public bool range;

        public List<List<string>> clusters = null;

        public string Name = "Not Defined";

        public override string ToString()
        {
            return Name;
        }

    }
}
