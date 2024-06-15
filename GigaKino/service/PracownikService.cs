using AutoMapper;
using Database;
using GigaKino.ObjectsDTO;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PracownikDTO?> CreatePracownikAsync(PracownikDTO pracownikDTO)
        {
            try
            {
                var pracownik = _mapper.Map<Pracownik>(pracownikDTO);
                _context.Pracownicy.Add(pracownik);
                await _context.SaveChangesAsync();
                return _mapper.Map<PracownikDTO>(pracownik);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CratePracownikAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<PracownikDTO?> GetPracownikByIdAsync(uint id)
        {
            try
            {
                var pracownik = await _context.Pracownicy.FindAsync(id);
                return _mapper.Map<PracownikDTO>(pracownik);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetPracownikByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<PracownikDTO>?> GetAllPracownicyAsync()
        {
            try
            {
                var pracownicy = await _context.Pracownicy.ToListAsync();
                return _mapper.Map<IEnumerable<PracownikDTO>>(pracownicy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllPracownicyAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<PracownikDTO?> UpdatePracownikAsync(uint id, PracownikDTO pracownikDTO)
        {
            try
            {
                var existingPracownik = await _context.Pracownicy.FindAsync(id);
                if (existingPracownik == null)
                {
                    Console.WriteLine("UpdatePracownikAsync - no such record in database");
                    return null;
                }

                _mapper.Map(pracownikDTO, existingPracownik);
                _context.Pracownicy.Update(existingPracownik);
                await _context.SaveChangesAsync();
                return _mapper.Map<PracownikDTO>(existingPracownik);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdatePracownikAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeletePracownikAsync(uint id)
        {
            try
            {
                var pracownik = await _context.Pracownicy.FindAsync(id);
                if (pracownik == null)
                    return false;

                _context.Pracownicy.Remove(pracownik);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeletePracownikAsync failed:"+ ex);
                return null;
            }
        }
    }
}
