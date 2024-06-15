using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class KontoService : IKontoService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public KontoService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KontoDTO> CreateKontoAsync(KontoDTO kontoDTO)
        {
            var konto = _mapper.Map<Konto>(kontoDTO);
            _context.Konta.Add(konto);
            await _context.SaveChangesAsync();
            return _mapper.Map<KontoDTO>(konto);
        }

        public async Task<KontoDTO> GetKontoByIdAsync(uint id)
        {
            var konto = await _context.Konta.FindAsync(id);
            return _mapper.Map<KontoDTO>(konto);
        }

        public async Task<IEnumerable<KontoDTO>> GetAllKontaAsync()
        {
            var konta = await _context.Konta.ToListAsync();
            return _mapper.Map<IEnumerable<KontoDTO>>(konta);
        }

        public async Task<KontoDTO> UpdateKontoAsync(uint id, KontoDTO kontoDTO)
        {
            var kontoToUpdate = await _context.Konta.FindAsync(id);
            if (kontoToUpdate == null)
                return null;

            _mapper.Map(kontoDTO, kontoToUpdate);
            await _context.SaveChangesAsync();
            return _mapper.Map<KontoDTO>(kontoToUpdate);
        }

        public async Task<bool> DeleteKontoAsync(uint id)
        {
            var konto = await _context.Konta.FindAsync(id);
            if (konto == null)
                return false;

            _context.Konta.Remove(konto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
