using System.Collections.Generic;

namespace InvoiceApp.Models
{
    // Maps to the "Invoices" table in init.sql
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public List<InvoiceItem> Items { get; set; } = new();
    }
}
