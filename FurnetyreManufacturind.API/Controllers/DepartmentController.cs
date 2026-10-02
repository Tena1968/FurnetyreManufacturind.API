using FurnetureManufacturind.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FurnetureManufacturind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IActionResult> GetDepartment(int id)
        {
            FurnitureManufacturingContext context = new FurnitureManufacturingContext();

            var department = await context.Departments.ToListAsync();

            return Ok(department);
        }
    }
}
