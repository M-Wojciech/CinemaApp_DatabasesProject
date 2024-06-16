using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface ISeansService
    {
        Task<SeansDTO?> CreateSeansAsync(SeansDTO seansDTO);
        Task<SeansDTO?> GetSeansByIdAsync(uint id);
        Task<IEnumerable<SeansDTO>?> GetAllSeanseAsync();
        Task<SeansDTO?> UpdateSeansAsync(uint id, SeansDTO seansDTO);
        Task<bool?> DeleteSeansAsync(uint id);
    }
}