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
        public static Raum[] list;  // geändert #####

        private List<ComboBox> alleComboBoxen;
        Dictionary<ComboBox, int> comboBoxFixCount;

        public Form2()
        {
            InitializeComponent();
            SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            comboBoxFixCount = new Dictionary<ComboBox, int>();
                       
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


            // Alle ComboBoxen sammeln
            alleComboBoxen = new List<ComboBox>
        {
            comboBox2,
            comboBox3,
            comboBox4,
            comboBox5,
            comboBox6,
            comboBox7,
            comboBox8,
            comboBox9,
            comboBox10,
            comboBox11,
            comboBox12,
            comboBox13,
            comboBox14,
            comboBox15,
            comboBox16,
            comboBox17,
            comboBox18,
            comboBox19,
            comboBox20,

            comboBox22,
            comboBox23,
            comboBox24,
            comboBox25,
            comboBox26,
            comboBox27,

            comboBox29,
            comboBox30,
            comboBox31,
            comboBox32,

            comboBox34,
            comboBox35,
            comboBox36,
            comboBox37,

            comboBox39,
            comboBox40,
            comboBox41,
            comboBox42,
        };
            // Fixe Einträge der Comboboxen zählen und speichern

            foreach (ComboBox cb in alleComboBoxen)
            {
                comboBoxFixCount[cb] = cb.Items.Count;
            }
        }

        private void UpdateComboBoxenDynamisch()
        {
            foreach (ComboBox cb in alleComboBoxen)
            {
                int fixCount = comboBoxFixCount[cb];

                cb.BeginUpdate();

                // Dynamische Einträge entfernen
                while (cb.Items.Count > fixCount)
                {
                    cb.Items.RemoveAt(cb.Items.Count - 1);
                }

                // Neue dynamische Einträge anhängen
                foreach (string eintrag in meineListe)
                {
                    cb.Items.Add(eintrag);
                }

                cb.EndUpdate();
            }
        }

        private List<string> meineListe = new List<string>();

        public Form2(int index) : this()
        {
            this.index = index;
            
            Raum raum = new Raum();
            raum = Form1.liste[index];
            foreach (Funktion fkt in raum.Funktionen)
            {
                if (fkt != null)
                {
                    switch (fkt.Art)
                    {
                        case 1:
                            if (!comboBox1.Items.Contains(fkt.Verbraucher))
                            {
                                comboBox1.Items.Add(fkt.Verbraucher);
                            }
                            break;
                        case 2:
                            if (!comboBox21.Items.Contains(fkt.Verbraucher))
                            {
                                comboBox21.Items.Add(fkt.Verbraucher);
                            }
                            break;
                        case 3:
                            if (!comboBox28.Items.Contains(fkt.Verbraucher))
                            {
                                comboBox28.Items.Add(fkt.Verbraucher);
                            }
                            break;
                        case 4:
                            if (!comboBox33.Items.Contains(fkt.Verbraucher))
                            {
                                comboBox33.Items.Add(fkt.Verbraucher);
                            }
                            break;
                        case 5:
                            if (!comboBox38.Items.Contains(fkt.Verbraucher))
                            {
                                comboBox38.Items.Add(fkt.Verbraucher);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }            
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

        //gibt Index der ersten leeren Stelle im Funktionen-Array zurück
        public int Index()
        {
            int i = 0;
            while (Form1.liste[index].Funktionen[i] != null)
            {
                i++;
            }
            return i;
        }

        //Lichtgruppe speichern
        private void button4_Click(object sender, EventArgs e)
        {
            
            if (comboBox2.Text != "" )
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox2.Text;
                fkt.Sollwert = textBox1.Text;
                fkt.Kommentar = textBox2.Text;
                fkt.Schalten = true;
                fkt.Name = "Licht Ein/Aus";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox3.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox3.Text;
                fkt.Sollwert = textBox3.Text;
                fkt.Kommentar = textBox4.Text;
                fkt.Schalten = true;
                fkt.Name = "Licht Ein/Aus";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox4.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox4.Text;
                fkt.Sollwert = textBox5.Text;
                fkt.Kommentar = textBox6.Text;
                fkt.Schalten = true;
                fkt.Name = "Licht Ein/Aus";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox5.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox5.Text;
                fkt.Sollwert = textBox7.Text;
                fkt.Kommentar = textBox8.Text;
                fkt.Schalten = true;
                fkt.Name = "Licht Ein/Aus";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox6.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox6.Text;
                fkt.Sollwert = textBox9.Text;
                fkt.Kommentar = textBox10.Text;
                fkt.Schalten = true;
                fkt.Name = "Licht Ein/Aus";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox7.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox7.Text;
                fkt.Sollwert = textBox11.Text;
                fkt.Kommentar = textBox12.Text;
                fkt.Dimmen = true;
                fkt.Name = "Licht Dimmen";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox8.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox8.Text;
                fkt.Sollwert = textBox13.Text;
                fkt.Kommentar = textBox14.Text;
                fkt.Dimmen = true;
                fkt.Name = "Licht Dimmen";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox9.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox9.Text;
                fkt.Sollwert = textBox15.Text;
                fkt.Kommentar = textBox16.Text;
                fkt.Dimmen = true;
                fkt.Name = "Licht Dimmen";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox10.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox10.Text;
                fkt.Sollwert = textBox17.Text;
                fkt.Kommentar = textBox18.Text;
                fkt.Dimmen = true;
                fkt.Name = "Licht Dimmen";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox11.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox11.Text;
                fkt.Sollwert = textBox19.Text;
                fkt.Kommentar = textBox20.Text;
                fkt.Dimmen = true;
                fkt.Name = "Licht Dimmen";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox12.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox12.Text;
                fkt.Sollwert = textBox21.Text;
                fkt.Kommentar = textBox22.Text;
                fkt.Name = "Farbtemperatur Einstellen";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox13.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox13.Text;
                fkt.Sollwert = textBox23.Text;
                fkt.Kommentar = textBox24.Text;
                fkt.Name = "Farbtemperatur Einstellen";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox14.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox14.Text;
                fkt.Sollwert = textBox25.Text;
                fkt.Kommentar = textBox26.Text;
                fkt.Name = "Farbtemperatur Einstellen";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox15.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox15.Text;
                fkt.Sollwert = textBox27.Text;
                fkt.Kommentar = textBox28.Text;
                fkt.Name = "RGB";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox16.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox16.Text;
                fkt.Sollwert = textBox29.Text;
                fkt.Kommentar = textBox30.Text;
                fkt.Name = "RGB";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox17.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox17.Text;
                fkt.Sollwert = textBox31.Text;
                fkt.Kommentar = textBox32.Text;
                fkt.Name = "RGB";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox18.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox18.Text;
                fkt.Sollwert = textBox33.Text;
                fkt.Kommentar = textBox34.Text;
                fkt.Name = "Direkt / Indirekt";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox19.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox19.Text;
                fkt.Sollwert = textBox35.Text;
                fkt.Kommentar = textBox36.Text;
                fkt.Name = "Direkt / Indirekt";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox20.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox1.Text;
                fkt.Bedienelement = comboBox20.Text;
                fkt.Sollwert = textBox37.Text;
                fkt.Kommentar = textBox38.Text;
                fkt.Name = "Direkt / Indirekt";
                fkt.Art = 1;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox24.Clear();
            textBox25.Clear();
            textBox26.Clear();
            textBox27.Clear();
            textBox28.Clear();
            textBox29.Clear();
            textBox30.Clear();
            textBox31.Clear();
            textBox32.Clear();
            textBox33.Clear();
            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();
            textBox38.Clear();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
            comboBox11.SelectedIndex = 0;
            comboBox12.SelectedIndex = 0;
            comboBox13.SelectedIndex = 0;
            comboBox14.SelectedIndex = 0;
            comboBox15.SelectedIndex = 0;
            comboBox16.SelectedIndex = 0;
            comboBox17.SelectedIndex = 0;
            comboBox18.SelectedIndex = 0;
            comboBox19.SelectedIndex = 0;
            comboBox20.SelectedIndex = 0;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;

        }


        //Verdunkelung
        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox22.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox21.Text;
                fkt.Bedienelement = comboBox22.Text;
                fkt.Sollwert = textBox39.Text;
                fkt.Kommentar = textBox40.Text;
                fkt.Jalousie = true;
                fkt.Name = comboBox21.Text;
                fkt.Art = 2;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox23.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox21.Text;
                fkt.Bedienelement = comboBox23.Text;
                fkt.Sollwert = textBox41.Text;
                fkt.Kommentar = textBox42.Text;
                fkt.Jalousie = true;
                fkt.Name = comboBox21.Text;
                fkt.Art = 2;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox24.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox21.Text;
                fkt.Bedienelement = comboBox24.Text;
                fkt.Sollwert = textBox43.Text;
                fkt.Kommentar = textBox44.Text;
                fkt.Jalousie = true;
                fkt.Name = comboBox21.Text;
                fkt.Art = 2;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox25.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox21.Text;
                fkt.Bedienelement = comboBox25.Text;
                fkt.Sollwert = textBox45.Text;
                fkt.Kommentar = textBox46.Text;
                fkt.Jalousie = true;
                fkt.Name = comboBox21.Text;
                fkt.Art = 2;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox26.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox21.Text;
                fkt.Bedienelement = comboBox26.Text;
                fkt.Sollwert = textBox47.Text;
                fkt.Kommentar = textBox48.Text;
                fkt.Jalousie = true;
                fkt.Name = comboBox21.Text;
                fkt.Art = 2;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox27.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox21.Text;
                fkt.Bedienelement = comboBox27.Text;
                fkt.Sollwert = textBox49.Text;
                fkt.Kommentar = textBox50.Text;
                fkt.Jalousie = true;
                fkt.Name = comboBox21.Text;
                fkt.Art = 2;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            textBox39.Clear();
            textBox40.Clear();
            textBox41.Clear();
            textBox42.Clear();
            textBox43.Clear();
            textBox44.Clear();
            textBox45.Clear();
            textBox46.Clear();
            textBox47.Clear();
            textBox48.Clear();
            textBox49.Clear();
            textBox50.Clear();

            comboBox21.SelectedIndex = 0;
            comboBox22.SelectedIndex = 0;
            comboBox23.SelectedIndex = 0;
            comboBox24.SelectedIndex = 0;
            comboBox25.SelectedIndex = 0;
            comboBox26.SelectedIndex = 0;
            comboBox27.SelectedIndex = 0;
        }


        //Oberlichter
        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox29.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox28.Text;
                fkt.Bedienelement = comboBox29.Text;
                fkt.Sollwert = textBox51.Text;
                fkt.Kommentar = textBox52.Text;                
                fkt.Name = "Oberlicht";
                fkt.Art = 3;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox30.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox28.Text;
                fkt.Bedienelement = comboBox30.Text;
                fkt.Sollwert = textBox53.Text;
                fkt.Kommentar = textBox54.Text;
                fkt.Name = "Oberlicht";
                fkt.Art = 3;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox31.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox28.Text;
                fkt.Bedienelement = comboBox31.Text;
                fkt.Sollwert = textBox55.Text;
                fkt.Kommentar = textBox56.Text;
                fkt.Name = "Oberlicht";
                fkt.Art = 3;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox32.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox28.Text;
                fkt.Bedienelement = comboBox32.Text;
                fkt.Sollwert = textBox57.Text;
                fkt.Kommentar = textBox58.Text;
                fkt.Name = "Oberlicht";
                fkt.Art = 3;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            textBox51.Clear();
            textBox52.Clear();
            textBox53.Clear();
            textBox54.Clear();
            textBox55.Clear();
            textBox56.Clear();
            textBox57.Clear();
            textBox58.Clear();

            comboBox28.SelectedIndex = 0;
            comboBox29.SelectedIndex = 0;
            comboBox30.SelectedIndex = 0;
            comboBox31.SelectedIndex = 0;
            comboBox32.SelectedIndex = 0;            

        }


        //Heizgruppen
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox34.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox33.Text;
                fkt.Bedienelement = comboBox34.Text;
                fkt.Sollwert = textBox59.Text;
                fkt.Kommentar = textBox60.Text;
                fkt.Name = "Heizen";
                fkt.Art = 4;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox35.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox33.Text;
                fkt.Bedienelement = comboBox35.Text;
                fkt.Sollwert = textBox61.Text;
                fkt.Kommentar = textBox62.Text;
                fkt.Name = "Heizen";
                fkt.Art = 4;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox36.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox33.Text;
                fkt.Bedienelement = comboBox36.Text;
                fkt.Sollwert = textBox63.Text;
                fkt.Kommentar = textBox64.Text;
                fkt.Name = "Heizen";
                fkt.Art = 4;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox37.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox33.Text;
                fkt.Bedienelement = comboBox37.Text;
                fkt.Sollwert = textBox65.Text;
                fkt.Kommentar = textBox66.Text;
                fkt.Name = "Heizen";
                fkt.Art = 4;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            textBox59.Clear();
            textBox60.Clear();
            textBox61.Clear();
            textBox62.Clear();
            textBox63.Clear();
            textBox64.Clear();
            textBox65.Clear();
            textBox66.Clear();

            comboBox33.SelectedIndex = 0;
            comboBox34.SelectedIndex = 0;
            comboBox35.SelectedIndex = 0;
            comboBox36.SelectedIndex = 0;
            comboBox37.SelectedIndex = 0;
        }


        //Steckdosen
        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox39.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox38.Text;
                fkt.Bedienelement = comboBox39.Text;
                fkt.Sollwert = textBox67.Text;
                fkt.Kommentar = textBox68.Text;
                fkt.Schalten = true;
                fkt.Name = "Steckdosen Schalten";
                fkt.Art = 5;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox40.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox38.Text;
                fkt.Bedienelement = comboBox40.Text;
                fkt.Sollwert = textBox69.Text;
                fkt.Kommentar = textBox70.Text;
                fkt.Schalten = true;
                fkt.Name = "Steckdosen Schalten";
                fkt.Art = 5;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox41.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox38.Text;
                fkt.Bedienelement = comboBox41.Text;
                fkt.Sollwert = textBox71.Text;
                fkt.Kommentar = textBox72.Text;
                fkt.Schalten = true;
                fkt.Name = "Steckdosen Schalten";
                fkt.Art = 5;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            if (comboBox42.Text != "")
            {
                int i = Index();
                Funktion fkt = new Funktion();
                fkt.Verbraucher = comboBox38.Text;
                fkt.Bedienelement = comboBox42.Text;
                fkt.Sollwert = textBox73.Text;
                fkt.Kommentar = textBox74.Text;
                fkt.Schalten = true;
                fkt.Name = "Steckdosen Schalten";
                fkt.Art = 5;

                Form1.liste[index].Funktionen[i] = fkt;
            }

            textBox67.Clear();
            textBox68.Clear();
            textBox69.Clear();
            textBox70.Clear();
            textBox71.Clear();
            textBox72.Clear();
            textBox73.Clear();
            textBox74.Clear();

            comboBox38.SelectedIndex = 0;
            comboBox39.SelectedIndex = 0;
            comboBox40.SelectedIndex = 0;
            comboBox41.SelectedIndex = 0;
            comboBox42.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            // Schaltstelle hinzufügen 

          
            string text = textBox76.Text.Trim();

            if (text == "")
                return; // nichts einfügen, wenn Text leer ist
           
            meineListe.Add(text);   // Text in die Liste schreiben
            listView1.Items.Add(text);
            UpdateComboBoxenDynamisch(); // neu
            textBox76.Clear();    // Textbox leeren
                      
        }
       


        private void button10_Click_1(object sender, EventArgs e)
        {
            // ListView Eintrag in TextBox laden für Änderung oder nur zum kopieren

            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Bitte einen Eintrag auswählen.");
                return;
            }

            ListViewItem item = listView1.SelectedItems[0];

            // Text aus ListView in TextBox schreiben
            textBox76.Text = item.Text;
        }


        private void button11_Click(object sender, EventArgs e)
        {
            // Ausgwählten Listview Eintrag löschen

            // Prüfen ob ein Eintrag ausgewählt ist
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Bitte einen Eintrag auswählen.");
                return;
            }

            // Optional: Sicherheitsabfrage
            DialogResult result = MessageBox.Show(
                "Möchten Sie den ausgewählten Eintrag wirklich löschen?",
                "Eintrag löschen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;
                       
            int index = listView1.SelectedItems[0].Index;

            meineListe.RemoveAt(index);
            listView1.Items.RemoveAt(index);

            UpdateComboBoxenDynamisch();
            textBox76.Clear();
        }
              
        private void button12_Click(object sender, EventArgs e)
        {
            // Änderungen speichern

            // Prüfen ob ein Eintrag ausgewählt ist
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Bitte einen Eintrag auswählen.");
                return;
            }

            string neuerText = textBox76.Text.Trim();

            if (neuerText == "")
            {
                MessageBox.Show("Text darf nicht leer sein.");
                return;
            }

            int index = listView1.SelectedItems[0].Index;

            meineListe[index] = neuerText;
            listView1.Items[index].Text = neuerText;

            // ComboBoxen updaten nach Änderung
            UpdateComboBoxenDynamisch();

            textBox76.Clear();
        }

        
    }
}
