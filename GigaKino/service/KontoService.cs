using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class KontoService : IKontoService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public KontoService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<KontoDTO?> CreateKontoAsync(KontoDTO kontoDTO)
        {
            try
            {
                var konto = _mapper.Map<Konto>(kontoDTO);
                _context.Konta.Add(konto);
                await _context.SaveChangesAsync();
                return _mapper.Map<KontoDTO>(konto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateKontoAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<KontoDTO?> GetKontoByIdAsync(uint id)
        {
            try
            {
                var konto = await _context.Konta.FindAsync(id);
                return _mapper.Map<KontoDTO>(konto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetKontoByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<KontoDTO>?> GetAllKontaAsync()
        {
            try
            {
                var konta = await _context.Konta.ToListAsync();
                return _mapper.Map<IEnumerable<KontoDTO>>(konta);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllKontaAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<KontoDTO?> UpdateKontoAsync(uint id, KontoDTO kontoDTO)
        {
            try
            {
                var existingKonto = await _context.Konta.FindAsync(id);
                if (existingKonto == null)
                {
                    Console.WriteLine("UpdateKontoAsync - no such record in database");
                    return null;
                }

                _mapper.Map(kontoDTO, existingKonto);
                _context.Konta.Update(existingKonto);
                await _context.SaveChangesAsync();
                return _mapper.Map<KontoDTO>(existingKonto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateKontoAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteKontoAsync(uint id)
        {
            try
            {
                var konto = await _context.Konta.FindAsync(id);
                if (konto == null)
                    return false;

                _context.Konta.Remove(konto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteKontoAsync failed:"+ ex);
                return null;
            }
        }
    }
}
