using FurnetureManufacturind.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FurnetureManufacturind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclatureController : ControllerBase
    {
        // GET: api/<NomenclatureController>
        [HttpGet]
        public async Task<IActionResult> GetNomenclature(int id)
        {
            FurnitureManufacturingContext context = new FurnitureManufacturingContext();

            var nomenclature = await context.Nomenclatures.ToListAsync();

            return Ok(nomenclature);
        }
    }
}
