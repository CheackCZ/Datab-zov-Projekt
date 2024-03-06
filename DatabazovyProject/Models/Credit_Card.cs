using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Credit_Card
    {
        public List<Payment> payments;

        public int ID { get; set; }
        public string Card_Number { get; set; }
        public DateOnly Expiration_date { get; set; }
        public string CVV { get; set; }

        public Credit_Card(int iD, string cardNumber, DateOnly expiration_date, string cVV)
        {
            ID = iD;
            Card_Number = cardNumber;
            Expiration_date = expiration_date;
            CVV = cVV;
        }

        public override string? ToString()
        {
            return $"{ID} | Car Number: {Card_Number} | Expiration Date: {Expiration_date} | CVV: {CVV}";
        }
    }
}
