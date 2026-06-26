namespace InvoiceApp.Models
{
    public class InvoiceItem
    {
        public int ItemID { get; set; }
        public int InvoiceID { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }
}
