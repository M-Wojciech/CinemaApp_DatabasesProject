using GigaKino.ObjectsDTO;
using GigaKino.Services;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeansController : Controller
    {
        private readonly ISeansService _seansService;

        public SeansController(ISeansService seansService)
        {
            _seansService = seansService;
        }

        [HttpPost]
        public async Task<ActionResult<SeansDTO>> CreateSeans(SeansDTO seansDTO)
        {
            if (seansDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdSeans = await _seansService.CreateSeansAsync(seansDTO);
            if (createdSeans == null)
            {
                return BadRequest("Failed to create Seans.");
            }

            return CreatedAtAction(nameof(GetSeansById), new { id = createdSeans.IdSeans }, createdSeans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeansDTO>> GetSeansById(uint id)
        {
            var seans = await _seansService.GetSeansByIdAsync(id);

            if (seans == null)
            {
                return NotFound();
            }

            return seans;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeansDTO>>> GetAllSeanss()
        {
            var seanss = await _seansService.GetAllSeanseAsync();
            if (seanss == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(seanss);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeans(uint id)
        {
            var isDeleted = await _seansService.DeleteSeansAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
