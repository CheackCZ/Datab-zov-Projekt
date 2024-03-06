using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Template
    {
        public List<Item> Items;

        public int ID { get; set; }
        
        public int Author_id { get; set; }
        public Author Author { get; set; }
        
        public int Typ_id { get; set; }
        public Typ Typ { get; set; }

        public string Name { get; set; }
        public bool Priced { get; set; }
        public int? Price { get; set; }

        public Template(int iD, int author_id, Author author, int typ_id, Typ typ, string name, bool priced, int? price)
        {
            ID = iD;
            Author_id = author_id;
            Author = author;
            Typ_id = typ_id;
            Typ = typ;
            Name = name;
            Priced = priced;
            Price = price;
        }

        public override string? ToString()
        {
            return $"{ID} | Author & id: {Author}, {Author_id} | Type & id: {Typ}, {Typ_id} | {Name} | Price: ${Price}";
        } 
    }
}
