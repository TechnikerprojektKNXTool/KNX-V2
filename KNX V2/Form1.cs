using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public static Raum[] liste;

        
        public Form1()
        {
            InitializeComponent();
            SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            listView1.FullRowSelect = true;
            liste = new Raum[1000];
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
            liste[index] = new Raum();
            liste[index].Funktionen = liste[i].Funktionen;           
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

        //Textdatei speichern
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string path = ofd.FileName;

            StreamWriter writer = new StreamWriter(path);

            int i = 0;
            foreach (Raum raum in liste)
            {
                if (raum != null && raum.Typ != "invalid")
                {
                    foreach (Funktion funktion in raum.Funktionen)
                    {
                        if (funktion != null)
                        {
                            string fkt = funktion.Name + "\t" + funktion.Bedienelement + "\t" + funktion.Verbraucher + "\t" + funktion.Sollwert + "\t" + funktion.Schalten.ToString() + "\t" + funktion.Dimmen.ToString() + "\t" + funktion.Jalousie.ToString() + "\t" + funktion.Kommentar + "\t" + funktion.Art.ToString();
                            writer.WriteLine(i.ToString() + "\t" + raum.Typ + "\t" + raum.Name + "\t" + fkt);
                        }
                    }
                    i++;
                }
            }
            writer.Close();
        }

        //Textdatei einlesen
        private void button8_Click(object sender, EventArgs e)
        {
            Array.Clear(liste, 0, 1000);
            listView1.Items.Clear();

            int j = 0;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string path = ofd.FileName;

            StreamReader reader = new StreamReader(path);

            while (reader.Peek() != -1)
            {
                string[] einlesen = reader.ReadLine().Split('\t');
                int i = Convert.ToInt32(einlesen[0]);

                if (liste[i] == null)
                {
                    liste[i] = new Raum();
                    liste[i].Typ = einlesen[1];
                    liste[i].Name = einlesen[2];

                    ListViewItem listitem = new ListViewItem();
                    listitem.SubItems.Add(einlesen[1]);
                    listitem.SubItems.Add(einlesen[2]);
                    listitem.SubItems.Add(i.ToString());
                    listView1.Items.Add(listitem);

                    j = 0;
                }
                Funktion fkt = new Funktion();
                fkt.Name = einlesen[3];
                fkt.Bedienelement = einlesen[4];
                fkt.Verbraucher = einlesen[5];
                fkt.Sollwert = einlesen[6];
                fkt.Schalten = Convert.ToBoolean(einlesen[7]);
                fkt.Dimmen = Convert.ToBoolean(einlesen [8]);
                fkt.Jalousie = Convert.ToBoolean(einlesen[9]);
                fkt.Kommentar = einlesen[10];
                fkt.Art = Convert.ToInt32(einlesen[11]);
                liste[i].Funktionen[j] = fkt;
                j++;
            }

            reader.Close();
        }
    }
}
