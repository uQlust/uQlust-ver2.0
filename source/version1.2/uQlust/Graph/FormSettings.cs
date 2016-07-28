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

namespace Graph
{
    public partial class FormSettings : Form
    {
        Settings set=new Settings();
        public FormSettings(Settings set)
        {
            this.set = set;
            InitializeComponent();


            foreach (var item in Enum.GetValues(typeof(INPUTMODE)))
                comboBox1.Items.Add(Enum.GetName(typeof(INPUTMODE), item));

            comboBox1.SelectedItem = Enum.GetName(typeof(INPUTMODE), set.mode);
            extensionFile.Text = set.extension;
            textBox1.Text = set.profilesDir;
        }
        public FormSettings(bool flag)
        {
            InitializeComponent();

            foreach (var item in Enum.GetValues(typeof(INPUTMODE)))
                comboBox1.Items.Add(Enum.GetName(typeof(INPUTMODE), item));

           
            try
            {
                set.Load();
                extensionFile.Text = set.extension;
                textBox1.Text = set.profilesDir;
                numericUpDown1.Value = set.numberOfCores;
                comboBox1.SelectedItem = Enum.GetName(typeof(INPUTMODE), set.mode);

            }
            catch
            {
                cancelBtn.Enabled = false;               
                extensionFile.Text = "*";
                numericUpDown1.Value = 1;
            }

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {        
            set.extension = extensionFile.Text;
            set.profilesDir = textBox1.Text;
            set.numberOfCores = (int)numericUpDown1.Value;
            set.mode = (INPUTMODE)Enum.Parse(typeof(INPUTMODE), comboBox1.SelectedItem.ToString());
            try
            {
                set.Save();
            }
            catch
            {
                MessageBox.Show("Cannot save setting file!");
            }
            this.Close();
        }

        private string ButtonClick(string dir)
        {
            DialogResult res;
            res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath.Length > 0)
                {
                    return folderBrowserDialog1.SelectedPath;
                }
            }
            return dir;

        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

     
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ButtonClick(textBox1.Text);
            if (textBox1.Text.Length > 0 && Directory.Exists(textBox1.Text))
                saveBtn.Enabled = true;

        }
    }
}
