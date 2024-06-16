using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IFilmService
    {
        Task<FilmDTO?> CreateFilmAsync(FilmDTO filmDTO);
        Task<FilmDTO?> GetFilmByIdAsync(uint id);
        Task<IEnumerable<FilmDTO>?> GetAllFilmyAsync();
        Task<FilmDTO?> GetFilmByTitleAsync(string title);
        Task<FilmDTO?> UpdateFilmAsync(uint id, FilmDTO filmDTO);
        Task<bool?> DeleteFilmAsync(uint id);
    }
}
