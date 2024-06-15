using System.CodeDom;
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

        public async Task<BiletDTO?> CreateBiletAsync(BiletDTO biletDTO)
        {
            try
            {
                var bilet = _mapper.Map<Bilet>(biletDTO);
                _context.Bilety.Add(bilet);
                await _context.SaveChangesAsync();
                return _mapper.Map<BiletDTO>(bilet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateBiletAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<BiletDTO?> GetBiletByIdAsync(uint id)
        {
            try
            {
                var bilet = await _context.Bilety.FindAsync(id);
                return _mapper.Map<BiletDTO>(bilet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetBiletByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<BiletDTO>?> GetAllBiletyAsync()
        {
            try
            {
                var bilety = await _context.Bilety.ToListAsync();
                return _mapper.Map<IEnumerable<BiletDTO>>(bilety);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllBiletyAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<BiletDTO?> UpdateBiletAsync(uint id, BiletDTO biletDTO)
        {
            try
            {
                var existingBilet = await _context.Bilety.FindAsync(id);
                if (existingBilet == null)
                {
                    Console.WriteLine("UpdateBiletAsync - no such record in database");
                    return null;
                }

                _mapper.Map(biletDTO, existingBilet);
                _context.Bilety.Update(existingBilet);
                await _context.SaveChangesAsync();
                return _mapper.Map<BiletDTO>(existingBilet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateBiletAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteBiletAsync(uint id)
        {
            try
            {
                var bilet = await _context.Bilety.FindAsync(id);
                if (bilet == null)
                    return false;

                _context.Bilety.Remove(bilet);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteBiletAsync failed:"+ ex);
                return null;
            }
        }
    }
}
