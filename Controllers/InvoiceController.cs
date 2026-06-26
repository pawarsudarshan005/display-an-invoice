using InvoiceApp.Data;
using InvoiceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public InvoiceController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            Invoice? invoice = await _db.Invoices
                .Include(i => i.Items)
                .OrderBy(i => i.InvoiceID)
                .FirstOrDefaultAsync();

            if (invoice is null || invoice.Items.Count == 0)
            {
                return NotFound("No invoice found");
            }

            var subtotal = invoice.Items.Sum(i => i.Price * i.Quantity);
            const decimal taxRate = 0.10m; // 10% tax
            var tax = subtotal * taxRate;
            var total = subtotal + tax;

            return Ok(new
            {
                invoiceNumber = invoice.InvoiceNumber,
                invoiceDate = invoice.InvoiceDate.ToString("yyyy-MM-dd"),
                dueDate = invoice.DueDate.ToString("yyyy-MM-dd"),
                customer = new
                {
                    name = invoice.CustomerName,
                    email = invoice.CustomerEmail,
                    phone = invoice.CustomerPhone,
                    address = invoice.BillingAddress
                },
                items = invoice.Items
                    .OrderBy(i => i.ItemID)
                    .Select(i => new
                    {
                        name = i.Name,
                        quantity = i.Quantity,
                        price = i.Price,
                        lineTotal = i.Price * i.Quantity
                    }),
                subtotal,
                taxRate,
                tax,
                total
            });
        }
    }
}
