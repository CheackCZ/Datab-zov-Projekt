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
        public int Order_id { get; set; }
        public int Template_id { get; set; }

        public int Quantity { get; set; }
        public double Price_of_Item { get; set; }

        public Item(int id, int order_id, int template_id, int quantity, double priceOfItem)
        {
            ID = id;
            Order_id = order_id;
            Template_id = template_id;
            Quantity = quantity;
            Price_of_Item = priceOfItem;
        }

        public Item() { }

        public override string? ToString()
        {
            return $"{ID} | Objednavka & id: {Order_id} | Template & id: {Template_id} | Quantity: {Quantity} | Price/item: ${Price_of_Item}";
        }
    }
}
