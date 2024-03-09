using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents an author entity, containing information about an author.
    /// </summary>
    public class Author
    {
       // public List<Template> templates { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Portfolio { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the author.</param>
        /// <param name="name">The first name of the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        /// <param name="email">The email address of the author.</param>
        /// <param name="portfolio">The portfolio reference of the author.</param>
        public Author(int iD, string name, string lastName, string email, string portfolio)
        {
            ID = iD;
            Name = name;
            LastName = lastName;
            Email = email;
            Portfolio = portfolio;
        }

        /// <summary>
        /// Empty constructor of this class.
        /// </summary>
        public Author() { }

        /// <summary>
        /// Returns a string representation of the author.
        /// </summary>
        /// <returns>A string containing the author's ID, name, last name, email, and portfolio reference.</returns>

        public override string? ToString()
        {
            return $"{ID} | {Name} {LastName} | Email: {Email} | Portfolio ref.: {Portfolio}";
        }
    }
}
