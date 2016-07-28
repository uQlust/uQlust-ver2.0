using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using uQlustCore;
using uQlustCore.Interface;

namespace Graph
{
    public partial class FormText : Form,IVisual
    {
        public ClosingForm closeForm;
        string winName;

        public FormText(List<KeyValuePair<string,double>> lista,string itemL)
        {
            InitializeComponent();
            splitContainer1.Panel1Collapsed = true;
            label2.Text = lista.Count.ToString();
            int size=0;
            foreach (var item in lista)
                size += (item.Key + "\t" + item.Value + "\n").Length;
            StringBuilder st = new StringBuilder(size);

            foreach (var item in lista)
            {
                st.AppendLine(item.Key+"\t"+item.Value);
            }
            richTextBox1.Text = st.ToString() ;
            
            winName = itemL;
            this.Text = itemL;
        }
        public override string ToString()
        {
            return "Text";
        }
        public void ToFront()
        {
            this.BringToFront();
        }
        public FormText(HClusterNode node)
        {
            InitializeComponent();
            
            label2.Text = node.setStruct.Count.ToString();
            label4.Text = node.refStructure;
            label6.Text = node.realDist.ToString();
            if (node.joined != null)
                label8.Text = node.joined.Count.ToString();
            else
                label8.Text = "0";

            int size = 0;
            foreach (var item in node.setStruct)
                size += item.Length;

            StringBuilder st = new StringBuilder(size);

            foreach (var item in node.setStruct)
            {
                st.AppendLine(item);
            }
            richTextBox1.Text = st.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res=saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                StreamWriter ww = new StreamWriter(fileName);
                ww.Write(richTextBox1.Text);
                ww.Close();
                
            }
        }

        private void FormText_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(closeForm!=null)
                closeForm(winName);
        }
    }
}
