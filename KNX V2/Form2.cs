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

            //Checkboxen werden erst angewählt und gleich wieder abgewählt um die Textboxen, labels, etc auszugrauen wenn Form2 aufgerufen wird!
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
        }

        public Form2(int index) : this()
        {
            this.index = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {   //zurück Button
            if (tabControl1.SelectedIndex > 0) 
            {
                tabControl1.SelectedIndex--;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {   //vor Button
            if (tabControl1.SelectedIndex < tabControl1.TabCount - 1)
            {
                tabControl1.SelectedIndex++;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool aktiv = checkBox1.Checked;
            label2.Enabled = aktiv;
            label3.Enabled = aktiv;
            label4.Enabled = aktiv;
            comboBox2.Enabled = aktiv;
            textBox1.Enabled = aktiv;
            textBox2.Enabled = aktiv;

            label5.Enabled = aktiv;
            label6.Enabled = aktiv;
            comboBox3.Enabled = aktiv;
            textBox3.Enabled = aktiv;
            textBox4.Enabled = aktiv;

            label7.Enabled = aktiv;
            label8.Enabled = aktiv;
            comboBox4.Enabled = aktiv;
            textBox5.Enabled = aktiv;
            textBox6.Enabled = aktiv;

            label9.Enabled = aktiv;
            label10.Enabled = aktiv;
            comboBox5.Enabled = aktiv;
            textBox7.Enabled = aktiv;
            textBox8.Enabled = aktiv;

            label11.Enabled = aktiv;
            label12.Enabled = aktiv;
            comboBox6.Enabled = aktiv;
            textBox9.Enabled = aktiv;
            textBox10.Enabled = aktiv;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool aktiv = checkBox2.Checked;
            label13.Enabled = aktiv;
            label14.Enabled = aktiv;
            label15.Enabled = aktiv;
            comboBox7.Enabled = aktiv;
            textBox11.Enabled = aktiv;
            textBox12.Enabled = aktiv;

            label16.Enabled = aktiv;
            label17.Enabled = aktiv;
            comboBox8.Enabled = aktiv;
            textBox13.Enabled = aktiv;
            textBox14.Enabled = aktiv;

            label18.Enabled = aktiv;
            label19.Enabled = aktiv;
            comboBox9.Enabled = aktiv;
            textBox15.Enabled = aktiv;
            textBox16.Enabled = aktiv;

            label20.Enabled = aktiv;
            label21.Enabled = aktiv;
            comboBox10.Enabled = aktiv;
            textBox17.Enabled = aktiv;
            textBox18.Enabled = aktiv;

            label22.Enabled = aktiv;
            label23.Enabled = aktiv;
            comboBox11.Enabled = aktiv;
            textBox19.Enabled = aktiv;
            textBox20.Enabled = aktiv;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            bool aktiv = checkBox3.Checked;
            label24.Enabled = aktiv;
            label25.Enabled = aktiv;
            label26.Enabled = aktiv;
            comboBox12.Enabled = aktiv;
            textBox21.Enabled = aktiv;
            textBox22.Enabled = aktiv;

            label27.Enabled = aktiv;
            label28.Enabled = aktiv;
            comboBox13.Enabled = aktiv;
            textBox23.Enabled = aktiv;
            textBox24.Enabled = aktiv;

            label29.Enabled = aktiv;
            label30.Enabled = aktiv;
            comboBox14.Enabled = aktiv;
            textBox25.Enabled = aktiv;
            textBox26.Enabled = aktiv;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            bool aktiv = checkBox4.Checked;
            label31.Enabled = aktiv;
            label32.Enabled = aktiv;
            label33.Enabled = aktiv;
            comboBox15.Enabled = aktiv;
            textBox27.Enabled = aktiv;
            textBox28.Enabled = aktiv;

            label34.Enabled = aktiv;
            label35.Enabled = aktiv;
            comboBox16.Enabled = aktiv;
            textBox29.Enabled = aktiv;
            textBox30.Enabled = aktiv;

            label36.Enabled = aktiv;
            label37.Enabled = aktiv;
            comboBox17.Enabled = aktiv;
            textBox31.Enabled = aktiv;
            textBox32.Enabled = aktiv;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            bool aktiv = checkBox5.Checked;
            label38.Enabled = aktiv;
            label39.Enabled = aktiv;
            label40.Enabled = aktiv;
            comboBox18.Enabled = aktiv;
            textBox33.Enabled = aktiv;
            textBox34.Enabled = aktiv;

            label41.Enabled = aktiv;
            label42.Enabled = aktiv;
            comboBox19.Enabled = aktiv;
            textBox35.Enabled = aktiv;
            textBox36.Enabled = aktiv;

            label43.Enabled = aktiv;
            label44.Enabled = aktiv;
            comboBox20.Enabled = aktiv;
            textBox37.Enabled = aktiv;
            textBox38.Enabled = aktiv;
        }

       
    }
}
