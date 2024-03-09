using DatabazovyProjekt;
using Microsoft.EntityFrameworkCore;
using Type = DatabazovyProjekt.Type;

namespace DatabazovyProject.Data
{
    /// <summary>
    /// Represents the database context for the application, providing access to the underlying database and facilitating interaction with entity sets.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the "DataContext" class with the specified options.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the entity set for authors in the database.
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the entity set for bank transfers in the database.
        /// </summary>
        public DbSet<Bank_Transfer> Bank_Transfers { get; set; }

        /// <summary>
        /// Gets or sets the entity set for credit cards in the database.
        /// </summary>
        public DbSet<Credit_Card> Credit_Cards { get; set; }

        /// <summary>
        /// Gets or sets the entity set for customers in the database.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the entity set for items in the database.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the entity set for orders in the database.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the entity set for payments in the database.
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Gets or sets the entity set for templates in the database.
        /// </summary>
        public DbSet<Template> Templates { get; set; }

        /// <summary>
        /// Gets or sets the entity set for types in the database.
        /// </summary>
        public DbSet<Type> Types { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Objednavka>()
                .HasKey(o => new { o.Payment_id, o.Customer_id });
            modelBuilder.Entity<Objednavka>()
                .HasOne(c => c.Customer)
                .WithMany(o => o.objednavky)
                .HasForeignKey(c => c.Customer_id);
            modelBuilder.Entity<Objednavka>()
                .HasOne(p => p.Payment)
                .WithMany(o => o.objednavky)
                .HasForeignKey(p => p.Payment_id);
        }*/

    }
}
