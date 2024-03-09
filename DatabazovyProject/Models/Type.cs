using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents a type entity, containing information about a type.
    /// </summary>
    public class Type
    {
        //public List<Template> templates { get; set; }

        public int ID {  get; set; }
        public string Nazev { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the type.</param>
        /// <param name="nazev">The name of the type.</param>
        public Type(int iD, string nazev)
        {
            ID = iD;
            Nazev = nazev;
        }

        /// <summary>
        /// Returns a string representation of the type.
        /// </summary>
        /// <returns>A string containing the type's ID and name.</returns>
        public override string? ToString()
        {
            return $"{ID} | {Nazev}";
        }
    }
}
