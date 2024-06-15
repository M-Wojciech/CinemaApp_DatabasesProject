using AutoMapper;
using Database;
using GigaKino.Models;
using GigaKino.ObjectsDTO;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Services
{
    public class FilmService : IFilmService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public FilmService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FilmDTO?> CreateFilmAsync(FilmDTO filmDTO)
        {
            try
            {
                var film = _mapper.Map<Film>(filmDTO);
                _context.Filmy.Add(film);
                await _context.SaveChangesAsync();
                return _mapper.Map<FilmDTO>(film);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CrateFilmAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<FilmDTO?> GetFilmByIdAsync(uint id)
        {
            try
            {
                var film = await _context.Filmy.FindAsync(id);
                return _mapper.Map<FilmDTO>(film);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetFilmByIdAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<FilmDTO?> GetFilmByTitleAsync(string title)
        {
            try
            {
                var film = await _context.Filmy.FirstOrDefaultAsync(f => f.Tytul == title);
                return _mapper.Map<FilmDTO>(film);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetFilmByTitleAsync failed:"+ ex);
                return null;
            }
        }
        
        public async Task<IEnumerable<FilmDTO>?> GetAllFilmyAsync()
        {
            try
            {
                var filmy = await _context.Filmy.ToListAsync();
                return _mapper.Map<IEnumerable<FilmDTO>>(filmy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllFilmyAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<FilmDTO?> UpdateFilmAsync(uint id, FilmDTO filmDTO)
        {
            try
            {
                var existingFilm = await _context.Filmy.FindAsync(id);
                if (existingFilm == null)
                {
                    Console.WriteLine("UpdateFilmAsync - no such record in database");
                    return null;
                }

                _mapper.Map(filmDTO, existingFilm);
                _context.Filmy.Update(existingFilm);
                await _context.SaveChangesAsync();
                return _mapper.Map<FilmDTO>(existingFilm);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateFilmAsync failed:"+ ex);
                return null;
            }
        }

        public async Task<bool?> DeleteFilmAsync(uint id)
        {
            try
            {
                var film = await _context.Filmy.FindAsync(id);
                if (film == null)
                    return false;

                _context.Filmy.Remove(film);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteFilmAsync failed:"+ ex);
                return null;
            }
        }
    }
}