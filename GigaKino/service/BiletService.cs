using System.CodeDom;
using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class BiletService : IBiletService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public BiletService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BiletDTO?> CreateBiletAsync(BiletDTO biletDTO)
        {
            try
            {
                var bilet = _mapper.Map<Bilet>(biletDTO);
                _context.Bilety.Add(bilet);
                await _context.SaveChangesAsync();
                return _mapper.Map<BiletDTO>(bilet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateBiletAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<BiletDTO?> GetBiletByIdAsync(uint id)
        {
            try
            {
                var bilet = await _context.Bilety.FindAsync(id);
                return _mapper.Map<BiletDTO>(bilet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetBiletByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<BiletDTO>?> GetAllBiletyAsync()
        {
            try
            {
                var bilety = await _context.Bilety.ToListAsync();
                return _mapper.Map<IEnumerable<BiletDTO>>(bilety);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllBiletyAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<BiletDTO?> UpdateBiletAsync(uint id, BiletDTO biletDTO)
        {
            try
            {
                var existingBilet = await _context.Bilety.FindAsync(id);
                if (existingBilet == null)
                {
                    Console.WriteLine("UpdateBiletAsync - no such record in database");
                    return null;
                }

                _mapper.Map(biletDTO, existingBilet);
                _context.Bilety.Update(existingBilet);
                await _context.SaveChangesAsync();
                return _mapper.Map<BiletDTO>(existingBilet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateBiletAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteBiletAsync(uint id)
        {
            try
            {
                var bilet = await _context.Bilety.FindAsync(id);
                if (bilet == null)
                    return false;

                _context.Bilety.Remove(bilet);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteBiletAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<IEnumerable<BiletDTO>> GetBiletBySeansIdAsync(uint idSeans)
        {
            return await _context.Bilety
                .Where(b => b.IdSeans == idSeans)
                .Include(b => b.Seans)
                    .ThenInclude(s => s.Film)
                .Include(b => b.Seans)
                    .ThenInclude(s => s.Sala)
                        .ThenInclude(s => s.Kino)
                .Include(b => b.Miejsce)
                .Include(b => b.Transakcja)
                    .ThenInclude(t => t.Klient)
                        .ThenInclude(k => k.Konto)
                .Select(b => new BiletDTO
                {
                    IdBilet = b.IdBilet,
                    CenaFaktyczna = b.Cena_Faktyczna,
                    Ulga = b.Ulga,
                    IdSeans = b.IdSeans,
                    IdMiejsce = b.IdMiejsce,
                    IdTransakcja = b.IdTransakcja,
                    Seans = new SeansDTO
                    {
                        IdSeans = b.Seans.IdSeans,
                        Termin = b.Seans.Termin,
                        Technologia = b.Seans.Technologia,
                        WersjaJezykowa = b.Seans.Wersja_Jezykowa,
                        CenaDomyslna = b.Seans.Cena_Domyslna,
                        IdFilm = b.Seans.IdFilm,
                        IdSala = b.Seans.IdSala,
                        Film = new FilmDTO
                        {
                            IdFilm = b.Seans.Film.IdFilm,
                            Tytul = b.Seans.Film.Tytul,
                            Dlugosc = b.Seans.Film.Dlugosc,
                            Gatunek = b.Seans.Film.Gatunek,
                            Rezyser = b.Seans.Film.Rezyser,
                            Ogr_wiekowe = b.Seans.Film.Ogr_wiekowe,
                            Trailer = b.Seans.Film.Trailer,
                            Opis = b.Seans.Film.Opis,
                            BannerPath = b.Seans.Film.BannerPath,
                            PosterPath = b.Seans.Film.PosterPath
                        },
                        Sala = new SalaDTO
                        {
                            IdSala = b.Seans.Sala.IdSala,
                            Numer = b.Seans.Sala.Numer,
                            IdKino = b.Seans.Sala.IdKino,
                            Kino = new KinoDTO
                            {
                                IdKino = b.Seans.Sala.Kino.IdKino,
                                Nazwa = b.Seans.Sala.Kino.Nazwa,
                                Miasto = b.Seans.Sala.Kino.Miasto,
                                Ulica = b.Seans.Sala.Kino.Ulica,
                                NumerBudynku = b.Seans.Sala.Kino.Numer_Budynku
                            }
                        }
                    },
                    Miejsce = new MiejsceDTO
                    {
                        IdMiejsce = b.Miejsce.IdMiejsce,
                        Rzad = b.Miejsce.Rzad,
                        Kolumna = b.Miejsce.Kolumna,
                        IdSala = b.Miejsce.IdSala
                    },
                    Transakcja = new TransakcjaDTO
                    {
                        IdTransakcja = b.Transakcja.IdTransakcja,
                        CenaLaczna = b.Transakcja.Cena_Laczna,
                        CzasRozpoczecia = b.Transakcja.Czas_Rozpoczecia,
                        CzasZakupu = b.Transakcja.Czas_Zakupu,
                        Status = b.Transakcja.Status,
                        IdKlient = b.Transakcja.IdKlient,
                        Klient = new KlientDTO
                        {
                            IdKlient = b.Transakcja.Klient.IdKlient,
                            Mail = b.Transakcja.Klient.Mail,
                            Imie = b.Transakcja.Klient.Imie,
                            Nazwisko = b.Transakcja.Klient.Nazwisko,
                            IdKonto = b.Transakcja.Klient.IdKonto,
                            Konto = b.Transakcja.Klient.Konto != null ? new KontoDTO
                            {
                                IdKonto = b.Transakcja.Klient.Konto.IdKonto,
                                Typ = b.Transakcja.Klient.Konto.Typ,
                                Login = b.Transakcja.Klient.Konto.Login,
                                Haslo = b.Transakcja.Klient.Konto.Haslo,
                                Sol = b.Transakcja.Klient.Konto.Sol
                            } : null
                        }
                    }

                })
                .ToListAsync();
        }
    }
}
