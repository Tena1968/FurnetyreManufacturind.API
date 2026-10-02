using FurnetureManufacturind.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FurnetureManufacturind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        // GET: api/<NotesController>
        [HttpGet]
        public async Task<IActionResult> GetNotes(int id)
        {
            FurnitureManufacturingContext context = new FurnitureManufacturingContext();

            var notes = await context.Notes.Select(n => new NoteResponse
            {
                Id = n.NotesId,
                TitleUser = $"{n.Title} - {n.User.Login}",
                Contet = n.Content,
                FormattedDate = n.CreatedAt.ToString("dd.MM.yyyy")
            }).ToListAsync();

            return Ok(notes);
        }
        public class NoteResponse
        {
            public int Id { get; set; }
            public string TitleUser { get; set; }
            public string Contet { get; set; }
            public string FormattedDate { get; set; }
        }
    }
}
