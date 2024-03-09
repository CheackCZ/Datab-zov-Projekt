using DatabazovyProjekt;
using Microsoft.EntityFrameworkCore;
using Type = DatabazovyProjekt.Type;

namespace DatabazovyProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Bank_Transfer> Bank_Transfers { get; set; }
        public DbSet<Credit_Card> Credit_Cards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Template> Templates { get; set; }
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
