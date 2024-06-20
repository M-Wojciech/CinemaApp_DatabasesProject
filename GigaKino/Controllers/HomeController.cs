using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;
using GigaKino.Services;

namespace GigaKino.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFilmService _filmService;
    private readonly ISeansService _seansService;

    public HomeController(ILogger<HomeController> logger, IFilmService filmService, ISeansService seansService)
    {
        _logger = logger;
        _filmService = filmService;
        _seansService = seansService;
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
        var seans = await _seansService.GetAllSeanseAsync();
        if (seans == null)
        {
            return StatusCode(500, "Internal server error");
        }
        return View(seans);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
