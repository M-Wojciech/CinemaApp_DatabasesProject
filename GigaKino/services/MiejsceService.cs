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
    public class MiejsceService : IMiejsceService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public MiejsceService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MiejsceDTO> CreateMiejsceAsync(MiejsceDTO miejsceDTO)
        {
            var miejsce = _mapper.Map<Miejsce>(miejsceDTO);
            _context.Miejsca.Add(miejsce);
            await _context.SaveChangesAsync();
            return _mapper.Map<MiejsceDTO>(miejsce);
        }

        public async Task<MiejsceDTO> GetMiejsceByIdAsync(uint id)
        {
            var miejsce = await _context.Miejsca.FindAsync(id);
            return _mapper.Map<MiejsceDTO>(miejsce);
        }

        public async Task<IEnumerable<MiejsceDTO>> GetAllMiejscaAsync()
        {
            var miejsca = await _context.Miejsca.ToListAsync();
            return _mapper.Map<IEnumerable<MiejsceDTO>>(miejsca);
        }

        public async Task<MiejsceDTO> UpdateMiejsceAsync(uint id, MiejsceDTO miejsceDTO)
        {
            var miejsceToUpdate = await _context.Miejsca.FindAsync(id);
            if (miejsceToUpdate == null)
                return null;

            _mapper.Map(miejsceDTO, miejsceToUpdate);
            await _context.SaveChangesAsync();
            return _mapper.Map<MiejsceDTO>(miejsceToUpdate);
        }

        public async Task<bool> DeleteMiejsceAsync(uint id)
        {
            var miejsce = await _context.Miejsca.FindAsync(id);
            if (miejsce == null)
                return false;

            _context.Miejsca.Remove(miejsce);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
