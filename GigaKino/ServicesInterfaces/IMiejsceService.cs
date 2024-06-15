using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IMiejsceService
    {
        Task<MiejsceDTO> CreateMiejsceAsync(MiejsceDTO miejsceDTO);
        Task<MiejsceDTO> GetMiejsceByIdAsync(uint id);
        Task<IEnumerable<MiejsceDTO>> GetAllMiejscaAsync();
        Task<MiejsceDTO> UpdateMiejsceAsync(uint id, MiejsceDTO miejsceDTO);
        Task<bool> DeleteMiejsceAsync(uint id);
    }
}
