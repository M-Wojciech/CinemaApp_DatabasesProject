using GigaKino.ObjectsDTO;
using GigaKino.Services;
using GigaKino.ServicesInterfaces;
using GigaKino.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransakcjaController : Controller
    {
        private readonly ITransakcjaService _transakcjaService;
        private readonly ISeansService _seansService;
        private readonly ISalaService _salaService;
        private readonly IFilmService _filmService;
        private readonly IKinoService _kinoService;
        private readonly IMiejsceService _miejsceService;
        private readonly IBiletService _biletService;

        public TransakcjaController(ITransakcjaService transakcjaService, ISeansService seansService, ISalaService salaService, IFilmService filmService, IKinoService kinoService, IMiejsceService miejsceService, IBiletService biletService)
        {
            _transakcjaService = transakcjaService;
            _seansService = seansService;
            _salaService = salaService;
            _filmService = filmService;
            _kinoService = kinoService;
            _miejsceService = miejsceService;
            _biletService = biletService;
        }

        [HttpPost]
        public async Task<ActionResult<TransakcjaDTO>> CreateTransakcja(TransakcjaDTO transakcjaDTO)
        {
            if (transakcjaDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdTransakcja = await _transakcjaService.CreateTransakcjaAsync(transakcjaDTO);
            if (createdTransakcja == null)
            {
                return BadRequest("Failed to create Transakcja.");
            }

            return CreatedAtAction(nameof(GetTransakcjaById), new { id = createdTransakcja.IdTransakcja }, createdTransakcja);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransakcjaDTO>> GetTransakcjaById(uint id)
        {
            var transakcja = await _transakcjaService.GetTransakcjaByIdAsync(id);

            if (transakcja == null)
            {
                return NotFound();
            }

            return transakcja;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransakcjaDTO>>> GetAllTransakcjas()
        {
            var transakcjas = await _transakcjaService.GetAllTransakcjeAsync();
            if (transakcjas == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(transakcjas);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransakcja(uint id)
        {
            var isDeleted = await _transakcjaService.DeleteTransakcjaAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
