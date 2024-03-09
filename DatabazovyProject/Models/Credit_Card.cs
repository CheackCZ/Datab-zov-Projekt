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
    /// <summary>
    /// Represents a credit card entity, containing information about a credit card.
    /// </summary>
    public class Credit_Card
    {
       // public List<Payment> payments;

        public int ID { get; set; }
        public string Card_Number { get; set; }
        public DateTime Expiration_date { get; set; }
        public string CVV { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the credit card.</param>
        /// <param name="card_Number">The number of the credit card.</param>
        /// <param name="expiration_date">The expiration date of the credit card.</param>
        /// <param name="cVV">The Card Verification Value (CVV) of the credit card.</param>
        public Credit_Card(int iD, string card_Number, DateTime expiration_date, string cVV)
        {
            ID = iD;
            Card_Number = card_Number;
            Expiration_date = expiration_date;
            CVV = cVV;
        }

        /// <summary>
        /// Empty constructor of this class.
        /// </summary>
        public Credit_Card() { }

        /// <summary>
        /// Returns a string representation of the credit card.
        /// </summary>
        /// <returns>A string containing the credit card's ID, card number, expiration date, and CVV.</returns>
        public override string? ToString()
        {
            return $"{ID} | Car Number: {Card_Number} | Expiration Date: {Expiration_date} | CVV: {CVV}";
        }
    }
}