using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabazovyProjekt
{
    public class Order
    {
        public int ID {  get; set; }
        public int Payment_id { get; set; }
        public int Customer_id { get; set; }

        public int Order_number { get; set; }
        public double Order_price { get; set; }
        public DateTime Date { get; set; }

        public Order(int iD, int payment_id, int customer_id, int order_number, double order_price, DateTime date)
        {
            ID = iD;
            Payment_id = payment_id;
            Customer_id = customer_id;
            Order_number = order_number;
            Order_price = order_price;
            Date = date;
        }

        public Order() { }

        public override string? ToString()
        {
            return $"{ID} | Payment & id: {Payment_id} | Customer & id: {Customer_id} | Ord.Number: {Order_number} | Ord.Price: ${Order_price} | Date: {Date}";
        }
    }
}
