using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


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
            //hauptarray, in dem die Räume abgespeichert werden
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
            foreach (string item in liste[i].Schaltstellen) 
            {
                liste[index].Schaltstellen.Add(item);
            }

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
            try
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
                        writer.WriteLine(i.ToString() + "\t" + raum.Typ + "\t" + raum.Name + "\tSchaltstellenliste\t " + string.Join("\t", raum.Schaltstellen));
                        bool leer = true;
                        foreach (Funktion funktion in raum.Funktionen)
                        {
                            if (funktion != null)
                            {
                                string fkt = funktion.Name + "\t" + funktion.Bedienelement + "\t" + funktion.Verbraucher + "\t" + funktion.Sollwert + "\t" + funktion.Schalten.ToString() + "\t" + funktion.Dimmen.ToString() + "\t" + funktion.Jalousie.ToString() + "\t" + funktion.Kommentar + "\t" + funktion.Art.ToString() + "\t" + funktion.ComboNr.ToString();
                                writer.WriteLine(i.ToString() + "\t" + raum.Typ + "\t" + raum.Name + "\t" + fkt);
                                leer = false;
                            }
                        }
                        if (leer)
                        {
                            //writer.WriteLine(i.ToString() + "\t" + raum.Typ + "\t" + raum.Name + "\t \tleer\t \t \t \t \t \t \t \t ");
                        }

                        i++;
                    }
                }
                writer.Close();
            }
            catch { }
        }

        //Textdatei einlesen
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int j = 0;
                int indx = -1;

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                string path = ofd.FileName;

                StreamReader reader = new StreamReader(path);                

                Array.Clear(liste, 0, 1000);
                listView1.Items.Clear();

                while (reader.Peek() != -1)
                {
                    string[] einlesen = reader.ReadLine().Split('\t');
                    indx = Convert.ToInt32(einlesen[0]);

                    if (liste[indx] == null)
                    {
                        liste[indx] = new Raum();
                        liste[indx].Typ = einlesen[1];
                        liste[indx].Name = einlesen[2];

                        ListViewItem listitem = new ListViewItem();
                        listitem.SubItems.Add(einlesen[1]);
                        listitem.SubItems.Add(einlesen[2]);
                        listitem.SubItems.Add(indx.ToString());
                        listView1.Items.Add(listitem);

                        j = 0;
                    }

                    //Schaltstellenlsite einlesen
                    if (einlesen[3] == "Schaltstellenliste" && einlesen[4] != null)
                    {
                        for (int i = 4; i < einlesen.Length; i++)
                        {
                            liste[indx].Schaltstellen.Add(einlesen[i]);
                        }
                    }

                    //if (einlesen[4] != "leer")
                    if (einlesen[3] != "Schaltstellenliste")
                    {
                        Funktion fkt = new Funktion();
                        fkt.Name = einlesen[3];
                        fkt.Bedienelement = einlesen[4];
                        fkt.Verbraucher = einlesen[5];
                        fkt.Sollwert = einlesen[6];
                        fkt.Schalten = Convert.ToBoolean(einlesen[7]);
                        fkt.Dimmen = Convert.ToBoolean(einlesen[8]);
                        fkt.Jalousie = Convert.ToBoolean(einlesen[9]);
                        fkt.Kommentar = einlesen[10];
                        fkt.Art = Convert.ToInt32(einlesen[11]);
                        fkt.ComboNr = Convert.ToInt32(einlesen[12]);
                        liste[indx].Funktionen[j] = fkt;
                        j++;
                    }
                }

                reader.Close();

                index = indx + 1;
            }
            catch { }
        }

        //Excel-Tabelle erstellen
        private void button7_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            //Start Excel and get Application object.
            oXL = new Excel.Application();
            oXL.Visible = true;

            //Get a new workbook.
            oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            //Add table headers going cell by cell.
            oSheet.Cells[1, 1] = "Raumtyp";
            oSheet.Cells[1, 2] = "Raumname";
            oSheet.Cells[1, 3] = "Verbraucher";
            oSheet.Cells[1, 4] = "Funktion";
            oSheet.Cells[1, 5] = "Bedienelement";
            oSheet.Cells[1, 6] = "Sollwert";
            oSheet.Cells[1, 7] = "Schalten";
            oSheet.Cells[1, 8] = "Dimmen";
            oSheet.Cells[1, 9] = "Jalousie";
            oSheet.Cells[1, 10] = "Kommentar";

            //Format A1:J1 as bold, vertical alignment = center.
            oSheet.get_Range("A1", "J1").Font.Bold = true;
            oSheet.get_Range("A1", "J1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            //string-Array für Excel-Ausgabe
            int l = Listenlaenge();
            string[,] xclAusgabe = new string[l, 10];

            int i = -1;
            string typAlt = "";
            
            foreach (Raum raum in liste)
            {
                if (raum != null && raum.Typ != "invalid" && raum.Funktionen[0] != null)
                {
                    // Wenn neuer Raumtyp, dann leere Zeile (ist noch nicht schön so)
                    string typNeu = raum.Typ;
                    if (typNeu != typAlt)
                    {
                        i++;
                    }
                    typAlt = raum.Typ;
                    foreach (Funktion funktion in raum.Funktionen)
                    {
                        if (funktion != null)
                        {
                            xclAusgabe[i, 0] = raum.Typ;
                            xclAusgabe[i, 1] = raum.Name;
                            xclAusgabe[i, 2] = funktion.Verbraucher;
                            xclAusgabe[i, 3] = funktion.Name;
                            xclAusgabe[i, 4] = funktion.Bedienelement;
                            xclAusgabe[i, 5] = funktion.Sollwert;
                            if (funktion.Schalten)
                            {
                                xclAusgabe[i, 6] = "X";
                            }
                            else { xclAusgabe[i, 6] = ""; }
                            if (funktion.Dimmen)
                            {
                                xclAusgabe[i, 7] = "X";
                            }
                            else { xclAusgabe[i, 7] = ""; }
                            if (funktion.Jalousie)
                            {
                                xclAusgabe[i, 8] = "X";
                            }
                            else { xclAusgabe[i, 8] = ""; }
                            xclAusgabe[i, 9] = funktion.Kommentar;

                            i++;                  
                        }
                    }

                    i++;
                    
                }
            }


            //Fill A2:B6 with an array of values 
            string b = "J" + (l+2).ToString();
            oSheet.get_Range("A3", b).Value2 = xclAusgabe;

            //mittig ausrichten
            oSheet.get_Range("A1", b).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            oSheet.get_Range("A1", b).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

            //AutoFit columns A:J.
            //oRng = oSheet.get_Range("A1", "J1");
            //oRng.EntireColumn.AutoFit();
            oRng = oSheet.get_Range("A1", "A" + (l+2).ToString());
            oRng.EntireColumn.AutoFit();
            oRng = oSheet.get_Range("B1", "B" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 21;
            oRng = oSheet.get_Range("C1", "C" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 26;
            oRng = oSheet.get_Range("D1", "D" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 26;
            oRng = oSheet.get_Range("E1", "E" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 26;
            oRng = oSheet.get_Range("F1", "F" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 21;
            oRng = oSheet.get_Range("G1", "G" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 8;
            oRng = oSheet.get_Range("H1", "H" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 8;
            oRng = oSheet.get_Range("I1", "I" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 8;
            oRng = oSheet.get_Range("J1", "J" + (l + 2).ToString());
            oRng.EntireColumn.ColumnWidth = 32;


            //Make sure Excel is visible and give the user control
            //of Microsoft Excel's lifetime.
            oXL.Visible = true;
            oXL.UserControl = true;


        }

        public int Listenlaenge ()
        {
            int l = 0;
            for (int i = 0; i < liste.Length; i++)
            {
                if (liste[i] != null && liste[i].Funktionen[0] != null)
                {
                    l++;
                    foreach (Funktion funktion in liste[i].Funktionen)
                    {
                        if (funktion != null)
                        {
                            l++;
                        }
                    }
                    if (liste[i+1] != null && i+1 < liste.Length && liste[i].Typ != liste[i+1].Typ)
                    {
                        l++;
                    }
                }
            }

            return l;
        }

        //Funktionen-Vorschau anzeigen, wenn eine Raum ausgewählt wird
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
           
            ListViewItem lvi = listView1.SelectedItems[0];
           
            int j = Convert.ToInt32(lvi.SubItems[3].Text);
            listView2.Items.Clear();
            foreach(Funktion fkt in liste[j].Funktionen)
            {
                if (fkt != null)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.SubItems.Add(fkt.Verbraucher);
                    lvitem.SubItems.Add(fkt.Name);
                    lvitem.SubItems.Add(fkt.Bedienelement);
                    lvitem.SubItems.Add(fkt.Sollwert);
                    lvitem.SubItems.Add(fkt.Kommentar);
                    listView2.Items.Add(lvitem);
                }
            }
            
        }
    }
}
