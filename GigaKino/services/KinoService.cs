using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class KinoService : IKinoService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public KinoService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KinoDTO> CreateKinoAsync(KinoDTO kinoDTO)
        {
            var kino = _mapper.Map<Kino>(kinoDTO);
            _context.Kina.Add(kino);
            await _context.SaveChangesAsync();
            return _mapper.Map<KinoDTO>(kino);
        }

        public async Task<KinoDTO> GetKinoByIdAsync(uint id)
        {
            var kino = await _context.Kina.FindAsync(id);
            return _mapper.Map<KinoDTO>(kino);
        }

        public async Task<IEnumerable<KinoDTO>> GetAllKinaAsync()
        {
            var kina = await _context.Kina.ToListAsync();
            return _mapper.Map<IEnumerable<KinoDTO>>(kina);
        }

        public async Task<KinoDTO> UpdateKinoAsync(uint id, KinoDTO kinoDTO)
        {
            var kinoToUpdate = await _context.Kina.FindAsync(id);
            if (kinoToUpdate == null)
                return null;

            _mapper.Map(kinoDTO, kinoToUpdate);
            await _context.SaveChangesAsync();
            return _mapper.Map<KinoDTO>(kinoToUpdate);
        }

        public async Task<bool> DeleteKinoAsync(uint id)
        {
            var kino = await _context.Kina.FindAsync(id);
            if (kino == null)
                return false;

            _context.Kina.Remove(kino);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
