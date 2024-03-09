using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents a customer entity, containing information about a customer.
    /// </summary>
    public class Customer
    {
        //public List<Objednavka> objednavky;
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the customer.</param>
        /// <param name="name">The first name of the customer.</param>
        /// <param name="lastName">The last name of the customer.</param>
        /// <param name="email">The email address of the customer.</param>
        /// <param name="phone">The phone number of the customer.</param>
        /// <param name="password">The password of the customer.</param>
        public Customer(int iD, string name, string lastName, string email, string phone, string password)
        {
            ID = iD;
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
        }

        /// <summary>
        /// Empty constructor of this class.
        /// </summary>
        public Customer() { }

        /// <summary>
        /// Returns a string representation of the customer.
        /// </summary>
        /// <returns>A string containing the customer's ID, full name, contact information, and password.</returns>
        public override string? ToString()
        {
            return $"{ID} | {Name} {LastName} | Contact: {Email}, {Phone} | {Password}";
        }
    }
}
