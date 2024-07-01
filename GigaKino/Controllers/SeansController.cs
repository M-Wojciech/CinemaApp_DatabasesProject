using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.Services;
using GigaKino.ServicesInterfaces;
using GigaKino.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeansController : Controller
    {
        private readonly ISeansService _seansService;
        private readonly IFilmService _filmService;
        private readonly ISalaService _salaService;
        private readonly IKinoService _kinoService;
        private readonly IMiejsceService _miejsceService;
        private readonly IBiletService _biletService;

        public SeansController(ISeansService seansService, IFilmService filmService, ISalaService salaService, IKinoService kinoService, IMiejsceService miejsceService, IBiletService biletService)
        {
            _seansService = seansService;
            _filmService = filmService;
            _salaService = salaService;
            _kinoService = kinoService;
            _miejsceService = miejsceService;
            _biletService = biletService;
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

        [HttpGet("film/{filmId}")]
        public async Task<ActionResult<IEnumerable<SeansDTO>>> GetSeansByFilmId(uint filmId)
        {
            var seanse = await _seansService.GetSeansByIdAsync(filmId);
            if (seanse == null)
            {
                return NotFound();
            }

            return Ok(seanse);
        }

        /*[HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }*/

        [HttpGet("index")]
        public async Task<IActionResult> Index(uint idSeans)
        {
            Console.WriteLine("-------- given id: " + idSeans);
            var seans = await _seansService.GetSeansByIdAsync(idSeans);
            if (seans == null)
                return StatusCode(500, "Internal server error");
            Console.WriteLine("-------- seans id: " + seans.IdSeans);
            var film = await _filmService.GetFilmByIdAsync(seans.IdFilm);
            if (film == null)
                return StatusCode(500, "Internal server error");
            Console.WriteLine("-------- film id: " + film.IdFilm);
            Console.WriteLine("-------- film id: " + film.Tytul);
            var sala = await _salaService.GetSalaByIdAsync(seans.IdSala);
            if (sala == null)
                return NotFound();
            Console.WriteLine("-------- sala id: " + sala.IdSala);
            var kino = await _kinoService.GetKinoByIdAsync(sala.IdKino);
            if (kino == null)
                return NotFound();

            var miejsca = await _miejsceService.GetMiejscaBySalaIdAsync(sala.IdSala);
            if (miejsca == null || !miejsca.Any())
            {
                return NotFound();
            }

            var bilety = await _biletService.GetBiletBySeansIdAsync(idSeans);
            if (bilety == null)
            {
                bilety = new List<BiletDTO>();
            }

            // Получить ID всех занятых мест
            var occupiedMiejsceIds = bilety.Select(b => b.IdMiejsce).ToHashSet();

            // Фильтровать свободные места
            var freeMiejsca = miejsca.Where(m => !occupiedMiejsceIds.Contains(m.IdMiejsce)).ToList();

            // Подсчитать количество свободных мест
            int freeMiejscaCount = freeMiejsca.Count;

            var model = new SeansViewModel
            {
                Seans = seans,
                Film = film,
                Sala = sala,
                Kino = kino,
                Miejsca = freeMiejsca,
                Bilet = bilety,
                FreeMiejscaCount = freeMiejscaCount
            };

            //var model = new Tuple<SeansDTO, FilmDTO, SalaDTO, KinoDTO, MiejsceDTO>(seans, film, sala, kino, (MiejsceDTO)miejsca);//, KinoDTO>(seans, film, sala, kino);

            return View(model);
        }
    }
}
