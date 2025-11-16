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
    public partial class Form1 : Form
    {
        private Form2 form2;
        int index = 0;
        Raum[] liste = new Raum[1000];

        public Form1()
        {
            InitializeComponent();
            SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            listView1.FullRowSelect = true;
        }

        //liest Raumtyp und -namen ein und speichert an der nächsten Stelle
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                //speichern in Array
                liste[index] = new Raum();
                liste[index].Typ = textBox1.Text;
                liste[index].Name = textBox2.Text;

                //anzeigen in ListView
                ListViewItem listItem = new ListViewItem();
                listItem.SubItems.Add(textBox1.Text);
                listItem.SubItems.Add(textBox2.Text);
                listItem.SubItems.Add(index.ToString());
                listView1.Items.Add(listItem);


                index++;
            }
        }
         
        //bestehenden Raum kopieren
        private void button2_Click_1(object sender, EventArgs e)
        {
            //Array-Index des ausgewählten Raums auslesen
            ListViewItem lvi = listView1.SelectedItems[0];
            int i = Convert.ToInt32(lvi.SubItems[3].Text);

            // Raum kopieren und neuen Typ und Name aktualisieren           
            liste[index] = liste[i];
            liste[index].Typ = textBox1.Text;
            liste[index].Name = textBox2.Text;

            //anzeigen in ListView
            ListViewItem listItem = new ListViewItem();
            listItem.SubItems.Add(textBox1.Text);
            listItem.SubItems.Add(textBox2.Text);
            listItem.SubItems.Add(index.ToString());
            listView1.Items.Add(listItem);

            index++;

        }

        //Raum konfigurieren -> Form2 öffnen
        private void button3_Click(object sender, EventArgs e)
        {
            //Array-Index des ausgewählten Raums auslesen
            ListViewItem lvi = listView1.SelectedItems[0];
            int j = Convert.ToInt32(lvi.SubItems[3].Text);

            form2 = new Form2(j);
            form2.Show();
        }

        //Eintrag bearbeiten
        private void button4_Click(object sender, EventArgs e)
        {
            //Array-Index des ausgewählten Raums auslesen
            ListViewItem lvi = listView1.SelectedItems[0];
            int i = Convert.ToInt32(lvi.SubItems[3].Text);

            //Namen im Array ändern
            liste[i].Typ = textBox1.Text;
            liste[i].Name = textBox2.Text;

            //Namen in ListView ändern
            lvi.SubItems[1].Text = textBox1.Text;
            lvi.SubItems[2].Text = textBox2.Text;
        }

        //Eintrag löschen
        private void button5_Click(object sender, EventArgs e)
        {
            //Array-Index des ausgewählten Raums auslesen
            ListViewItem lvi = listView1.SelectedItems[0];
            int j = Convert.ToInt32(lvi.SubItems[3].Text);
            lvi.Remove();

            liste[j].Typ = "invalid";

            /*for (int i = j; i < liste.Length - 1; i++)
            {
                liste[i] = liste[i + 1];
            }
            index--; */
        }
    }
}
