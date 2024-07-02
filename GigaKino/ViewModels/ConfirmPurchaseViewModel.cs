using GigaKino.ObjectsDTO;
using System.ComponentModel.DataAnnotations;

namespace GigaKino.ViewModels
{
    public class ConfirmPurchaseViewModel
    {
        public SeansDTO Seans { get; set; }
        public FilmDTO Film { get; set; }
        public SalaDTO Sala { get; set; }
        public KinoDTO Kino { get; set; }
        public List<MiejsceDTO> WybraneMiejsca { get; set; }
    }
}
