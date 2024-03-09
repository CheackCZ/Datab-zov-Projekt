using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Credit_Card
    {
       // public List<Payment> payments;

        public int ID { get; set; }
        public string Card_Number { get; set; }
        public DateTime Expiration_date { get; set; }
        public string CVV { get; set; }

        public Credit_Card(int iD, string card_Number, DateTime expiration_date, string cVV)
        {
            ID = iD;
            Card_Number = card_Number;
            Expiration_date = expiration_date;
            CVV = cVV;
        }

        public Credit_Card() { }

        public override string? ToString()
        {
            return $"{ID} | Car Number: {Card_Number} | Expiration Date: {Expiration_date} | CVV: {CVV}";
        }
    }
}