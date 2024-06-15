using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;

namespace GigaKino.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFilmService _filmService;

    public HomeController(ILogger<HomeController> logger, IFilmService filmService)
    {
        _logger = logger;
        _filmService = filmService;
    }

    public async Task<IActionResult> Index()
    {
        var films = await _filmService.GetAllFilmyAsync();
        return View(films);
    }

    public IActionResult Repertuar()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
