using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    public class Author
    {
        public List<Template> templates { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Portfolio { get; set; }

        public Author(int iD, string name, string lastName, string email, string portfolio)
        {
            ID = iD;
            Name = name;
            LastName = lastName;
            Email = email;
            Portfolio = portfolio;
        }

        public override string? ToString()
        {
            return $"{ID} | {Name} {LastName} | Email: {Email} | Portfolio ref.: {Portfolio}";
        }
    }
}
