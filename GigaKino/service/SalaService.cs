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
    public class SalaService : ISalaService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public SalaService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SalaDTO?> CreateSalaAsync(SalaDTO salaDTO)
        {
            try
            {
                var sala = _mapper.Map<Sala>(salaDTO);
                _context.Sale.Add(sala);
                await _context.SaveChangesAsync();
                return _mapper.Map<SalaDTO>(sala);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateSalaAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<SalaDTO?> GetSalaByIdAsync(uint id)
        {
            try
            {
                var sala = await _context.Sale.FindAsync(id);
                return _mapper.Map<SalaDTO>(sala);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetSalaByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<SalaDTO>?> GetAllSaleAsync()
        {
            try
            {
                var sale = await _context.Sale.ToListAsync();
                return _mapper.Map<IEnumerable<SalaDTO>>(sale);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllSaleAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<SalaDTO?> UpdateSalaAsync(uint id, SalaDTO salaDTO)
        {
            try
            {
                var existingSala = await _context.Sale.FindAsync(id);
                if (existingSala == null)
                {
                    Console.WriteLine("UpdateSalaAsync - no such record in database");
                    return null;
                }

                _mapper.Map(salaDTO, existingSala);
                _context.Sale.Update(existingSala);
                await _context.SaveChangesAsync();
                return _mapper.Map<SalaDTO>(existingSala);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateSalaAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteSalaAsync(uint id)
        {
            try
            {
                var sala = await _context.Sale.FindAsync(id);
                if (sala == null)
                    return false;

                _context.Sale.Remove(sala);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteSalaAsync failed:"+ ex);
                return null;
            }
        }
    }
}
