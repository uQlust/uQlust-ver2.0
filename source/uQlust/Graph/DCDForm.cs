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
    public partial class DCDForm : Form
    {
        public DCDFile dcd=new DCDFile();

        public DCDForm()
        {
            InitializeComponent();
        }
        public DCDForm(string dcdF,string pdbF,string tempD)
        {
            InitializeComponent();
            textBox1.Text = dcdF;
            textBox2.Text = pdbF;
            textBox3.Text = tempD;
        }

        private void buttonClick(TextBox t)
        {
            DialogResult res;
            if (t.Text.Length > 0)
                FileDialog.FileName = t.Text;

            res = FileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                t.Text = FileDialog.FileName;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            buttonClick(textBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonClick(textBox2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res;
            if (textBox3.Text.Length > 0)
                folderBrowserDialog1.SelectedPath = textBox3.Text;

            res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            dcd.dcdFile = textBox1.Text;
            dcd.pdbFile = textBox2.Text;
            dcd.tempDir = textBox3.Text;
            this.Close();
        }
    }
}
