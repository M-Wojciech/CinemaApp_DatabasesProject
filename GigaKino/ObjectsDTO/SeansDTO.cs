namespace GigaKino.ObjectsDTO
{
    public class SeansDTO
    {
        public uint IdSeans { get; set; }

        public DateTime Termin { get; set; }

        public required string Technologia { get; set; }

        public required string WersjaJezykowa { get; set; }

        public int CenaDomyslna { get; set; }

        public uint IdFilm { get; set; }

        public uint IdSala { get; set; }

        public required FilmDTO Film { get; set; }

        public required SalaDTO Sala { get; set; }
    }
}
