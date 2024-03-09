using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Bank_Transfer
    {
        // public List<Payment> payments;

        public int ID { get; set; }
        public string Variable_Symbol { get; set; }
        public string IBAN { get; set; }

        public Bank_Transfer(int iD, string variable_Symbol, string iBAN)
        {
            ID = iD;
            Variable_Symbol = variable_Symbol;
            IBAN = iBAN;
        }

        public override string? ToString()
        {
            return $"{ID} | Var. Symbol: {Variable_Symbol} | IBAN: {IBAN} ";
        }
    }
}
