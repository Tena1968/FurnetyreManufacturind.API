using FurnetureManufacturind.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FurnetureManufacturind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        // GET: api/<ManufacturerController>
        [HttpGet]
        public async Task<IActionResult> GetManufacturer(int id)
        {
            FurnitureManufacturingContext context = new FurnitureManufacturingContext();

            var manufacturer = await context.Manufacturers.ToListAsync();

            return Ok(manufacturer);
        }
    }
}
