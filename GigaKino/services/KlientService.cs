using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class KlientService : IKlientService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public KlientService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KlientDTO> CreateKlientAsync(KlientDTO klientDTO)
        {
            var klient = _mapper.Map<Klient>(klientDTO);
            _context.Klienci.Add(klient);
            await _context.SaveChangesAsync();
            return _mapper.Map<KlientDTO>(klient);
        }

        public async Task<KlientDTO> GetKlientByIdAsync(uint id)
        {
            var klient = await _context.Klienci.FindAsync(id);
            return _mapper.Map<KlientDTO>(klient);
        }

        public async Task<IEnumerable<KlientDTO>> GetAllKlienciAsync()
        {
            var klienci = await _context.Klienci.ToListAsync();
            return _mapper.Map<IEnumerable<KlientDTO>>(klienci);
        }

        public async Task<KlientDTO> UpdateKlientAsync(uint id, KlientDTO klientDTO)
        {
            var klientToUpdate = await _context.Klienci.FindAsync(id);
            if (klientToUpdate == null)
                return null;

            _mapper.Map(klientDTO, klientToUpdate);
            await _context.SaveChangesAsync();
            return _mapper.Map<KlientDTO>(klientToUpdate);
        }

        public async Task<bool> DeleteKlientAsync(uint id)
        {
            var klient = await _context.Klienci.FindAsync(id);
            if (klient == null)
                return false;

            _context.Klienci.Remove(klient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
