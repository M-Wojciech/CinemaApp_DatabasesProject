using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;


namespace GigaKino.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IRepertuarService _repertuarService;

        public HomeController(IFilmService filmService, IRepertuarService repertuarService)
        {
            _filmService = filmService;
            _repertuarService = repertuarService;
        }

        public async Task<IActionResult> Movies()
        {
            var filmy = await _filmService.GetAllFilmyAsync();
            if (filmy == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return View(filmy);
        }

        public async Task<IActionResult> Showtimes()
        {
            var repertuar = await _repertuarService.GetRepertuarAsync(1);
            if (repertuar == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return View(repertuar);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("details/{id}")] 
        public async Task<IActionResult> MovieDetails(uint id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }
    }
}