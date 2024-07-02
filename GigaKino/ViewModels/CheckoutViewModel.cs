using GigaKino.ObjectsDTO;
using System.ComponentModel.DataAnnotations;

namespace GigaKino.ViewModels
{
    public class CheckoutViewModel
    {
        public SeansDTO Seans { get; set; }
        public List<MiejsceDTO> WybraneMiejsca { get; set; }
        public int CenaLaczna { get; set; }
    }
}
