namespace GigaKino.ObjectsDTO
{
    public class PracownikDTO
    {
        public uint IdPracownik { get; set; }

        public required string Imie { get; set; }

        public required string Nazwisko { get; set; }

        public DateTime DataUrodzenia { get; set; }

        public required string Stanowisko { get; set; }

        public uint? IdKonto { get; set; }

        public KontoDTO? Konto { get; set; }
    }
}
