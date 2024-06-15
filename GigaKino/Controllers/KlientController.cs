using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlientController : ControllerBase
    {
        private readonly IKlientService _klientService;

        public KlientController(IKlientService klientService)
        {
            _klientService = klientService;
        }

        [HttpPost]
        public async Task<ActionResult<KlientDTO>> CreateKlient(KlientDTO klientDTO)
        {
            if (klientDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdKlient = await _klientService.CreateKlientAsync(klientDTO);
            if (createdKlient == null)
            {
                return BadRequest("Failed to create Klient.");
            }

            return CreatedAtAction(nameof(GetKlientById), new { id = createdKlient.IdKlient }, createdKlient);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KlientDTO>> GetKlientById(uint id)
        {
            var klient = await _klientService.GetKlientByIdAsync(id);

            if (klient == null)
            {
                return NotFound();
            }

            return klient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KlientDTO>>> GetAllKlients()
        {
            var klients = await _klientService.GetAllKlienciAsync();
            if (klients == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(klients);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKlient(uint id)
        {
            var isDeleted = await _klientService.DeleteKlientAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
