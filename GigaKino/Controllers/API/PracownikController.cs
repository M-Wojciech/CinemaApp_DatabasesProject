using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracownikController : ControllerBase
    {
        private readonly IPracownikService _pracownikService;

        public PracownikController(IPracownikService pracownikService)
        {
            _pracownikService = pracownikService;
        }

        [HttpPost]
        public async Task<ActionResult<PracownikDTO>> CreatePracownik(PracownikDTO pracownikDTO)
        {
            if (pracownikDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdPracownik = await _pracownikService.CreatePracownikAsync(pracownikDTO);
            if (createdPracownik == null)
            {
                return BadRequest("Failed to create Pracownik.");
            }

            return CreatedAtAction(nameof(GetPracownikById), new { id = createdPracownik.IdPracownik }, createdPracownik);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PracownikDTO>> GetPracownikById(uint id)
        {
            var pracownik = await _pracownikService.GetPracownikByIdAsync(id);

            if (pracownik == null)
            {
                return NotFound();
            }

            return pracownik;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PracownikDTO>>> GetAllPracowniks()
        {
            var pracowniks = await _pracownikService.GetAllPracownicyAsync();
            if (pracowniks == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(pracowniks);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePracownik(uint id)
        {
            var isDeleted = await _pracownikService.DeletePracownikAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
