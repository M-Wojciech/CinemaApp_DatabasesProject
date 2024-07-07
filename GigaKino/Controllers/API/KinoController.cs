using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GigaKino.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KinoController : ControllerBase
    {
        private readonly IKinoService _kinoService;

        public KinoController(IKinoService kinoService)
        {
            _kinoService = kinoService;
        }

        [HttpPost]
        public async Task<ActionResult<KinoDTO>> CreateKino(KinoDTO kinoDTO)
        {
            if (kinoDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdKino = await _kinoService.CreateKinoAsync(kinoDTO);
            if (createdKino == null)
            {
                return BadRequest("Failed to create Kino.");
            }

            return CreatedAtAction(nameof(GetKinoById), new { id = createdKino.IdKino }, createdKino);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KinoDTO>> GetKinoById(uint id)
        {
            var kino = await _kinoService.GetKinoByIdAsync(id);

            if (kino == null)
            {
                return NotFound();
            }

            return kino;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KinoDTO>>> GetAllKina()
        {
            var kina = await _kinoService.GetAllKinaAsync();
            if (kina == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(kina);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKino(uint id)
        {
            var isDeleted = await _kinoService.DeleteKinoAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
