using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class SeansService : ISeansService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public SeansService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SeansDTO> CreateSeansAsync(SeansDTO seansDTO)
        {
            var seans = _mapper.Map<Seans>(seansDTO);
            _context.Seanse.Add(seans);
            await _context.SaveChangesAsync();
            return _mapper.Map<SeansDTO>(seans);
        }

        public async Task<SeansDTO> GetSeansByIdAsync(uint id)
        {
            var seans = await _context.Seanse.FindAsync(id);
            return _mapper.Map<SeansDTO>(seans);
        }

        public async Task<IEnumerable<SeansDTO>> GetAllSeanseAsync()
        {
            var seanse = await _context.Seanse.ToListAsync();
            return _mapper.Map<IEnumerable<SeansDTO>>(seanse);
        }

        public async Task<SeansDTO> UpdateSeansAsync(uint id, SeansDTO seansDTO)
        {
            var seansToUpdate = await _context.Seanse.FindAsync(id);
            if (seansToUpdate == null)
                return null;

            _mapper.Map(seansDTO, seansToUpdate);
            _context.Entry(seansToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<SeansDTO>(seansToUpdate);
        }

        public async Task<bool> DeleteSeansAsync(uint id)
        {
            var seans = await _context.Seanse.FindAsync(id);
            if (seans == null)
                return false;

            _context.Seanse.Remove(seans);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
