using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpPost]
        public async Task<ActionResult<FilmDTO>> CreateFilm(FilmDTO filmDTO)
        {
            var createdFilm = await _filmService.CreateFilmAsync(filmDTO);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmDTO>>> GetAllFilms()
        {
            var films = await _filmService.GetAllFilmsAsync();
            return Ok(films);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(uint id)
        {
            var isDeleted = await _filmService.DeleteFilmAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
