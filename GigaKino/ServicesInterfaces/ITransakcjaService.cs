using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface ITransakcjaService
    {
        Task<TransakcjaDTO?> CreateTransakcjaAsync(TransakcjaDTO transakcjaDTO);
        Task<TransakcjaDTO?> GetTransakcjaByIdAsync(uint id);
        Task<IEnumerable<TransakcjaDTO>?> GetAllTransakcjeAsync();
        Task<TransakcjaDTO?> UpdateTransakcjaAsync(uint id, TransakcjaDTO transakcjaDTO);
        Task<bool?> DeleteTransakcjaAsync(uint id);
    }
}