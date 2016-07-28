using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using uQlustCore;
using uQlustCore.Interface;

namespace Graph
{
    public delegate void ClosingForm(string item);

    public partial class ListVisual : Form,IVisual
    {
        List<List<string>> clusters;
        TextInput input = null;

        public ClosingForm closeForm=null;

        public ListVisual(List<List<string>> clusters,string item)
        {
            InitializeComponent();
            this.clusters = clusters;
            for (int i = 1; i <= clusters.Count; i++)                
                listBox1.Items.Add("Cluster_" + i+" "+clusters[i-1].Count);
            this.Text = item;

        }
        public override string ToString()
        {
            return "List";
        }
        public void ToFront()
        {
            this.BringToFront();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text="";
            if (listBox1.SelectedIndex == -1)
                return;
            int size = 0;
            for (int i = 0; i < clusters[listBox1.SelectedIndex].Count; i++)
                size += clusters[listBox1.SelectedIndex][i].Length;

            StringBuilder st = new StringBuilder(size);
            for (int i = 0; i < clusters[listBox1.SelectedIndex].Count; i++)
                st.AppendLine(clusters[listBox1.SelectedIndex][i]);
            richTextBox1.Text=st.ToString();


        }

        private void BakerVisual_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeForm != null)
                closeForm(this.Text);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.F:
                    {
                        if (input == null)
                        {
                            input = new TextInput("NEXT");
                            input.textBox = richTextBox1;
                            input.Show();
                            
                        }
                        input.BringToFront();
                        
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
