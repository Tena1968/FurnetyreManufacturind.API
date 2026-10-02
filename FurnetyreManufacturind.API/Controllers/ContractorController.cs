using FurnetureManufacturind.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FurnetureManufacturind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        // GET: api/<ContractorController>
        [HttpGet]
        public async Task<IActionResult> GetContractor(int id)
        {
            FurnitureManufacturingContext context = new FurnitureManufacturingContext();

            var contractor = await context.Contractors.ToListAsync();
            
            return Ok(contractor);
        }


    }
}