using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IBiletService
    {
        Task<BiletDTO> CreateBiletAsync(BiletDTO biletDTO);
        Task<BiletDTO> GetBiletByIdAsync(uint id);
        Task<IEnumerable<BiletDTO>> GetAllBiletyAsync();
        Task<bool> UpdateBiletAsync(uint id, BiletDTO biletDTO);
        Task<bool> DeleteBiletAsync(uint id);
    }
}