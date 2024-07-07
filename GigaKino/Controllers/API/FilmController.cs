using GigaKino.ObjectsDTO;
using GigaKino.Services;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly ISeansService _seansService;

        public FilmController(IFilmService filmService, ISeansService seansService)
        {
            _filmService = filmService;
            _seansService = seansService;
        }

        [HttpPost]
        public async Task<ActionResult<FilmDTO>> CreateFilm(FilmDTO filmDTO)
        {
            if (filmDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdFilm = await _filmService.CreateFilmAsync(filmDTO);
            if (createdFilm == null)
            {
                return BadRequest("Failed to create Film.");
            }

            return CreatedAtAction(nameof(GetFilmById), new { id = createdFilm.IdFilm }, createdFilm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmDTO>> GetFilmById(uint id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<FilmDTO>> GetFilmByTitleAsync(string title)
        {
            var film = await _filmService.GetFilmByTitleAsync(title);

            if (film == null)
            {
                return NotFound();
            }

            return Ok(film);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmDTO>>> GetAllFilmy()
        {
            var filmy = await _filmService.GetAllFilmyAsync();
            if (filmy == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(filmy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(uint id)
        {
            var isDeleted = await _filmService.DeleteFilmAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
