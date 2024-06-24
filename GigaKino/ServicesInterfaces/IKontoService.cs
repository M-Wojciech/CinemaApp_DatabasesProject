using GigaKino.Models;
using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IKontoService
    {
        Task<KontoDTO?> CreateKontoAsync(KontoDTO kontoDTO);
        Task<KontoDTO?> GetKontoByIdAsync(uint id);
        Task<IEnumerable<KontoDTO>?> GetAllKontaAsync();
        Task<KontoDTO?> UpdateKontoAsync(uint id, KontoDTO kontoDTO);
        Task<bool?> DeleteKontoAsync(uint id);
        string GenerateSalt();
        string HashPassword(string password, string salt);
        void AddKonto(Konto konto);
        bool UserExists(string login);
    }
}
