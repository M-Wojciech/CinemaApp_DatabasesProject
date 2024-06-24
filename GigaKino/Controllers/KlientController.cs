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
    public class KlientController : Controller
    {
        private readonly IKlientService _klientService;
        private readonly IKontoService _kontoService;

        public KlientController(IKlientService klientService, IKontoService kontoService)
        {
            _klientService = klientService;
            _kontoService = kontoService;
        }

        [HttpPost]
        public async Task<ActionResult<KlientDTO>> CreateKlient(KlientDTO klientDTO)
        {
            if (klientDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdKlient = await _klientService.CreateKlientAsync(klientDTO);
            if (createdKlient == null)
            {
                return BadRequest("Failed to create Klient.");
            }

            return CreatedAtAction(nameof(GetKlientById), new { id = createdKlient.IdKlient }, createdKlient);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KlientDTO>> GetKlientById(uint id)
        {
            var klient = await _klientService.GetKlientByIdAsync(id);

            if (klient == null)
            {
                return NotFound();
            }

            return klient;
        }

        [HttpGet("klients")]
        public async Task<ActionResult<IEnumerable<KlientDTO>>> GetAllKlients()
        {
            var klients = await _klientService.GetAllKlienciAsync();
            if (klients == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(klients);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKlient(uint id)
        {
            var isDeleted = await _klientService.DeleteKlientAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }

        /*[HttpGet("signup")]
        public IActionResult Signup()
        {
            return View();
        }*/

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromForm] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_kontoService.UserExists(model.Login))
            {
                ModelState.AddModelError(string.Empty, "Login already registered.");
                return View(model);
            }

            var salt = _kontoService.GenerateSalt();
            var hashedPassword = _kontoService.HashPassword(model.Password, salt);

            var konto = new Konto
            {
                Typ = model.Typ,
                Login = model.Login,
                Haslo = hashedPassword,
                Sol = salt
            };

            _kontoService.AddKonto(konto);

            var klient = new Klient
            {
                Mail = model.Mail,
                Imie = model.Imie,
                Nazwisko = model.Nazwisko,
                IdKonto = konto.IdKonto
            };

            _klientService.AddKlient(klient);

            return RedirectToAction("Index", "Home");
        }
    }
}
