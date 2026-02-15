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
    public partial class Umbenennen : Form
    {
        public string TxtName => txtName.Text;
        public Umbenennen()
        {
            InitializeComponent();
        }

        public Umbenennen(string alt)
        {
            InitializeComponent();
            txtName.Text = alt;
            txtName.Focus();
        }
    }
}
