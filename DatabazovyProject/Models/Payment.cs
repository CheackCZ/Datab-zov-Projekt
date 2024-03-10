using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents a payment entity, containing information about a payment.
    /// </summary>
    public class Payment
    {
        public int ID { get; set; }
        public int? Credit_Card_id { get; set; }
        public int? Bank_Transfer_id { get; set; }
        public string? Description { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the payment.</param>
        /// <param name="card_id">The identifier of the associated credit card, if applicable.</param>
        /// <param name="bank_transfer_id">The identifier of the associated bank transfer, if applicable.</param>
        /// <param name="description">The description of the payment.</param>
        public Payment(int iD, int? card_id, int? bank_transfer_id, string? description)
        {
            ID = iD;
            Credit_Card_id = card_id;
            Bank_Transfer_id = bank_transfer_id;
            Description = description;
        }

        /// <summary>
        /// Empty constructor of this class.
        /// </summary>
        public Payment() { }

        /// <summary>
        /// Returns a string representation of the payment.
        /// </summary>
        /// <returns>A string containing the payment's ID, associated credit card and bank transfer IDs, and description.</returns>
        public override string? ToString()
        {
            return $"{{\r\n  \"id\": {ID},\r\n  \"credit_Card_id\": {Credit_Card_id},\r\n  \"bank_Transfer_id\": {Bank_Transfer_id},\r\n  \"description\": \"{Description}\"\r\n}}";
        }
    }
}
