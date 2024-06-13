using GigaKino.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IFilmService
    {
        Task<FilmDTO> CreateFilmAsync(FilmDTO filmDTO);
        Task<FilmDTO> GetFilmByIdAsync(uint id);
        Task<IEnumerable<FilmDTO>> GetAllFilmsAsync();
        Task<FilmDTO> GetFilmByTitleAsync(string title);
        Task<bool> DeleteFilmAsync(uint id);
    }
}
