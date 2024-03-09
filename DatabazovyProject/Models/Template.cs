using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Template
    {
        //public List<Item> Items;
        public int ID { get; set; }
        
        public int Author_id { get; set; }
        
        public int Type_id { get; set; }

        public string Name { get; set; }
        public bool Priced { get; set; }
        public double? Price { get; set; }

        public Template(int iD, int author_id, int type_id, string name, bool priced, double? price)
        {
            ID = iD;
            Author_id = author_id;
            Type_id = type_id;
            Name = name;
            Priced = priced;
            Price = price;
        }

        public Template() { }

        public override string? ToString()
        {
            return $"{ID} | Author & id: {Author_id} | Type & id: {Type_id} | {Name} | Price: ${Price}";
        } 
    }
}
