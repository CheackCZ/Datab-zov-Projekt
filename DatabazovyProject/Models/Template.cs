using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents a template entity, containing information about a template.
    /// </summary>
    public class Template
    {
        public int ID { get; set; }
        
        public int Author_id { get; set; }
        
        public int Typ_id { get; set; }

        public string Name { get; set; }
        public bool Priced { get; set; }
        public double? Price { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the template.</param>
        /// <param name="author_id">The identifier of the author associated with the template.</param>
        /// <param name="type_id">The identifier of the type associated with the template.</param>
        /// <param name="name">The name of the template.</param>
        /// <param name="priced">A value indicating whether the template is priced.</param>
        /// <param name="price">The price of the template, if applicable.</param>
        public Template(int iD, int author_id, int type_id, string name, bool priced, double? price)
        {
            ID = iD;
            Author_id = author_id;
            Typ_id = type_id;
            Name = name;
            Priced = priced;
            Price = price;
        }

        /// <summary>
        /// Empty constructor of this class.
        /// </summary>
        public Template() { }

        /// <summary>
        /// Returns a string representation of the template.
        /// </summary>
        /// <returns>A string containing the template's ID, associated author and type IDs, name, and price.</returns>
        public override string? ToString()
        {
            return $"{{\r\n    \"id\": {ID},\r\n    \"author_id\": {Author_id},\r\n    \"type_id\": {Typ_id},\r\n    \"name\": \"{Name}\",\r\n    \"priced\": {Priced},\r\n    \"price\": {Price}\r\n  }}";
        } 
    }
}
