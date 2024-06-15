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

        public async Task<SalaDTO> CreateSalaAsync(SalaDTO salaDTO)
        {
            var sala = _mapper.Map<Sala>(salaDTO);
            _context.Sale.Add(sala);
            await _context.SaveChangesAsync();
            return _mapper.Map<SalaDTO>(sala);
        }

        public async Task<SalaDTO> GetSalaByIdAsync(uint id)
        {
            var sala = await _context.Sale.FindAsync(id);
            return _mapper.Map<SalaDTO>(sala);
        }

        public async Task<IEnumerable<SalaDTO>> GetAllSaleAsync()
        {
            var sale = await _context.Sale.ToListAsync();
            return _mapper.Map<IEnumerable<SalaDTO>>(sale);
        }

        public async Task<SalaDTO> UpdateSalaAsync(uint id, SalaDTO salaDTO)
        {
            var salaToUpdate = await _context.Sale.FindAsync(id);
            if (salaToUpdate == null)
                return null;

            _mapper.Map(salaDTO, salaToUpdate);
            _context.Entry(salaToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<SalaDTO>(salaToUpdate);
        }

        public async Task<bool> DeleteSalaAsync(uint id)
        {
            var sala = await _context.Sale.FindAsync(id);
            if (sala == null)
                return false;

            _context.Sale.Remove(sala);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
