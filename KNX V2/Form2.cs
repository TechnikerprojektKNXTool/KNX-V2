using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

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
            
            Raum raum = new Raum();
            raum = Form1.liste[index];

            //fügt schon gespeicherte Verbraucher als Option in den Comboboxen hinzu
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
        public void AllesLeeren()
        {
            for (int i = 1; i < 44; i++)
            {
                var combo = Controls.Find("comboBox" + i, true).FirstOrDefault();
                if (combo != null)
                {
                    combo.Text = "";
                }
            }
            for (int i = 1;i < 75; i++)
            {
                var textbox = Controls.Find("textBox" + i, true).FirstOrDefault();
                if (textbox != null)
                {
                    textbox.Text = "";
                }
            }
        }
        
        public void ComboLeerenVonBis(int a, int b)
        {
            for (int i = a; i < (b+1); i++)
            {
                var combobox = Controls.Find("comboBox" + i, true).FirstOrDefault();
                if (combobox != null) { combobox.Text = ""; }
            }
            
        }

        private void TextLeerenVonBis(int a, int b)
        {           
            for (int i = a; i < (b+1); i++)
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
                    int a = 2 * i - 9;
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

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        //tab1 Licht: gespeicherte Funktionen anzeigen wenn eine Lichtgruppe ausgewählt wird
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

        //Verdunkelung
        private void comboBox21_SelectedIndexChanged_1(object sender, EventArgs e)
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

        //Oberlichter
        private void comboBox28_SelectedIndexChanged_1(object sender, EventArgs e)
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

        //Heizgruppen
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

        //Steckdosen
        private void comboBox38_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboLeerenVonBis(39, 42);
            TextLeerenVonBis(67,74);

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

        
    }
}
