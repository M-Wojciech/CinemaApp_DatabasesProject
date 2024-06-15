using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface ISalaService
    {
        Task<SalaDTO> CreateSalaAsync(SalaDTO salaDTO);
        Task<SalaDTO> GetSalaByIdAsync(uint id);
        Task<IEnumerable<SalaDTO>> GetAllSaleAsync();
        Task<SalaDTO> UpdateSalaAsync(uint id, SalaDTO salaDTO);
        Task<bool> DeleteSalaAsync(uint id);
    }
}
