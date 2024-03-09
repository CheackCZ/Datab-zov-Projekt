using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Type
    {
        //public List<Template> templates { get; set; }

        public int ID {  get; set; }
        public string Nazev { get; set; }

        public Type(int iD, string nazev)
        {
            ID = iD;
            Nazev = nazev;
        }

        public override string? ToString()
        {
            return $"{ID} | {Nazev}";
        }
    }
}
