using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNX_V2
{
    internal class Funktion
    {
        string name;
        string bedienelement;
        string verbraucher;
        string sollwert;
        string kommentar;
        bool schalten;
        bool dimmen;
        bool jalousie;

        public string Name { get { return name; } set { name = value; } }
        public string Bedienelement { get { return bedienelement; } set { bedienelement = value; } }
        public string Verbraucher { get { return verbraucher; } set { verbraucher = value; } }
        public string Sollwert { get { return sollwert; } set { sollwert = value; } }
        public string Kommentar { get { return kommentar; } set { kommentar = value; } }
        public bool Schalten { get { return schalten; } set { schalten = value; } }
        public bool Dimmen { get { return dimmen; } set { dimmen = value; } }
        public bool Jalousie { get { return jalousie; } set { jalousie = value; } }


        public Funktion()
        {
            schalten = false;
            dimmen = false;
            jalousie = false;
        }
    }
}
