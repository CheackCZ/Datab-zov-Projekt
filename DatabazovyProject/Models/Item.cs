using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents an item entity, containing information about an item.
    /// </summary>
    public class Item
    {
        public int ID { get; set; }
        public int Orders_id { get; set; }
        public int Template_id { get; set; }

        public int Quantity { get; set; }
        public double Price_of_Item { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier for the item.</param>
        /// <param name="order_id">The identifier of the order associated with the item.</param>
        /// <param name="template_id">The identifier of the template associated with the item.</param>
        /// <param name="quantity">The quantity of the item.</param>
        /// <param name="priceOfItem">The price of the item.</param>
        public Item(int id, int order_id, int template_id, int quantity, double priceOfItem)
        {
            ID = id;
            Orders_id = order_id;
            Template_id = template_id;
            Quantity = quantity;
            Price_of_Item = priceOfItem;
        }

        /// <summary>
        /// Empty constructor of this class.
        /// </summary>
        public Item() { }

        /// <summary>
        /// Returns a string representation of the item.
        /// </summary>
        /// <returns>A string containing the item's ID, associated order and template IDs, quantity, and price per item.</returns>
        public override string? ToString()
        {
            return $"{{\r\n  \"id\": {ID},\r\n  \"order_id\": {Orders_id},\r\n  \"template_id\": {Template_id},\r\n  \"quantity\": {Quantity},\r\n  \"price_of_Item\": {Price_of_Item}\r\n}}";
        }
    }
}
