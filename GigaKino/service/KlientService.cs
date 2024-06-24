using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class KlientService : IKlientService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public KlientService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KlientDTO?> CreateKlientAsync(KlientDTO klientDTO)
        {
            try
            {
                var klient = _mapper.Map<Klient>(klientDTO);
                _context.Klienci.Add(klient);
                await _context.SaveChangesAsync();
                return _mapper.Map<KlientDTO>(klient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateKlientAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<KlientDTO?> GetKlientByIdAsync(uint id)
        {
            try
            {
                var klient = await _context.Klienci.FindAsync(id);
                return _mapper.Map<KlientDTO>(klient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetKlientByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<KlientDTO>?> GetAllKlienciAsync()
        {
            try
            {
                var klienci = await _context.Klienci.ToListAsync();
                return _mapper.Map<IEnumerable<KlientDTO>>(klienci);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllKlienciAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<KlientDTO?> UpdateKlientAsync(uint id, KlientDTO klientDTO)
        {
            try
            {
                var existingKlient = await _context.Klienci.FindAsync(id);
                if (existingKlient == null)
                {
                    Console.WriteLine("UpdateKlientAsync - no such record in database");
                    return null;
                }

                _mapper.Map(klientDTO, existingKlient);
                _context.Klienci.Update(existingKlient);
                await _context.SaveChangesAsync();
                return _mapper.Map<KlientDTO>(existingKlient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateKlientAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteKlientAsync(uint id)
        {
            try
            {
                var klient = await _context.Klienci.FindAsync(id);
                if (klient == null)
                    return false;

                _context.Klienci.Remove(klient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteKlientAsync failed:"+ ex);
                return null;
            }
        }

        /*public async Task<KlientDTO?> RegisterKlientAsync(KlientDTO klientDTO)
        {
            
            try
            {
                if (klientDTO.Konto == null || string.IsNullOrWhiteSpace(klientDTO.Konto.Haslo))
                {
                    throw new ArgumentException("Password is required for registration");
                }

           

                var konto = new Konto
                {
                    Login = klientDTO.Mail,
                    Haslo = klientDTO.Konto.Haslo,
                    Typ = klientDTO.Konto.Typ,
                    Sol = klientDTO.Konto.Haslo
                };

                var klient = _mapper.Map<Klient>(klientDTO);
                klient.Konto = konto;

                _context.Klienci.Add(klient);
                await _context.SaveChangesAsync();

                return _mapper.Map<KlientDTO>(klient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("RegisterKlientAsync failed:" + ex);
                return null;
            }
        }*/

        public void AddKlient(Klient klient)
        {
            _context.Klienci.Add(klient);
            _context.SaveChanges();
        }
    }
}
