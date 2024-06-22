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

        public async Task<SeansDTO?> CreateSeansAsync(SeansDTO seansDTO)
        {
            try
            {
                var seans = _mapper.Map<Seans>(seansDTO);
                _context.Seanse.Add(seans);
                await _context.SaveChangesAsync();
                return _mapper.Map<SeansDTO>(seans);
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
                var seans = await _context.Seanse.FindAsync(id);
                return _mapper.Map<SeansDTO>(seans);
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

        public async Task<SeansDTO?> UpdateSeansAsync(uint id, SeansDTO seansDTO)
        {
            try
            {
                var existingSeans = await _context.Seanse.FindAsync(id);
                if (existingSeans == null)
                {
                    Console.WriteLine("UpdateSeansAsync - no such record in database");
                    return null;
                }

                _mapper.Map(seansDTO, existingSeans);
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
                var seans = await _context.Seanse.FindAsync(id);
                if (seans == null)
                    return false;

                _context.Seanse.Remove(seans);
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
