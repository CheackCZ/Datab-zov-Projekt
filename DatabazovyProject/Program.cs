using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject
{
    /// <summary>
    /// This class initializes and configures the ASP.NET Core web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            //Importing JSON data (Author)
            /*string filePath = "Imports/authors3.json";
            Author.ImportJSON(filePath);*/

            //Importing XML data (Customer)
            /*string filePath = "Imports/customers2.xml";
            Customer.ImportXML(filePath);*/

            // Create a new web application builder.
            var builder = WebApplication.CreateBuilder(args);

            // Add controllers to the services collection.
            builder.Services.AddControllers();

            // Add API Explorer endpoints to the services collection.
            builder.Services.AddEndpointsApiExplorer();

            // Add Swagger generation services to the services collection.
            builder.Services.AddSwaggerGen();

            // Add database context to the services collection.
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Build the web application.
            var app = builder.Build();

            // Configure the HTTP request pipeline based on the environment.
            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger and Swagger UI middleware in development environment.
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable HTTPS redirection middleware.
            app.UseHttpsRedirection();

            // Enable authorization middleware.
            app.UseAuthorization();

            // Map controllers routes.
            app.MapControllers();

            // Run the application.
            app.Run();
        }
    }
}