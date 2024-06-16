using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IKinoService
    {
        Task<KinoDTO?> CreateKinoAsync(KinoDTO kinoDTO);
        Task<KinoDTO?> GetKinoByIdAsync(uint id);
        Task<IEnumerable<KinoDTO>?> GetAllKinaAsync();
        Task<KinoDTO?> UpdateKinoAsync(uint id, KinoDTO kinoDTO);
        Task<bool?> DeleteKinoAsync(uint id);
    }
}
