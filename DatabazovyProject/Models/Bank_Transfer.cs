using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents a bank transfer entity, containing information about a bank transfer(s).
    /// </summary>
    public class Bank_Transfer
    {
        // public List<Payment> payments;

        public int ID { get; set; }
        public string Variable_Symbol { get; set; }
        public string IBAN { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the bank transfer.</param>
        /// <param name="variable_Symbol">The variable symbol associated with the bank transfer.</param>
        /// <param name="iBAN">The International Bank Account Number (IBAN) associated with the bank transfer.</param>
        public Bank_Transfer(int iD, string variable_Symbol, string iBAN)
        {
            ID = iD;
            Variable_Symbol = variable_Symbol;
            IBAN = iBAN;
        }

        /// <summary>
        /// Returns a string representation of the bank transfer.
        /// </summary>
        /// <returns>A string containing the bank transfer's ID, variable symbol, and IBAN.</returns>
        public override string? ToString()
        {
            return $"{ID} | Var. Symbol: {Variable_Symbol} | IBAN: {IBAN} ";
        }
    }
}
