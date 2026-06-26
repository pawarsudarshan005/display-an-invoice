using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        // GET /api/data
        [HttpGet]
        public IActionResult GetData()
        {
            // BUG FIX: original code set `string result = null;` and then read
            // `result.Length`, throwing a NullReferenceException on every call.
            // Provide a real value and guard against null/empty safely.
            string result = "Data fetched";

            if (!string.IsNullOrEmpty(result))
            {
                return Ok(new { message = result });
            }
            return BadRequest("No data");
        }
    }
}
