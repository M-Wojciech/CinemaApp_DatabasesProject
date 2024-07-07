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
    }
}
