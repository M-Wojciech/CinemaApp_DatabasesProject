 using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiletController : ControllerBase
    {
        private readonly IBiletService _biletService;

        public BiletController(IBiletService biletService)
        {
            _biletService = biletService;
        }

        [HttpPost]
        public async Task<ActionResult<BiletDTO>> CreateBilet(BiletDTO biletDTO)
        {
            if (biletDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdBilet = await _biletService.CreateBiletAsync(biletDTO);
            if (createdBilet == null)
            {
                return BadRequest("Failed to create Bilet.");
            }

            return CreatedAtAction(nameof(GetBiletById), new { id = createdBilet.IdBilet }, createdBilet);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BiletDTO>> GetBiletById(uint id)
        {
            var bilet = await _biletService.GetBiletByIdAsync(id);

            if (bilet == null)
            {
                return NotFound();
            }

            return bilet;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BiletDTO>>> GetAllBilety()
        {
            var bilety = await _biletService.GetAllBiletyAsync();
            if (bilety == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(bilety);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilet(uint id)
        {
            var isDeleted = await _biletService.DeleteBiletAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
