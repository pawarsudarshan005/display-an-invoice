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

        // GET /api/invoice  -> returns the invoice items for the front-end
        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            // BUG FIX: original code used a null list (`List<Item> items = null;`)
            // and dereferenced it, throwing NullReferenceException.
            // We now load the items from the database instead.
            List<InvoiceItem> items = await _db.InvoiceItems
                .OrderBy(i => i.ItemID)
                .ToListAsync();

            if (items.Count == 0)
            {
                return NotFound("No invoice found");
            }

            return Ok(new { items });
        }
    }
}
