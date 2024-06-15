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

        public async Task<TransakcjaDTO> CreateTransakcjaAsync(TransakcjaDTO transakcjaDTO)
        {
            var transakcja = _mapper.Map<Transakcja>(transakcjaDTO);
            _context.Transakcje.Add(transakcja);
            await _context.SaveChangesAsync();
            return _mapper.Map<TransakcjaDTO>(transakcja);
        }

        public async Task<TransakcjaDTO> GetTransakcjaByIdAsync(uint id)
        {
            var transakcja = await _context.Transakcje.FindAsync(id);
            return _mapper.Map<TransakcjaDTO>(transakcja);
        }

        public async Task<IEnumerable<TransakcjaDTO>> GetAllTransakcjeAsync()
        {
            var transakcje = await _context.Transakcje.ToListAsync();
            return _mapper.Map<IEnumerable<TransakcjaDTO>>(transakcje);
        }

        public async Task<TransakcjaDTO> UpdateTransakcjaAsync(uint id, TransakcjaDTO transakcjaDTO)
        {
            var transakcjaToUpdate = await _context.Transakcje.FindAsync(id);
            if (transakcjaToUpdate == null)
                return null;

            _mapper.Map(transakcjaDTO, transakcjaToUpdate);
            _context.Entry(transakcjaToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<TransakcjaDTO>(transakcjaToUpdate);
        }

        public async Task<bool> DeleteTransakcjaAsync(uint id)
        {
            var transakcja = await _context.Transakcje.FindAsync(id);
            if (transakcja == null)
                return false;

            _context.Transakcje.Remove(transakcja);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
