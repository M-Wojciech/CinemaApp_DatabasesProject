using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;
using GigaKino.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GigaKino.ObjectsDTO;

namespace GigaKino.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFilmService _filmService;
    private readonly IRepertuarService _repertuarService;
    private readonly ISalaService _salaService;
    private readonly ISeansService _seansService;
    private readonly IKinoService _kinoService;

    public HomeController(ILogger<HomeController> logger, IFilmService filmService, IRepertuarService repertuarService, ISalaService salaService, ISeansService seansService, IKinoService kinoService)
    {
        _logger = logger;
        _filmService = filmService;
        _repertuarService = repertuarService;
        _salaService = salaService;
        _seansService = seansService;
        _kinoService = kinoService;
    }

    public async Task<IActionResult> Index()
    {
        var filmy = await _filmService.GetAllFilmyAsync();
        if (filmy == null)
        {
            return StatusCode(500, "Internal server error");
        }
        return View(filmy);
    }

    public async Task<IActionResult> Repertuar()
    {
        var repertuar = await _repertuarService.GetRepertuarAsync(1);
        if (repertuar == null)
        {
            return StatusCode(500, "Internal server error");
        }
        return View(repertuar);
    }

    public async Task<IActionResult> StronaTestowa()
    {
        var repertuar = await _repertuarService.GetRepertuarAsync(1);
        if (repertuar == null)
        {
            return StatusCode(500, "Internal server error");
        }
        return View(repertuar);
    }

    public async Task<IActionResult> StronaTestowa2(uint idSeans)
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
