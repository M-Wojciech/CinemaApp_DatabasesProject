using GigaKino.ObjectsDTO;
using GigaKino.Services;
using GigaKino.ServicesInterfaces;
using GigaKino.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransakcjaController : Controller
    {
        private readonly ITransakcjaService _transakcjaService;
        private readonly ISeansService _seansService;
        private readonly ISalaService _salaService;
        private readonly IFilmService _filmService;
        private readonly IKinoService _kinoService;
        private readonly IMiejsceService _miejsceService;
        private readonly IBiletService _biletService;

        public TransakcjaController(ITransakcjaService transakcjaService, ISeansService seansService, ISalaService salaService, IFilmService filmService, IKinoService kinoService, IMiejsceService miejsceService, IBiletService biletService)
        {
            _transakcjaService = transakcjaService;
            _seansService = seansService;
            _salaService = salaService;
            _filmService = filmService;
            _kinoService = kinoService;
            _miejsceService = miejsceService;
            _biletService = biletService;
        }

        [HttpPost]
        public async Task<ActionResult<TransakcjaDTO>> CreateTransakcja(TransakcjaDTO transakcjaDTO)
        {
            if (transakcjaDTO == null)
            {
                return BadRequest("Cannot create from null object");
            }

            var createdTransakcja = await _transakcjaService.CreateTransakcjaAsync(transakcjaDTO);
            if (createdTransakcja == null)
            {
                return BadRequest("Failed to create Transakcja.");
            }

            return CreatedAtAction(nameof(GetTransakcjaById), new { id = createdTransakcja.IdTransakcja }, createdTransakcja);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransakcjaDTO>> GetTransakcjaById(uint id)
        {
            var transakcja = await _transakcjaService.GetTransakcjaByIdAsync(id);

            if (transakcja == null)
            {
                return NotFound();
            }

            return transakcja;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransakcjaDTO>>> GetAllTransakcjas()
        {
            var transakcjas = await _transakcjaService.GetAllTransakcjeAsync();
            if (transakcjas == null)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(transakcjas);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransakcja(uint id)
        {
            var isDeleted = await _transakcjaService.DeleteTransakcjaAsync(id);
            if (isDeleted != true)
            {
                return NotFound();
            }

            return NoContent();
        }

        /*[HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }*/

        [HttpGet("Checkout")]
        public async Task<IActionResult> Checkout(uint idSeans, string selectedSeats)
        {
            var seans = await _seansService.GetSeansByIdAsync(idSeans);
            if (seans == null) return NotFound();

            var selectedMiejsca = selectedSeats.Split(',').Select(id => uint.Parse(id)).ToList();
            var miejsca = new List<MiejsceDTO>();
            foreach (var id in selectedMiejsca)
            {
                var miejsce = await _miejsceService.GetMiejsceByIdAsync(id);
                if (miejsce != null) miejsca.Add(miejsce);
            }

            var model = new CheckoutViewModel
            {
                Seans = seans,
                WybraneMiejsca = miejsca,
                CenaLaczna = miejsca.Count * seans.CenaDomyslna // например, вычисление цены
            };

            return View(model);
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout([FromForm] CheckoutFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                // Handle invalid model
                return BadRequest(ModelState);
            }

            // Создайте или получите клиента
            var klient = new KlientDTO
            {
                Mail = formModel.Mail,
                Imie = formModel.Imie,
                Nazwisko = formModel.Nazwisko
            };
            var transakcja = new TransakcjaDTO
            {
                CenaLaczna = formModel.CenaLaczna,
                CzasRozpoczecia = DateTime.Now,
                Status = false, // Pending
                Klient = new KlientDTO
                {
                    Mail = formModel.Mail,
                    Imie = formModel.Imie,
                    Nazwisko = formModel.Nazwisko
                }
            };

            var createdTransakcja = await _transakcjaService.CreateTransakcjaAsync(transakcja);

            foreach (var idMiejsce in formModel.SelectedSeats)
            {
                var biletDTO = new BiletDTO
                {
                    CenaFaktyczna = formModel.CenaDomyslna,
                    IdSeans = formModel.IdSeans,
                    IdMiejsce = idMiejsce,
                    IdTransakcja = createdTransakcja.IdTransakcja,
                    Seans = await _seansService.GetSeansByIdAsync(formModel.IdSeans),
                    Miejsce = await _miejsceService.GetMiejsceByIdAsync(idMiejsce),
                    Transakcja = createdTransakcja
                };
                await _biletService.CreateBiletAsync(biletDTO);
            }

            createdTransakcja.Status = true;
            await _transakcjaService.UpdateTransakcjaAsync(createdTransakcja.IdTransakcja, createdTransakcja);

            return RedirectToAction("Confirmation", new { idTransakcja = createdTransakcja.IdTransakcja });
        }

        [HttpGet("Confirmation")]
        public async Task<IActionResult> Confirmation(uint idTransakcja)
        {
            var transakcja = await _transakcjaService.GetTransakcjaByIdAsync(idTransakcja);
            if (transakcja == null)
            {
                return NotFound();
            }

            return View(transakcja);
        }
    }
}
