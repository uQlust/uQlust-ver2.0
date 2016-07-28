using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graph
{
    public partial class BestView : Form
    {
        Dictionary<string, ViewData> dic;
        public BestView(Dictionary<string, ViewData> dic)
        {
            InitializeComponent();
            this.dic = dic;
            double avr = 0;

            if (dic == null || dic.Count == 0)
                return;

            dataGridView1.Rows.Add(dic.Keys.Count);
            int i = 0;
            foreach (var item in dic)
            {
                dataGridView1.Rows[i].Cells[0].Value = item.Key;
                dataGridView1.Rows[i].Cells[1].Value = item.Value.structures;
                dataGridView1.Rows[i].Cells[2].Value = item.Value.size;
                dataGridView1.Rows[i++].Cells[3].Value = item.Value.distance;
                avr += item.Value.distance;
            }

            avr /= dic.Count;

            dataGridView1.Rows.Add(1);
            dataGridView1.Rows[i].Cells[3].Value = "Avr=" + String.Format("{0:0.00}", avr);
        }
    }
}
