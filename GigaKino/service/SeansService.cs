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

        public async Task<SeansDTO?> CreateSeansAsync(SeansDTO klientDTO)
        {
            try
            {
                var klient = _mapper.Map<Seans>(klientDTO);
                _context.Seanse.Add(klient);
                await _context.SaveChangesAsync();
                return _mapper.Map<SeansDTO>(klient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateSeansAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<SeansDTO?> GetSeansByIdAsync(uint id)
        {
            try
            {
                var klient = await _context.Seanse.FindAsync(id);
                return _mapper.Map<SeansDTO>(klient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetSeansByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<SeansDTO>?> GetAllSeanseAsync()
        {
            try
            {
                var seanse = await _context.Seanse.ToListAsync();
                return _mapper.Map<IEnumerable<SeansDTO>>(seanse);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllSeanseAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<SeansDTO?> UpdateSeansAsync(uint id, SeansDTO klientDTO)
        {
            try
            {
                var existingSeans = await _context.Seanse.FindAsync(id);
                if (existingSeans == null)
                {
                    Console.WriteLine("UpdateSeansAsync - no such record in database");
                    return null;
                }

                _mapper.Map(klientDTO, existingSeans);
                _context.Seanse.Update(existingSeans);
                await _context.SaveChangesAsync();
                return _mapper.Map<SeansDTO>(existingSeans);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateSeansAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteSeansAsync(uint id)
        {
            try
            {
                var klient = await _context.Seanse.FindAsync(id);
                if (klient == null)
                    return false;

                _context.Seanse.Remove(klient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteSeansAsync failed:"+ ex);
                return null;
            }
        }
    }
}
