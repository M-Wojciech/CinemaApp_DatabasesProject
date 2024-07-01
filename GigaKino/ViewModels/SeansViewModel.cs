using GigaKino.ObjectsDTO;

namespace GigaKino.ViewModels
{
    public class SeansViewModel
    {
        public SeansDTO Seans { get; set; }
        public FilmDTO Film { get; set; }
        public SalaDTO Sala { get; set; }
        public KinoDTO Kino { get; set; }
        public IEnumerable<MiejsceDTO> Miejsca { get; set; }
        public IEnumerable<BiletDTO> Bilet { get; set; }
        public int FreeMiejscaCount { get; set; }
    }
}
