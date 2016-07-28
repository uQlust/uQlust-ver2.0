using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using uQlustCore;

namespace Graph
{
    public partial class ClusterClassification : Form
    {
        ClusterOutput clOut;
        List<List<string>> selected = null;
        Dictionary<string, string> classDef = new Dictionary<string, string>();

        public ClusterClassification(ClusterOutput clOut)
        {
            InitializeComponent();
            this.clOut = clOut;
            if (clOut.hNode != null)
            {
                button2.Visible = true;
                button3.Enabled = false;
            }
            else
                if (clOut.clusters != null)
                    selected = clOut.clusters;
                else
                {
                    MessageBox.Show("This tool is not design for this type of clustering!");
                    this.Close();
                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                if (selected != null && selected.Count>0)
                    button3.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (clOut.hNode != null)
            {
                visHierar formSelect = new visHierar(clOut.hNode, "Select clusters",clOut.measure,null);
                formSelect.ShowCloseButton();
                DialogResult res = formSelect.ShowDialog();

                if (res == DialogResult.OK)
                {
                    if (formSelect.listNodes != null)
                    {
                        if (selected == null)
                            selected = new List<List<string>>();
                        selected.Clear();
                        foreach (var item in formSelect.listNodes)
                        //for (int i = 0; i < formSelect.listNodes.Count; i++)
                            selected.Add(item.Key.setStruct);

                        if(textBox1.Text!=null && textBox1.Text.Length>0)
                            button3.Enabled = true;

                        label4.Text = "Number of selected clusters: " + formSelect.listNodes.Count;
                    }
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            int good = 0;
            int all = 0;
            Dictionary<string, int> classNum = new Dictionary<string, int>();

            ReadClassFile(textBox1.Text);
            dataGridView1.Rows.Clear();

            dataGridView1.Rows.Add(selected.Count);

            foreach (var item in selected)
            {
                double acc = 0;
                classNum.Clear();
                foreach (var it in item)
                {
                    if (!classDef.ContainsKey(it))
                        continue;
                    if (!classNum.ContainsKey(classDef[it]))
                        classNum.Add(classDef[it], 0);

                    classNum[classDef[it]]++;
                }
                if (classNum.Count > 0)
                {
                    var orderdCl = classNum.OrderByDescending(j => j.Value);
                    int sum = 0;
                    foreach (var it in classNum.Keys)
                        sum += classNum[it];
                    good += orderdCl.First().Value;
                    all += sum;
                    if (sum > 0)
                        acc = ((double)orderdCl.First().Value) / sum * 100;

                    dataGridView1.Rows[i].Cells[0].Value = item.Count;
                    dataGridView1.Rows[i++].Cells[1].Value = String.Format("{0:0.00}", acc);
                }
            }
            double allAcc = 0;

            allAcc = ((double)good )/ all;

            label3.Text = String.Format("{0:0.00}", allAcc);
        }
        private void ReadClassFile(string fileName)
        {
            try
            {
                classDef.Clear();
                StreamReader r = new StreamReader(fileName);
                string line = r.ReadLine();

                while (line != null)
                {
                    string[] tmp = line.Split(' ');
                    if (tmp.Length >= 2)
                    {
                        string[] aux = tmp[0].Split(Path.DirectorySeparatorChar);
                        if (!classDef.ContainsKey(aux[aux.Length - 1]))
                            classDef.Add(aux[aux.Length - 1], tmp[1]);
                        
                    }
                    line = r.ReadLine();
                }
                r.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("File reading error: " + ex.Message);
            }

        }
    }
}