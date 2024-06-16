using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<MiejsceDTO?> CreateMiejsceAsync(MiejsceDTO miejsceDTO)
        {
            try
            {
                var miejsce = _mapper.Map<Miejsce>(miejsceDTO);
                _context.Miejsca.Add(miejsce);
                await _context.SaveChangesAsync();
                return _mapper.Map<MiejsceDTO>(miejsce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateMiejsceAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<MiejsceDTO?> GetMiejsceByIdAsync(uint id)
        {
            try
            {
                var miejsce = await _context.Miejsca.FindAsync(id);
                return _mapper.Map<MiejsceDTO>(miejsce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetMiejsceByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<MiejsceDTO>?> GetAllMiejscaAsync()
        {
            try
            {
                var miejsca = await _context.Miejsca.ToListAsync();
                return _mapper.Map<IEnumerable<MiejsceDTO>>(miejsca);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllMiejscaAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<MiejsceDTO?> UpdateMiejsceAsync(uint id, MiejsceDTO miejsceDTO)
        {
            try
            {
                var existingMiejsce = await _context.Miejsca.FindAsync(id);
                if (existingMiejsce == null)
                {
                    Console.WriteLine("UpdateMiejsceAsync - no such record in database");
                    return null;
                }

                _mapper.Map(miejsceDTO, existingMiejsce);
                _context.Miejsca.Update(existingMiejsce);
                await _context.SaveChangesAsync();
                return _mapper.Map<MiejsceDTO>(existingMiejsce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateMiejsceAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteMiejsceAsync(uint id)
        {
            try
            {
                var miejsce = await _context.Miejsca.FindAsync(id);
                if (miejsce == null)
                    return false;

                _context.Miejsca.Remove(miejsce);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteMiejsceAsync failed:"+ ex);
                return null;
            }
        }
    }
}
