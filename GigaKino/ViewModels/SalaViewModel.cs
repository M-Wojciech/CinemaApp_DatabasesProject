using GigaKino.ObjectsDTO;
using System.Collections.Generic;

namespace GigaKino.ViewModels
{
    public class SalaViewModel
    {
        public SeansDTO Seans { get; set; }
        public IEnumerable<MiejsceDTO> Miejsca { get; set; }
        public HashSet<uint> ZajeteMiejsca { get; set; }
        public int Quantity { get; set; }
    }
}
