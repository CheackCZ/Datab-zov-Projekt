using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabazovyProjekt
{
    public class Objednavka
    {
        public int ID {  get; set; }
        public int Payment_id { get; set; }
        public Payment Payment { get; set; }
        public int Customer_id { get; set; }
        public Customer Customer { get; set; }

        public int Order_number { get; set; }
        public int Order_price { get; set; }
        public DateOnly Date { get; set; }

        public Objednavka(int iD, int payment_id, Payment payment, int customer_id, Customer customer, int order_number, int order_price, DateOnly date)
        {
            ID = iD;
            Payment_id = payment_id;
            Payment = payment;
            Customer_id = customer_id;
            Customer = customer;
            Order_number = order_number;
            Order_price = order_price;
            Date = date;
        }

        public override string? ToString()
        {
            return $"{ID} | Payment & id: {Payment}, {Payment_id} | Customer & id: {Customer}, {Customer_id} | Ord.Number: {Order_number} | Ord.Price: ${Order_price} | Date: {Date}";
        }
    }
}
