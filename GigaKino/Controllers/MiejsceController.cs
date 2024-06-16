using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiejsceController : ControllerBase
    {
        private readonly IMiejsceService _miejsceService;

        public MiejsceController(IMiejsceService miejsceService)
        {
            _miejsceService = miejsceService;
        }

        [HttpPost]
        public async Task<ActionResult<MiejsceDTO>> CreateMiejsce(MiejsceDTO miejsceDTO)
        {
            if (miejsceDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdMiejsce = await _miejsceService.CreateMiejsceAsync(miejsceDTO);
            if (createdMiejsce == null)
            {
                return BadRequest("Failed to create Miejsce.");
            }

            return CreatedAtAction(nameof(GetMiejsceById), new { id = createdMiejsce.IdMiejsce }, createdMiejsce);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MiejsceDTO>> GetMiejsceById(uint id)
        {
            var miejsce = await _miejsceService.GetMiejsceByIdAsync(id);

            if (miejsce == null)
            {
                return NotFound();
            }

            return miejsce;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MiejsceDTO>>> GetAllMiejsces()
        {
            var miejsces = await _miejsceService.GetAllMiejscaAsync();
            if (miejsces == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(miejsces);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMiejsce(uint id)
        {
            var isDeleted = await _miejsceService.DeleteMiejsceAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
