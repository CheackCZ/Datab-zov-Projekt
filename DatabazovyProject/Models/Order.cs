using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents an order entity, containing information about an order.
    /// </summary>
    public class Order
    {
        public int ID {  get; set; }
        public int Payment_id { get; set; }
        public int Customer_id { get; set; }

        public int Order_number { get; set; }
        public double Order_price { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the order.</param>
        /// <param name="payment_id">The identifier of the payment associated with the order.</param>
        /// <param name="customer_id">The identifier of the customer associated with the order.</param>
        /// <param name="order_number">The order number.</param>
        /// <param name="order_price">The price of the order.</param>
        /// <param name="date">The date of the order.</param>
        public Order(int iD, int payment_id, int customer_id, int order_number, double order_price, DateTime date)
        {
            ID = iD;
            Payment_id = payment_id;
            Customer_id = customer_id;
            Order_number = order_number;
            Order_price = order_price;
            Date = date;
        }

        /// <summary>
        /// Empty constructor of this class.
        /// </summary>
        public Order() { }

        /// <summary>
        /// Returns a string representation of the order.
        /// </summary>
        /// <returns>A string containing the order's ID, associated payment and customer IDs, order number, price, and date.</returns>
        public override string? ToString()
        {
            return $"{ID} | Payment & id: {Payment_id} | Customer & id: {Customer_id} | Ord.Number: {Order_number} | Ord.Price: ${Order_price} | Date: {Date}";
        }
    }
}
