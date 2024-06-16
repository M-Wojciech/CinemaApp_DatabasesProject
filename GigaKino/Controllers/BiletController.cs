 using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var createdBilet = await _biletService.CreateBiletAsync(biletDTO);
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
        public async Task<ActionResult<IEnumerable<BiletDTO>>> GetAllBilets()
        {
            var bilets = await _biletService.GetAllBiletyAsync();
            return Ok(bilets);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilet(uint id)
        {
            var isDeleted = await _biletService.DeleteBiletAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
