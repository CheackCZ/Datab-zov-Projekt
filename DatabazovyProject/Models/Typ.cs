using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Typ
    {
        public List<Template> templates { get; set; }

        public int ID {  get; set; }
        public string Type { get; set; }

        public Typ(int iD, string type)
        {
            ID = iD;
            Type = type;
        }

        public override string? ToString()
        {
            return $"{ID} | {Type}";
        }
    }
}
