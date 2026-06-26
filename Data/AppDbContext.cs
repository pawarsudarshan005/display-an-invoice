using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().HasKey(i => i.InvoiceID);
            modelBuilder.Entity<InvoiceItem>().HasKey(i => i.ItemID);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Items)
                .WithOne()
                .HasForeignKey(it => it.InvoiceID);

            // Seed data (mirrors init.sql)
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice { InvoiceID = 1, CustomerName = "John Doe" }
            );
            modelBuilder.Entity<InvoiceItem>().HasData(
                new InvoiceItem { ItemID = 1, InvoiceID = 1, Name = "Widget A", Price = 19.99m },
                new InvoiceItem { ItemID = 2, InvoiceID = 1, Name = "Widget B", Price = 29.50m },
                new InvoiceItem { ItemID = 3, InvoiceID = 1, Name = "Service Fee", Price = 9.99m }
            );
        }
    }
}
