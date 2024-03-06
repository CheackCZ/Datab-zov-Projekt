using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Payment
    {
        public int ID { get; set; }
        public int Card_id { get; set; }
        public Credit_Card Card { get; set; }
        public int Bank_Transfer_id { get; set; }
        public Bank_Transfer Bank_Transfer { get; set; }
        public string Description { get; set; }

        public Payment(int iD, int card_id, Credit_Card card, int bank_transfer_id, Bank_Transfer bank_Transfer, string description)
        {
            ID = iD;
            Card_id = card_id;
            Card = card;
            Bank_Transfer = bank_Transfer;
            Bank_Transfer_id = bank_transfer_id;
            Description = description;
        }

        public override string? ToString()
        {
            return $"{ID} | Car & id: {Card}, {Card_id} | Bank Transfer & id: {Bank_Transfer}, {Bank_Transfer_id}  | Desc.: {Description}";
        }
    }
}
