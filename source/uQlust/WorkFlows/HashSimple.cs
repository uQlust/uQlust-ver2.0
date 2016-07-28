using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uQlustCore;

namespace WorkFlows
{
    public partial class HashSimple : RpartSimple
    {

        public HashSimple(Form parent,Settings set,ResultWindow results,string fileName=null): base(parent,set,results,fileName)
        {
           // InitializeComponent();
            this.Text = "Hash";
            ShowLabels();
            opt.hash.combine = false;
        }
        public override string ToString()
        {
            return "Hash";
        }
    }
}
