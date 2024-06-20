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
    private readonly IRepertuarService _repertuarService;

    public HomeController(ILogger<HomeController> logger, IFilmService filmService, IRepertuarService repertuarService)
    {
        _logger = logger;
        _filmService = filmService;
        _repertuarService = repertuarService;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
