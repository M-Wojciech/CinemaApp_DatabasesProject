using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using GigaKino.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace GigaKino.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ISeansService _seansService;
        private readonly IFilmService _filmService;
        private readonly ISalaService _salaService;
        private readonly IKinoService _kinoService;
        private readonly IMiejsceService _miejsceService;
        private readonly IBiletService _biletService;
        private readonly ITransakcjaService _transakcjaService;

        public PurchaseController(ISeansService seansService, IFilmService filmService, ISalaService salaService, IKinoService kinoService, IMiejsceService miejsceService, IBiletService biletService, ITransakcjaService transakcjaService)
        {
            _seansService = seansService;
            _filmService = filmService;
            _salaService = salaService;
            _kinoService = kinoService;
            _miejsceService = miejsceService;
            _biletService = biletService;
            _transakcjaService = transakcjaService;
        }

        [HttpGet("TicketsChoice")]
        public async Task<IActionResult> TicketsChoice(uint idSeans)
        {
            var seans = await _seansService.GetSeansByIdAsync(idSeans);
            if (seans == null)
                return StatusCode(500, "Internal server error");
            var film = await _filmService.GetFilmByIdAsync(seans.IdFilm);
            if (film == null)
                return StatusCode(500, "Internal server error");
            var sala = await _salaService.GetSalaByIdAsync(seans.IdSala);
            if (sala == null)
                return StatusCode(500, "Internal server error");
            var kino = await _kinoService.GetKinoByIdAsync(sala.IdKino);
            if (kino == null)
                return StatusCode(500, "Internal server error");

            var miejsca = await _miejsceService.GetMiejscaBySalaIdAsync(sala.IdSala);
            if (miejsca == null || !miejsca.Any())
            {
                return StatusCode(500, "Internal server error");
            }

            var bilety = await _biletService.GetBiletBySeansIdAsync(idSeans);
            if (bilety == null)
            {
                bilety = new List<BiletDTO>();
            }

            // �������� ID ���� ������� ����
            var occupiedMiejsceIds = bilety.Select(b => b.IdMiejsce).ToHashSet();

            // ����������� ��������� �����
            var freeMiejsca = miejsca.Where(m => !occupiedMiejsceIds.Contains(m.IdMiejsce)).ToList();

            // ���������� ���������� ��������� ����
            int freeMiejscaCount = freeMiejsca.Count;

            var model = new SeansViewModel
            {
                Seans = seans,
                Film = film,
                Sala = sala,
                Kino = kino,
                Miejsca = freeMiejsca,
                Bilet = bilety,
                FreeMiejscaCount = freeMiejscaCount
            };

            //var model = new Tuple<SeansDTO, FilmDTO, SalaDTO, KinoDTO, MiejsceDTO>(seans, film, sala, kino, (MiejsceDTO)miejsca);//, KinoDTO>(seans, film, sala, kino);

            return View(model);
        }
    
        public async Task<IActionResult> SeatsChoice(uint idSeans, int quantity)
        {
            var seans = await _seansService.GetSeansByIdAsync(idSeans);
            if (seans == null) return NotFound();

            var miejsca = await _miejsceService.GetMiejscaBySalaIdAsync(seans.IdSala);
            if (miejsca == null || !miejsca.Any()) return NotFound();

            var bilety = await _biletService.GetBiletBySeansIdAsync(idSeans);

            var zajeteMiejsca = bilety.Select(b => b.IdMiejsce).ToHashSet();

            var model = new SalaViewModel
            {
                Seans = seans,
                Miejsca = miejsca,
                ZajeteMiejsca = zajeteMiejsca,
                Quantity = quantity
            };

            return View(model);
        }

        // [HttpGet("Checkout")]
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
                CenaLaczna = miejsca.Count * seans.CenaDomyslna // ��������, ���������� ����
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

            // �������� ��� �������� �������
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
                Status = true, // Pending
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