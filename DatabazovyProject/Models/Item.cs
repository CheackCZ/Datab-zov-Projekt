using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatabazovyProjekt
{
    public class Item
    {
        public int ID { get; set; }
        public int Objednavka_id { get; set; }
        public Objednavka Objednavka { get; set; }
        public int Template_id { get; set; }
        public Template Template { get; set; }

        public int Quantity { get; set; }
        public int PriceOfItem { get; set; }

        public Item(int id, int objednavka_id, Objednavka objednavka, int template_id, Template template, int quantity, int priceOfItem)
        {
            ID = id;
            Objednavka_id = objednavka_id;
            Objednavka = objednavka;
            Template_id = template_id;
            Template = template;
            Quantity = quantity;
            PriceOfItem = priceOfItem;
        }

        public override string? ToString()
        {
            return $"{ID} | Objednavka & id: {Objednavka}, {Objednavka_id} | Template & id: {Template}, {Template_id} | Quantity: {Quantity} | Price/item: ${PriceOfItem}";
        }
    }
}
