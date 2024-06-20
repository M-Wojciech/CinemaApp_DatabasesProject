using GigaKino.ObjectsDTO;

namespace GigaKino.ServicesInterfaces
{
    public interface IRepertuarService
    {
        Task<IEnumerable<RepertuarItemDTO>?> GetRepertuarAsync(uint kinoId, string? tytulFilmu=null, string? gatunek=null, string? technologia=null, string? wersjaJezykowa=null);
    }
    
}
