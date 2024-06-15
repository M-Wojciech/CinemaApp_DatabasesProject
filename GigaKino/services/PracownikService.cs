using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigaKino.Services
{
    public class PracownikService : IPracownikService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public PracownikService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Pracownik> CreatePracownikAsync(Pracownik pracownik)
        {
            _context.Pracownicy.Add(pracownik);
            await _context.SaveChangesAsync();
            return pracownik;
        }

        public async Task<Pracownik> GetPracownikByIdAsync(uint id)
        {
            var pracownik = await _context.Pracownicy.FindAsync(id);
            return pracownik;
        }

        public async Task<IEnumerable<Pracownik>> GetAllPracownicyAsync()
        {
            var pracownicy = await _context.Pracownicy.ToListAsync();
            return pracownicy;
        }

        public async Task<bool> UpdatePracownikAsync(Pracownik pracownik)
        {
            _context.Entry(pracownik).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePracownikAsync(uint id)
        {
            var pracownik = await _context.Pracownicy.FindAsync(id);
            if (pracownik == null)
                return false;

            _context.Pracownicy.Remove(pracownik);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
