using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace KNX_V2
{
    public partial class RaumDialog : Form
    {
        public string RaumTyp => txtTyp.Text;
        public string RaumName => txtName.Text;

        public RaumDialog(string typ, string name)
        {
            InitializeComponent();
            txtTyp.Text = typ;
            txtName.Text = name;
        }
    }
}

