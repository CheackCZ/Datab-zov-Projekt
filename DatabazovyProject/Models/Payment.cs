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
       // public List<Objednavka> objednavky;

        public int ID { get; set; }
        public int? Credit_Card_id { get; set; }
        public int? Bank_Transfer_id { get; set; }
        public string? Description { get; set; }

        public Payment(int iD, int? card_id, int? bank_transfer_id, string? description)
        {
            ID = iD;
            Credit_Card_id = card_id;
            Bank_Transfer_id = bank_transfer_id;
            Description = description;
        }

        public Payment() { }

        public override string? ToString()
        {
            return $"{ID} | Card(id): {Credit_Card_id} | Bank Transfer(id): {Bank_Transfer_id}  | Desc.: {Description}";
        }
    }
}
