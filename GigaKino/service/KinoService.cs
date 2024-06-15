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

        public async Task<KinoDTO?> CreateKinoAsync(KinoDTO kinoDTO)
        {
            try
            {
                var kino = _mapper.Map<Kino>(kinoDTO);
                _context.Kina.Add(kino);
                await _context.SaveChangesAsync();
                return _mapper.Map<KinoDTO>(kino);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateKinoAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<KinoDTO?> GetKinoByIdAsync(uint id)
        {
            try
            {
                var kino = await _context.Kina.FindAsync(id);
                return _mapper.Map<KinoDTO>(kino);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetKinoByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<KinoDTO>?> GetAllKinaAsync()
        {
            try
            {
                var kina = await _context.Kina.ToListAsync();
                return _mapper.Map<IEnumerable<KinoDTO>>(kina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllKinaAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<KinoDTO?> UpdateKinoAsync(uint id, KinoDTO kinoDTO)
        {
            try
            {
                var existingKino = await _context.Kina.FindAsync(id);
                if (existingKino == null)
                {
                    Console.WriteLine("UpdateKinoAsync - no such record in database");
                    return null;
                }

                _mapper.Map(kinoDTO, existingKino);
                _context.Kina.Update(existingKino);
                await _context.SaveChangesAsync();
                return _mapper.Map<KinoDTO>(existingKino);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateKinoAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteKinoAsync(uint id)
        {
            try
            {
                var kino = await _context.Kina.FindAsync(id);
                if (kino == null)
                    return false;

                _context.Kina.Remove(kino);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteKinoAsync failed:"+ ex);
                return null;
            }
        }
    }
}
