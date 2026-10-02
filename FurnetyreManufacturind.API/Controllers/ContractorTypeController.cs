using FurnetureManufacturind.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FurnetureManufacturind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorTypeController : ControllerBase
    {
        // GET: api/<ContractorTypeController>
        [HttpGet]
        public async Task<IActionResult> GetContractorType(int id)
        {
            FurnitureManufacturingContext context = new FurnitureManufacturingContext();

            var contractorType = await context.ContractorTypes.ToListAsync();

            return Ok(contractorType);
        }
    }
}
