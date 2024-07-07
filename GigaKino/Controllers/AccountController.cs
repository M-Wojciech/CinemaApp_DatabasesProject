using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;
using GigaKino.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    public class AccountController : Controller
    {
        private readonly IKontoService _kontoService;
        private readonly IKlientService _klientService;

        public AccountController(IKontoService kontoService, IKlientService klientService)
        {
            _kontoService = kontoService;
            _klientService = klientService;
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

                    return RedirectToAction("Movies", "Home");
                }
            }

            // ���� email ��� ������ �������, �������� ��������� �� ������
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Movies", "Home");
        }

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // ����� ��� �������� ������
            // ��������������, ��� �� ����������� ����������� � ���� ��� �������� �������
            var hash = HashPassword(password, storedSalt);
            return hash == storedHash;
        }

        private string HashPassword(string password, string salt)
        {
            // ����� ��� ����������� ������
            // �������� �� ���� ���������� �����������
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

        public IActionResult MyAccount()
        {
            var email = User.Identity.Name;

            var klient = _klientService.GetKlientByEmail(email);

            if (klient == null)
            {
                return StatusCode(500, "Internal server error"); 
                // return NotFound();
            }

            var viewModel = new MyAccountViewModel
            {
                Email = klient.Mail,
                Imie = klient.Imie,
                Nazwisko = klient.Nazwisko
            };

            return View(viewModel);
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

            if (_kontoService.UserExists(model.Mail))
            {
                ModelState.AddModelError(string.Empty, "Login already registered.");
                return View(model);
            }

            var salt = _kontoService.GenerateSalt();
            var hashedPassword = _kontoService.HashPassword(model.Password, salt);

            var konto = new Konto
            {
                //Typ = model.Typ,
                Typ = "klient",
                Login = model.Mail,
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

            return RedirectToAction("Movies", "Home");
        }
    }
}