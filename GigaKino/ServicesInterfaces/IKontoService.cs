using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IKontoService
    {
        Task<KontoDTO> CreateKontoAsync(KontoDTO kontoDTO);
        Task<KontoDTO> GetKontoByIdAsync(uint id);
        Task<IEnumerable<KontoDTO>> GetAllKontaAsync();
        Task<KontoDTO> UpdateKontoAsync(uint id, KontoDTO kontoDTO);
        Task<bool> DeleteKontoAsync(uint id);
    }
}
