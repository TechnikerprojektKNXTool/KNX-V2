using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KNX_V2
{
    public partial class Form2 : Form
    {
        int index;
        public Form2()
        {
            InitializeComponent();
            SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        public Form2(int index) : this()
        {
            this.index = index;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
