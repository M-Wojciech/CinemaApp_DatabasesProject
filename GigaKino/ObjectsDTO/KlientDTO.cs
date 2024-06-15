namespace GigaKino.ObjectsDTO
{
    public class KlientDTO
    {
        public uint IdKlient { get; set; }

        public required string Mail { get; set; }

        public string? Imie { get; set; }

        public string? Nazwisko { get; set; }

        public uint? IdKonto { get; set; }

        public KontoDTO? Konto { get; set; }
    }
}
