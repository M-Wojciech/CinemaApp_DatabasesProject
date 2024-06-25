using GigaKino.ObjectsDTO;
using GigaKino.Services;
using GigaKino.ServicesInterfaces;
using GigaKino.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KontoController : Controller
    {
        private readonly IKontoService _kontoService;
        private readonly IKlientService _klientService;

        public KontoController(IKontoService kontoService, IKlientService klientService)
        {
            _kontoService = kontoService;
            _klientService = klientService;
        }

        [HttpPost]
        public async Task<ActionResult<KontoDTO>> CreateKonto(KontoDTO kontoDTO)
        {
            if (kontoDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdKonto = await _kontoService.CreateKontoAsync(kontoDTO);
            if (createdKonto == null)
            {
                return BadRequest("Failed to create Konto.");
            }

            return CreatedAtAction(nameof(GetKontoById), new { id = createdKonto.IdKonto }, createdKonto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KontoDTO>> GetKontoById(uint id)
        {
            var konto = await _kontoService.GetKontoByIdAsync(id);

            if (konto == null)
            {
                return NotFound();
            }

            return konto;
        }

        [HttpGet("konto")]
        public async Task<ActionResult<IEnumerable<KontoDTO>>> GetAllKontos()
        {
            var kontos = await _kontoService.GetAllKontaAsync();
            if (kontos == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(kontos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKonto(uint id)
        {
            var isDeleted = await _kontoService.DeleteKontoAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var klient = _klientService.GetKlientByEmail(model.Email);

            if (klient != null)
            {
                var konto = _kontoService.GetKontoById(klient.IdKonto);

                if (konto != null && VerifyPassword(model.Password, konto.Haslo, konto.Sol))
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, model.Email)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true
                        });

                    return RedirectToAction("Index", "Home");
                }
            }

            // Если email или пароль неверны, добавьте сообщение об ошибке
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Метод для проверки пароля
            // Предполагается, что вы используете хеширование и соль для хранения паролей
            var hash = HashPassword(password, storedSalt);
            return hash == storedHash;
        }

        private string HashPassword(string password, string salt)
        {
            // Метод для хеширования пароля
            // Замените на вашу реализацию хеширования
            using var sha256 = SHA256.Create();
            var saltedPassword = password + salt;
            var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            return Convert.ToBase64String(hashBytes);
        }

        /*[HttpGet("mojekonto")]
        public async Task<IActionResult> MojeKonto()
        {
            var konty = await _kontoService.GetAllKontaAsync();
            if (konty == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return View(konty);
        }*/

        public IActionResult MojeKonto()
        {
            var email = User.Identity.Name;
            var klient = _klientService.GetKlientByEmail(email);

            if (klient == null)
            {
                return NotFound();
            }

            var viewModel = new MyAccountViewModel
            {
                Email = klient.Mail,
                Imie = klient.Imie,
                Nazwisko = klient.Nazwisko
            };

            return View(viewModel);
        }
    }
}
