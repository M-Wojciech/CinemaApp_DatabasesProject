using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class BiletService : IBiletService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public BiletService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BiletDTO> CreateBiletAsync(BiletDTO biletDTO)
        {
            var bilet = _mapper.Map<Bilet>(biletDTO);
            _context.Bilety.Add(bilet);
            await _context.SaveChangesAsync();
            return _mapper.Map<BiletDTO>(bilet);
        }

        public async Task<BiletDTO> GetBiletByIdAsync(uint id)
        {
            var bilet = await _context.Bilety.FindAsync(id);
            return _mapper.Map<BiletDTO>(bilet);
        }

        public async Task<IEnumerable<BiletDTO>> GetAllBiletyAsync()
        {
            var bilety = await _context.Bilety.ToListAsync();
            return _mapper.Map<IEnumerable<BiletDTO>>(bilety);
        }

        public async Task<bool> UpdateBiletAsync(uint id, BiletDTO biletDTO)
        {
            var existingBilet = await _context.Bilety.FindAsync(id);
            if (existingBilet == null)
                return false;

            // Update properties from DTO
            _mapper.Map(biletDTO, existingBilet);

            _context.Bilety.Update(existingBilet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBiletAsync(uint id)
        {
            var bilet = await _context.Bilety.FindAsync(id);
            if (bilet == null)
                return false;

            _context.Bilety.Remove(bilet);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
