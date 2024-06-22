using GigaKino.ObjectsDTO;
using GigaKino.Services;
using GigaKino.ServicesInterfaces;
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

        public SeansController(ISeansService seansService, IFilmService filmService, ISalaService salaService, IKinoService kinoService)
        {
            _seansService = seansService;
            _filmService = filmService;
            _salaService = salaService;
            _kinoService = kinoService;
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

            var model = new Tuple<SeansDTO, FilmDTO, SalaDTO, KinoDTO>(seans, film, sala, kino);//, KinoDTO>(seans, film, sala, kino);

            return View(model);
        }
    }
}
