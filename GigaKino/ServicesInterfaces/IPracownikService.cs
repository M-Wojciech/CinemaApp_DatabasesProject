using GigaKino.Models;

namespace GigaKino.ServicesInterfaces
{
    public interface IPracownikService
    {
        Task<Pracownik> CreatePracownikAsync(Pracownik pracownik);
        Task<Pracownik> GetPracownikByIdAsync(uint id);
        Task<IEnumerable<Pracownik>> GetAllPracownicyAsync();
        Task<bool> UpdatePracownikAsync(Pracownik pracownik);
        Task<bool> DeletePracownikAsync(uint id);
    }
}
