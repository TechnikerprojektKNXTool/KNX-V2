using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace KNX_V2
{
    public partial class Form2 : Form
    {
        int index;
        public static Raum[] list;  // geändert #####

        private List<ComboBox> alleComboBoxen;
        Dictionary<ComboBox, int> comboBoxFixCount;
        private List<string> meineListe = new List<string>();
        private ListViewItem editingItem = null;

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

        

        public Form2(int index) : this()
        {
            this.index = index;
            
            Raum raum = new Raum();
            raum = Form1.liste[index];
            label92.Text = "Raum: " + raum.Name + "\n" + "Typ: " + raum.Typ;

            meineListe = raum.Schaltstellen;
            foreach (string item in meineListe)
            {
                listView1.Items.Add(item);
            }
            UpdateComboBoxenDynamisch();
           
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

               
        private void UpdateComboBoxenDynamisch(int? deletedDynamicIndex = null)
        {
            foreach (ComboBox cb in alleComboBoxen)
            {
                int fixCount = comboBoxFixCount[cb];

                int selectedIndex = cb.SelectedIndex;
                bool hadDynamicSelection = selectedIndex >= fixCount;

                // Dynamischer Index der Auswahl (0-basiert)
                int selectedDynamicIndex = hadDynamicSelection
                    ? selectedIndex - fixCount
                    : -1;

                cb.BeginUpdate();

                // Dynamische Einträge entfernen
                while (cb.Items.Count > fixCount)
                {
                    cb.Items.RemoveAt(cb.Items.Count - 1);
                }

                // Dynamische Einträge neu hinzufügen
                foreach (string eintrag in meineListe)
                {
                    cb.Items.Add(eintrag);
                }

                cb.EndUpdate();

                // FALL 1: Gelöschter Eintrag war ausgewählt → Auswahl aufheben
                if (deletedDynamicIndex.HasValue &&
                    selectedDynamicIndex == deletedDynamicIndex.Value)
                {
                    cb.SelectedIndex = -1; // NICHTS auswählen
                    continue;
                }

                // FALL 2: Auswahl war ein anderer dynamischer Eintrag → wiederherstellen
                if (hadDynamicSelection)
                {
                    int newIndex = fixCount + selectedDynamicIndex;

                    if (newIndex >= fixCount && newIndex < cb.Items.Count)
                    {
                        cb.SelectedIndex = newIndex;
                    }
                    else
                    {
                        cb.SelectedIndex = -1;
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

        // gibt die Stelle der Schaltungsgruppe im Funktionen-Array zurück, wenn es die Schaltungsgruppe noch nicht gibt, gibt die Funktion 999 zurück
        private int StelleInFunktionen(string Schaltungsgruppe)
        {
            int j = 999;
            bool check = false;
            int i = 0;
            while (check == false && i < Form1.liste[index].Funktionen.Length)
            {
                if (Form1.liste[index].Funktionen[i] != null && Form1.liste[index].Funktionen[i].Verbraucher == Schaltungsgruppe)
                {
                    j = i;
                    check = true;
                }
                i++;
            }
            return j;
        }

        // leert alle Text- und Comboboxen
        private void AllesLeeren()
        {
            for (int i = 1; i < 44; i++)
            {
                var combo = Controls.Find("comboBox" + i, true).FirstOrDefault();
                if (combo != null)
                {
                    combo.Text = "";
                }
            }
            for (int i = 1; i < 75; i++)
            {
                var textbox = Controls.Find("textBox" + i, true).FirstOrDefault();
                if (textbox != null)
                {
                    textbox.Text = "";
                }
            }
        }

        private void ComboLeerenVonBis(int a, int b)
        {
            for (int i = a; i < (b + 1); i++)
            {
                var combobox = Controls.Find("comboBox" + i, true).FirstOrDefault();
                if (combobox != null) { combobox.Text = ""; }
            }

        }

        private void TextLeerenVonBis(int a, int b)
        {
            for (int i = a; i < (b + 1); i++)
            {
                var textbox = Controls.Find("textBox" + i, true).FirstOrDefault();
                if (textbox != null) { textbox.Text = ""; }
            }
        }


        //Lichtgruppe speichern
        private void button4_Click(object sender, EventArgs e)
        {

            int stelle = StelleInFunktionen(comboBox1.Text);
            bool neu = false;
            bool check = false;

            if (stelle == 999)
            {
                neu = true;
            }
            else
            {
                //löscht bereits vorhandene Funktionen für diese Schaltgruppe
                while (stelle != 999)
                {
                    Form1.liste[index].Funktionen[stelle] = null;
                    stelle = StelleInFunktionen(comboBox1.Text);
                }
            }

            // speichert die Funktionen der Schaltgruppe neu ab
            for (int i = 2; i < 21; i++)
            {
                var combo = Controls.Find("comboBox" + i, true).FirstOrDefault();
                int a = 2 * (i - 2) + 1;
                int b = a + 1;
                var textbox1 = Controls.Find("textBox" + a, true).FirstOrDefault();
                var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                if (combo.Text != "")
                {
                    int x = Index();
                    Funktion fkt = new Funktion();
                    fkt.Verbraucher = comboBox1.Text;
                    fkt.Bedienelement = combo.Text;
                    fkt.Sollwert = textbox1.Text;
                    fkt.Kommentar = textbox2.Text;
                    if (i <= 6)
                    {
                        fkt.Schalten = true;
                        fkt.Name = "Licht Ein/Aus";
                    }
                    else if (i <= 11)
                    {
                        fkt.Dimmen = true;
                        fkt.Name = "Licht Dimmen";
                    }
                    else if (i <= 14)
                    {
                        fkt.Name = "Farbtemperatur Einstellen";
                    }
                    else if (i <= 17)
                    {
                        fkt.Name = "RGB";
                    }
                    else
                    {
                        fkt.Name = "Direkt / Indirekt";
                    }
                    fkt.Art = 1;
                    fkt.ComboNr = i;

                    Form1.liste[index].Funktionen[x] = fkt;
                    check = true;
                }
            }

            if (neu && check)
            {
                comboBox1.Items.Add(comboBox1.Text);
            }


            AllesLeeren();

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;

        }

        //Lichtgruppe bearbeiten
        private void button14_Click(object sender, EventArgs e)
        {

        }

        //Lichtgruppe löschen
        private void button13_Click(object sender, EventArgs e)
        {

        }


        //Verdunkelung
        private void button5_Click(object sender, EventArgs e)
        {
            int stelle = StelleInFunktionen(comboBox21.Text);
            bool neu = false;
            bool check = false;

            if (stelle == 999)
            {
                neu = true;
            }
            else
            {
                //löscht bereits vorhandene Funktionen für diese Schaltgruppe
                while (stelle != 999)
                {
                    Form1.liste[index].Funktionen[stelle] = null;
                    stelle = StelleInFunktionen(comboBox21.Text);
                }
            }

            for (int i = 22; i < 28; i++)
            {
                var combo = Controls.Find("comboBox" + i, true).FirstOrDefault();

                if (combo.Text != "")
                {
                    int a = 2 * (i - 2) - 1;
                    int b = a + 1;
                    var textbox1 = Controls.Find("textBox" + a, true).FirstOrDefault();
                    var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                    int x = Index();
                    Funktion fkt = new Funktion();
                    fkt.Verbraucher = comboBox21.Text;
                    fkt.Bedienelement = combo.Text;
                    fkt.Sollwert = textbox1.Text;
                    fkt.Kommentar = textbox2.Text;
                    fkt.Jalousie = true;
                    fkt.Name = comboBox21.Text;
                    fkt.Art = 2;
                    fkt.ComboNr = i;

                    Form1.liste[index].Funktionen[x] = fkt;
                    check = true;
                }
            }

            if (neu && check)
            {
                comboBox21.Items.Add(comboBox21.Text);
            }


            AllesLeeren();
        }


        //Oberlichter
        private void button6_Click(object sender, EventArgs e)
        {
            int stelle = StelleInFunktionen(comboBox28.Text);
            bool neu = false;
            bool check = false;

            // Verbraucher gibt es noch nicht
            if (stelle == 999)
            {
                neu = true;
            }
            // Verbraucher gibt es schon / es wurde schon Funktionen für diesen Verbraucher gespeichert
            else
            {
                //löscht bereits vorhandene Funktionen für diese Schaltgruppe
                while (stelle != 999)
                {
                    Form1.liste[index].Funktionen[stelle] = null;
                    stelle = StelleInFunktionen(comboBox28.Text);
                }
            }

            // speichert eingegebene Funktionen im Funktionenarray des Raums ab
            for (int i = 29; i < 33; i++)
            {
                var combo = Controls.Find("comboBox" + i, true).FirstOrDefault();

                if (combo.Text != "")
                {
                    int a = 2 * i - 7;
                    int b = a + 1;
                    var textbox1 = Controls.Find("textBox" + a, true).FirstOrDefault();
                    var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                    int x = Index();
                    Funktion fkt = new Funktion();
                    fkt.Verbraucher = comboBox28.Text;
                    fkt.Bedienelement = combo.Text;
                    fkt.Sollwert = textbox1.Text;
                    fkt.Kommentar = textbox2.Text;
                    fkt.Name = "Oberlicht";
                    fkt.Art = 3;
                    fkt.ComboNr = i;

                    Form1.liste[index].Funktionen[x] = fkt;
                    check = true;
                }
            }

            //fügt Verbraucher zum DropDownMenü hinzu, wenn es ihn noch nicht gibt und mind. 1 Funktion abgespeichert wird
            if (neu && check)
            {
                comboBox28.Items.Add(comboBox28.Text);
            }

            AllesLeeren();

        }


        //Heizgruppen
        private void button7_Click(object sender, EventArgs e)
        {
            int stelle = StelleInFunktionen(comboBox33.Text);
            bool neu = false;
            bool check = false;

            if (stelle == 999)
            {
                neu = true;
            }
            else
            {
                //löscht bereits vorhandene Funktionen für diese Schaltgruppe
                while (stelle != 999)
                {
                    Form1.liste[index].Funktionen[stelle] = null;
                    stelle = StelleInFunktionen(comboBox33.Text);
                }
            }

            for (int i = 34; i < 38; i++)
            {
                var combo = Controls.Find("comboBox" + i, true).FirstOrDefault();

                if (combo.Text != "")
                {
                    int a = 2 * i - 9;
                    int b = a + 1;
                    var textbox1 = Controls.Find("textBox" + a, true).FirstOrDefault();
                    var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                    int x = Index();
                    Funktion fkt = new Funktion();
                    fkt.Verbraucher = comboBox33.Text;
                    fkt.Bedienelement = combo.Text;
                    fkt.Sollwert = textbox1.Text;
                    fkt.Kommentar = textbox2.Text;
                    fkt.Name = "Heizen";
                    fkt.Art = 4;
                    fkt.ComboNr = i;

                    Form1.liste[index].Funktionen[x] = fkt;
                    check = true;
                }
            }

            if (neu && check)
            {
                comboBox33.Items.Add(comboBox33.Text);
            }

            AllesLeeren();
        }


        //Steckdosen 
        private void button8_Click(object sender, EventArgs e)
        {
            int stelle = StelleInFunktionen(comboBox38.Text);
            bool neu = false;
            bool check = false;

            if (stelle == 999)
            {
                neu = true;
            }
            else
            {
                //löscht bereits vorhandene Funktionen für diese Schaltgruppe
                while (stelle != 999)
                {
                    Form1.liste[index].Funktionen[stelle] = null;
                    stelle = StelleInFunktionen(comboBox38.Text);
                }
            }

            for (int i = 39; i < 43; i++)
            {
                var combo = Controls.Find("comboBox" + i, true).FirstOrDefault();

                if (combo.Text != "")
                {
                    int a = 2 * i - 11;
                    int b = a + 1;
                    var textbox1 = Controls.Find("textBox" + a, true).FirstOrDefault();
                    var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                    int x = Index();
                    Funktion fkt = new Funktion();
                    fkt.Verbraucher = comboBox38.Text;
                    fkt.Bedienelement = combo.Text;
                    fkt.Sollwert = textbox1.Text;
                    fkt.Kommentar = textbox2.Text;
                    fkt.Name = "Steckdosen Schalten";
                    fkt.Art = 5;
                    fkt.ComboNr = i;

                    Form1.liste[index].Funktionen[x] = fkt;
                    check = true;
                }
            }

            if (neu && check)
            {
                comboBox38.Items.Add(comboBox38.Text);
            }



            AllesLeeren();
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
            // Wenn noch kein Edit-Modus → Text laden
            if (editingItem == null)
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Bitte einen Eintrag auswählen.");
                    return;
                }

                editingItem = listView1.SelectedItems[0];
                textBox76.Text = editingItem.Text;
                textBox76.Focus();
            }
            // Wenn Edit-Modus aktiv → Text speichern
            else
            {
                if (string.IsNullOrWhiteSpace(textBox76.Text))
                {
                    MessageBox.Show("Text darf nicht leer sein.");
                    return;
                }

                editingItem.Text = textBox76.Text;
                editingItem = null;
            }
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
            //test für merge test2
            // Sicherheitsabfrage
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

            UpdateComboBoxenDynamisch(index);
            textBox76.Clear();
        }
      
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Lichtgruppen, comboBox Auswahl oben
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboLeerenVonBis(2, 20);
            TextLeerenVonBis(1, 38);
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;

            int stelle = StelleInFunktionen(comboBox1.Text);
            if (stelle != 999)
            {
                foreach (Funktion fkt in Form1.liste[index].Funktionen)
                {
                    if (fkt != null && fkt.Art == 1 && fkt.Verbraucher == comboBox1.Text)
                    {
                        var combobox = Controls.Find("comboBox" + fkt.ComboNr, true).FirstOrDefault();
                        if (combobox != null) { combobox.Text = fkt.Bedienelement; }

                        int a = 2 * (fkt.ComboNr - 2) + 1;
                        int b = a + 1;
                        var textbox = Controls.Find("textBox" + a, true).FirstOrDefault();
                        if (textbox != null) { textbox.Text = fkt.Sollwert; }
                        var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                        if (textbox2 != null) { textbox2.Text = fkt.Kommentar; }

                        if (fkt.ComboNr <= 6)
                        {
                            checkBox1.Checked = true;
                        }
                        else if (fkt.ComboNr <= 11)
                        {
                            checkBox2.Checked = true;
                        }
                        else if (fkt.ComboNr <= 14)
                        {
                            checkBox3.Checked = true;
                        }
                        else if (fkt.ComboNr <= 17)
                        {
                            checkBox4.Checked = true;
                        }
                        else if (fkt.ComboNr <= 20)
                        {
                            checkBox5.Checked = true;
                        }
                    }
                }
            }
        }

        //Verdunkelung: Schaltgruppe in oberer ComboBox auswählen
        private void comboBox21_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboLeerenVonBis(22, 27);
            TextLeerenVonBis(39, 50);

            int stelle = StelleInFunktionen(comboBox21.Text);
            if (stelle != 999)
            {
                foreach (Funktion fkt in Form1.liste[index].Funktionen)
                {
                    if (fkt != null && fkt.Verbraucher == comboBox21.Text)
                    {
                        var combobox = Controls.Find("comboBox" + fkt.ComboNr, true).FirstOrDefault();
                        if (combobox != null) { combobox.Text = fkt.Bedienelement; }

                        int a = 2 * (fkt.ComboNr - 2) - 1;
                        int b = a + 1;
                        var textbox = Controls.Find("textBox" + a, true).FirstOrDefault();
                        if (textbox != null) { textbox.Text = fkt.Sollwert; }
                        var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                        if (textbox2 != null) { textbox2.Text = fkt.Kommentar; }
                    }
                }
            }
        }

        //Oberlicht: Schaltgruppe in oberer ComboBox auswählen
        private void comboBox28_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboLeerenVonBis(29, 32);
            TextLeerenVonBis(51, 58);

            int stelle = StelleInFunktionen(comboBox28.Text);
            if (stelle != 999)
            {
                foreach (Funktion fkt in Form1.liste[index].Funktionen)
                {
                    if (fkt != null && fkt.Verbraucher == comboBox28.Text)
                    {
                        var combobox = Controls.Find("comboBox" + fkt.ComboNr, true).FirstOrDefault();
                        if (combobox != null) { combobox.Text = fkt.Bedienelement; }

                        int a = 2 * fkt.ComboNr - 7;
                        int b = a + 1;
                        var textbox = Controls.Find("textBox" + a, true).FirstOrDefault();
                        if (textbox != null) { textbox.Text = fkt.Sollwert; }
                        var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                        if (textbox2 != null) { textbox2.Text = fkt.Kommentar; }
                    }
                }
            }
        }

        //Heizung: Schaltgruppe in oberer ComboBox auswählen
        private void comboBox33_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboLeerenVonBis(34, 37);
            TextLeerenVonBis(59, 66);

            int stelle = StelleInFunktionen(comboBox33.Text);
            if (stelle != 999)
            {
                foreach (Funktion fkt in Form1.liste[index].Funktionen)
                {
                    if (fkt != null && fkt.Verbraucher == comboBox33.Text)
                    {
                        var combobox = Controls.Find("comboBox" + fkt.ComboNr, true).FirstOrDefault();
                        if (combobox != null) { combobox.Text = fkt.Bedienelement; }

                        int a = 2 * fkt.ComboNr - 9;
                        int b = a + 1;
                        var textbox = Controls.Find("textBox" + a, true).FirstOrDefault();
                        if (textbox != null) { textbox.Text = fkt.Sollwert; }
                        var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                        if (textbox2 != null) { textbox2.Text = fkt.Kommentar; }
                    }
                }
            }
        }

        //Steckdose: Schaltgruppe in oberer ComboBox auswählen
        private void comboBox38_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboLeerenVonBis(39, 42);
            TextLeerenVonBis(67, 74);

            int stelle = StelleInFunktionen(comboBox38.Text);
            if (stelle != 999)
            {
                foreach (Funktion fkt in Form1.liste[index].Funktionen)
                {
                    if (fkt != null && fkt.Verbraucher == comboBox38.Text)
                    {
                        var combobox = Controls.Find("comboBox" + fkt.ComboNr, true).FirstOrDefault();
                        if (combobox != null) { combobox.Text = fkt.Bedienelement; }

                        int a = 2 * fkt.ComboNr - 11;
                        int b = a + 1;
                        var textbox = Controls.Find("textBox" + a, true).FirstOrDefault();
                        if (textbox != null) { textbox.Text = fkt.Sollwert; }
                        var textbox2 = Controls.Find("textBox" + b, true).FirstOrDefault();
                        if (textbox2 != null) { textbox2.Text = fkt.Kommentar; }
                    }
                }
            }
        }

        //Schaltgruppe löschen
        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Möchten Sie die ausgewählte Beleuchtungsgruppe wirklich löschen?",
                "Eintrag löschen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            int stelle = StelleInFunktionen(comboBox1.Text);
                //löscht bereits vorhandene Funktionen für diese Schaltgruppe
                while (stelle != 999)
                {
                    Form1.liste[index].Funktionen[stelle] = null;
                    stelle = StelleInFunktionen(comboBox1.Text);
                }
            
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);

            AllesLeeren();

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;

            

        }
    }
}
