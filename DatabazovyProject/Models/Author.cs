using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Portfolio { get; set; }

        /// <summary>
        /// Constructor of this class with the specified parameters.
        /// </summary>
        /// <param name="iD">The unique identifier for the author.</param>
        /// <param name="name">The first name of the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        /// <param name="email">The email address of the author.</param>
        /// <param name="portfolio">The portfolio reference of the author.</param>
        public Author(int iD, string name, string lastName, string email, string? portfolio)
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
            return $"{{\r\n  \"id\": {ID},\r\n  \"name\": \"{Name}\",\r\n  \"lastName\": \"{LastName}\",\r\n  \"email\": \"{Email}\",\r\n  \"portfolio\": \"{Portfolio}\"\r\n}}";
        }

        /// <summary>
        /// Imports JSON data from a file into the database.
        /// </summary>
        /// <param name="file">The path to the JSON file.</param>
        /// <remarks>
        /// This method reads the default connection string from the appsettings.json file.
        /// It deserializes the JSON data and inserts it into the database table.
        /// A unique ID is generated for each author entry using the <see cref="GenerateUniqueId(SqlConnection)"/> method.
        /// </remarks>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified JSON file is not found.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">Thrown when there is an error deserializing the JSON data.</exception>
        /// <exception cref="System.Data.SqlClient.SqlException">Thrown when there is an error executing SQL commands.</exception>
        public static void ImportJSON(string file)
        {
            // Read the default connection string from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            string jsonFilePath = file;

            string jsonData = File.ReadAllText(jsonFilePath);

            List<Author> authors = JsonConvert.DeserializeObject<List<Author>>(jsonData);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (Author author in authors)
                    {
                        // Generate a new ID
                        int newId = GenerateUniqueId(connection);

                        // Insert author into the database
                        string insertQuery = "INSERT INTO Authors (ID, Name, LastName, Email, Portfolio) VALUES (@ID, @Name, @LastName, @Email, @Portfolio)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@ID", newId);
                            command.Parameters.AddWithValue("@Name", author.Name);
                            command.Parameters.AddWithValue("@LastName", author.LastName);
                            command.Parameters.AddWithValue("@Email", author.Email);
                            command.Parameters.AddWithValue("@Portfolio", author.Portfolio);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            } 
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Data inserted successfully.");
        }

        /// <summary>
        /// Generates a unique ID for a new author entry.
        /// </summary>
        /// <param name="connection">The SqlConnection object for database connection.</param>
        /// <returns>A unique ID for the new author entry.</returns>
        private static int GenerateUniqueId(SqlConnection connection)
        {
            string query = "SELECT MAX(ID) FROM Authors";

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
                    return 1;
                }
            }
        }
    }
}
