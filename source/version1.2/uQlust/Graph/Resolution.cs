using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public partial class Resolution : Form
    {
        int width, height;
        public int WidthR
        {
            get { return width;}
        }
        public int HeightR
        {
            get { return height; }
        }
        
        public Resolution(int Width,int Height)
        {
            InitializeComponent();
            width = Width;
            height = Height;
            numericUpDown1.Value = width;
            numericUpDown2.Value = height;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            width = (int)numericUpDown1.Value;
            height = (int)numericUpDown2.Value;
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
