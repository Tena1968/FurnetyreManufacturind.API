using FurnetureManufacturind.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FurnetureManufacturind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        // GET: api/<CustomerOrderController>
        [HttpGet]
         public async Task<IActionResult> GetOrderController(int id)
        {
            FurnitureManufacturingContext context = new FurnitureManufacturingContext();

            var CustomerOrder = await context.CustomerOrders.ToListAsync();

            return Ok(CustomerOrder);
        }

    }
}
