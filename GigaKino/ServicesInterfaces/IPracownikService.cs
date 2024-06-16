using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IPracownikService
    {
        Task<PracownikDTO?> CreatePracownikAsync(PracownikDTO pracownikDTO);
        Task<PracownikDTO?> GetPracownikByIdAsync(uint id);
        Task<IEnumerable<PracownikDTO>?> GetAllPracownicyAsync();
        Task<PracownikDTO?> UpdatePracownikAsync(uint id, PracownikDTO pracownikDTO);
        Task<bool?> DeletePracownikAsync(uint id);
    }
}
