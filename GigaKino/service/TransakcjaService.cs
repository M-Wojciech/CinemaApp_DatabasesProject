using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigaKino.Services
{
    public class TransakcjaService : ITransakcjaService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public TransakcjaService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransakcjaDTO?> CreateTransakcjaAsync(TransakcjaDTO transakcjaDTO)
        {
            try
            {
                var transakcja = _mapper.Map<Transakcja>(transakcjaDTO);
                _context.Transakcje.Add(transakcja);
                await _context.SaveChangesAsync();
                return _mapper.Map<TransakcjaDTO>(transakcja);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateTransakcjaAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<TransakcjaDTO?> GetTransakcjaByIdAsync(uint id)
        {
            try
            {
                var transakcja = await _context.Transakcje.FindAsync(id);
                return _mapper.Map<TransakcjaDTO>(transakcja);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetTransakcjaByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<TransakcjaDTO>?> GetAllTransakcjeAsync()
        {
            try
            {
                var transakcje = await _context.Transakcje.ToListAsync();
                return _mapper.Map<IEnumerable<TransakcjaDTO>>(transakcje);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllTransakcjeAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<TransakcjaDTO?> UpdateTransakcjaAsync(uint id, TransakcjaDTO transakcjaDTO)
        {
            try
            {
                var existingTransakcja = await _context.Transakcje.FindAsync(id);
                if (existingTransakcja == null)
                {
                    Console.WriteLine("UpdateTransakcjaAsync - no such record in database");
                    return null;
                }

                _mapper.Map(transakcjaDTO, existingTransakcja);
                _context.Transakcje.Update(existingTransakcja);
                await _context.SaveChangesAsync();
                return _mapper.Map<TransakcjaDTO>(existingTransakcja);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateTransakcjaAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteTransakcjaAsync(uint id)
        {
            try
            {
                var transakcja = await _context.Transakcje.FindAsync(id);
                if (transakcja == null)
                    return false;

                _context.Transakcje.Remove(transakcja);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteTransakcjaAsync failed:"+ ex);
                return null;
            }
        }
    }
}
