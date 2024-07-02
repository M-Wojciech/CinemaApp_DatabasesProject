using GigaKino.ObjectsDTO;
using GigaKino.Services;
using GigaKino.ServicesInterfaces;
using GigaKino.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : Controller
    {
        private readonly ISalaService _salaService;
        private readonly IMiejsceService _miejsceService;
        private readonly IBiletService _biletService;
        private readonly ISeansService _seansService;
        private readonly ITransakcjaService _transakcjaService;

        public SalaController(ISalaService salaService, ISeansService seansService, IMiejsceService miejsceService, IBiletService biletService, ITransakcjaService transakcjaService)
        {
            _salaService = salaService;
            _seansService = seansService;
            _biletService = biletService;
            _miejsceService = miejsceService;
            _transakcjaService = transakcjaService;
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

        [HttpGet("Sala")]
        public async Task<IActionResult> Sala(uint idSeans, int quantity)
        {
            var seans = await _seansService.GetSeansByIdAsync(idSeans);
            if (seans == null) return NotFound();

            var miejsca = await _miejsceService.GetMiejscaBySalaIdAsync(seans.IdSala);
            if (miejsca == null || !miejsca.Any()) return NotFound();

            var bilety = await _biletService.GetBiletBySeansIdAsync(idSeans);

            var zajeteMiejsca = bilety.Select(b => b.IdMiejsce).ToHashSet();

            var model = new SalaViewModel
            {
                Seans = seans,
                Miejsca = miejsca,
                ZajeteMiejsca = zajeteMiejsca,
                Quantity = quantity
            };

            return View(model);
        }
    }
}
