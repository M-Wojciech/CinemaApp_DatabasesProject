using GigaKino.Models;

namespace GigaKino.ObjectsDTO
{
    
    public class RepertuarItemDTO (uint idFilm, string tytul, int ogr_wiekowe, string posterPath, string gatunek, int dlugosc, string wersjaTechnologiczna, string wersjaJezykowa, uint idSeans, DateTime termin)
    {
        public  uint IdFilm { get; set; } = idFilm;
        public  string Tytul { get; set; } = tytul;
        public int Ogr_wiekowe { get; set; } = ogr_wiekowe;
        public  string PosterPath { get; set; } = posterPath;
        public  string Gatunek { get; set; } = gatunek;
        public  int Dlugosc { get; set; } = dlugosc;
        public  List<WersjaTechnologiczna> ListaWersjiTechnologicznych { get; set; } = [new (wersjaTechnologiczna, wersjaJezykowa, idSeans, termin)];
    }

    public class WersjaTechnologiczna(string technologia, string wersjaJezykowa, uint idSeans, DateTime termin)
    {
        public  string Wersja { get; set; } = technologia;
        public  List<WersjaJezykowa> ListaWersjiJezykowych { get; set; } = [new(wersjaJezykowa, idSeans, termin)];
    }

    public class WersjaJezykowa (string wersjaJezykowa, uint idSeans, DateTime termin)
    {
        public  string Wersja { get; set; } = wersjaJezykowa;
        public  List<Repertuar_Seans> ListaSeansow { get; set; } = [new(idSeans, termin)];
    }

    public class Repertuar_Seans (uint idSeans, DateTime termin)
    {
        public  uint IdSeans { get; set; } = idSeans;
        public  DateTime Termin { get; set; } = termin;
    }
}
