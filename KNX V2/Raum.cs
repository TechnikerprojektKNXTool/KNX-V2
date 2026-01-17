using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNX_V2
{
    public class Raum
    {
        string name;
        string typ;
        Funktion[] funktionen;
        //string[] schaltstellen;
        List<string> schaltstellen;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Typ
        {
            get { return typ; }
            set { typ = value; }
        }

        public Funktion[] Funktionen
        {
            get { return funktionen; }
            set { funktionen = value; }
        }

        public List<string> Schaltstellen
        {
            get { return schaltstellen; }
            set { schaltstellen = value; }
        }


        public Raum()
        {
            funktionen = new Funktion[50];
            schaltstellen = new List<string>();
        }
    }
}
