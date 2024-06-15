using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly ISalaService _salaService;

        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpPost]
        public async Task<ActionResult<SalaDTO>> CreateSala(SalaDTO salaDTO)
        {
            if (salaDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdSala = await _salaService.CreateSalaAsync(salaDTO);
            if (createdSala == null)
            {
                return BadRequest("Failed to create Sala.");
            }

            return CreatedAtAction(nameof(GetSalaById), new { id = createdSala.IdSala }, createdSala);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalaDTO>> GetSalaById(uint id)
        {
            var sala = await _salaService.GetSalaByIdAsync(id);

            if (sala == null)
            {
                return NotFound();
            }

            return sala;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaDTO>>> GetAllSalas()
        {
            var salas = await _salaService.GetAllSaleAsync();
            if (salas == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(salas);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(uint id)
        {
            var isDeleted = await _salaService.DeleteSalaAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
