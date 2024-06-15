namespace GigaKino.ObjectsDTO
{
    public class FilmDTO
    {
        public uint IdFilm { get; set; }
        public required string Tytul { get; set; }
        public int Dlugosc { get; set; }
        public required string Gatunek { get; set; }
        public required string Rezyser { get; set; }
        public int Ogr_wiekowe { get; set; }
        public required string Trailer { get; set; }
        public required string Opis { get; set; }
    }
}
