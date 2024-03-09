using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Customer
    {
        //public List<Objednavka> objednavky;

        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public Customer(int iD, string name, string lastName, string email, string phone, string password)
        {
            ID = iD;
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
        }

        public Customer()
        {
        }

        public override string? ToString()
        {
            return $"{ID} | {Name} {LastName} | Contact: {Email}, {Phone} | {Password}";
        }
    }
}
