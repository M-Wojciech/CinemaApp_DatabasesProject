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

        public async Task<FilmDTO> CreateFilmAsync(FilmDTO filmDTO)
        {
            var film = _mapper.Map<Film>(filmDTO);
            _context.Filmy.Add(film);
            await _context.SaveChangesAsync();
            return _mapper.Map<FilmDTO>(film);
        }

        public async Task<FilmDTO> GetFilmByIdAsync(uint id)
        {
            var film = await _context.Filmy.FindAsync(id);
            return _mapper.Map<FilmDTO>(film);
        }
        public async Task<IEnumerable<FilmDTO>> GetAllFilmsAsync()
        {
            var filmy = await _context.Filmy.ToListAsync();
            return _mapper.Map<IEnumerable<FilmDTO>>(filmy);
        }

        public async Task<FilmDTO> GetFilmByTitleAsync(string title)
        {
            var film = await _context.Filmy.FirstOrDefaultAsync(f => f.Tytul == title);
            return _mapper.Map<FilmDTO>(film);
        }

        public async Task<bool> DeleteFilmAsync(uint id)
        {
            var film = await _context.Filmy.FindAsync(id);
            if (film == null)
                return false;
            
            _context.Filmy.Remove(film);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}