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

            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    InvoiceID = 1,
                    InvoiceNumber = "INV-2026-001",
                    InvoiceDate = new DateTime(2026, 6, 26),
                    DueDate = new DateTime(2026, 7, 26),
                    CustomerName = "John Doe",
                    CustomerEmail = "john.doe@example.com",
                    CustomerPhone = "+1 (555) 123-4567",
                    BillingAddress = "1234 Elm Street, Suite 500, Springfield, IL 62704, USA"
                }
            );
            modelBuilder.Entity<InvoiceItem>().HasData(
                new InvoiceItem { ItemID = 1, InvoiceID = 1, Name = "Widget A", Quantity = 2, Price = 19.99m },
                new InvoiceItem { ItemID = 2, InvoiceID = 1, Name = "Widget B", Quantity = 1, Price = 29.50m },
                new InvoiceItem { ItemID = 3, InvoiceID = 1, Name = "Service Fee", Quantity = 1, Price = 9.99m }
            );
        }
    }
}
