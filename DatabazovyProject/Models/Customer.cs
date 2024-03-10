using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DatabazovyProjekt
{
    /// <summary>
    /// Represents a customer entity, containing information about a customer.
    /// </summary>
    public class Customer
    {
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
            return $"{{\r\n  \"id\": {ID},\r\n  \"name\": \"{Name}\",\r\n  \"lastName\": \"{LastName}\",\r\n  \"email\": \"{Email}\",\r\n  \"phone\": \"{Phone}\",\r\n  \"password\": \"{Password}\"\r\n}}";
        }

        /// <summary>
        /// Imports XML data from a file into the database.
        /// </summary>
        /// <param name="file">The path to the XML file.</param>
        /// <remarks>
        /// This method reads the default connection string from the appsettings.json file.
        /// It parses the XML data and inserts it into the database table.
        /// A unique ID is generated for each customer entry using the <see cref="GenerateUniqueId(SqlConnection)"/> method.
        /// </remarks>
        /// <param name="file">The path to the XML file.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified XML file is not found.</exception>
        /// <exception cref="System.Xml.XmlException">Thrown when there is an error parsing the XML data.</exception>
        /// <exception cref="System.Data.SqlClient.SqlException">Thrown when there is an error executing SQL commands.</exception>
        public static void ImportXML(string file)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            string xmlFilePath = file;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNodeList customerNodes = xmlDoc.SelectNodes("/customers/customer");
            if (customerNodes != null)
            {
                List<Customer> customers = new List<Customer>();

                foreach (XmlNode customerNode in customerNodes)
                {
                    Customer customer = new Customer
                    {
                        ID = Convert.ToInt32(customerNode.SelectSingleNode("Id")?.InnerText),
                        Name = customerNode.SelectSingleNode("Name")?.InnerText,
                        LastName = customerNode.SelectSingleNode("LastName")?.InnerText,
                        Email = customerNode.SelectSingleNode("Email")?.InnerText,
                        Phone = customerNode.SelectSingleNode("Phone")?.InnerText,
                        Password = customerNode.SelectSingleNode("Password")?.InnerText
                    };

                    customers.Add(customer);
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        foreach (Customer customer in customers)
                        {
                            // Generate a new ID
                            int newId = GenerateUniqueId(connection);

                            // Insert customer into the database
                            string insertQuery = "INSERT INTO Customers (ID, Name, LastName, Email, Phone, Password) " +
                                                 "VALUES (@ID, @Name, @LastName, @Email, @Phone, @Password)";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@ID", newId);
                                command.Parameters.AddWithValue("@Name", customer.Name);
                                command.Parameters.AddWithValue("@LastName", customer.LastName);
                                command.Parameters.AddWithValue("@Email", customer.Email);
                                command.Parameters.AddWithValue("@Phone", customer.Phone);
                                command.Parameters.AddWithValue("@Password", customer.Password);

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    Console.WriteLine("Data inserted successfully.");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (XmlException ex)
                {
                    Console.WriteLine("Error parsing XML: " + ex.Message);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error inserting data into database: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No customers found in the XML file.");
            }
        }

        /// <summary>
        /// Generates a unique ID for a new customer entry.
        /// </summary>
        /// <param name="connection">The SqlConnection object for database connection.</param>
        /// <returns>A unique ID for the new customer entry.</returns>
        private static int GenerateUniqueId(SqlConnection connection)
        {
            string query = "SELECT MAX(ID) FROM Customers";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    int maxId = Convert.ToInt32(result);
                    return maxId + 1;
                }
                else
                {
                    return 1; // If there are no existing records, start from 1
                }
            }
        }
    }
}