using System.Collections.Generic;

namespace InvoiceApp.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;

        public List<InvoiceItem> Items { get; set; } = new();
    }
}
