using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IKlientService
    {
        Task<KlientDTO> CreateKlientAsync(KlientDTO klientDTO);
        Task<KlientDTO> GetKlientByIdAsync(uint id);
        Task<IEnumerable<KlientDTO>> GetAllKlienciAsync();
        Task<KlientDTO> UpdateKlientAsync(uint id, KlientDTO klientDTO);
        Task<bool> DeleteKlientAsync(uint id);
    }
}
