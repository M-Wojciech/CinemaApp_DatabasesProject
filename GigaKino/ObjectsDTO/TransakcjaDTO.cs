namespace GigaKino.ObjectsDTO
{
    public class TransakcjaDTO
    {
        public uint IdTransakcja { get; set; }

        public int? CenaLaczna { get; set; }

        public DateTime CzasRozpoczecia { get; set; }

        public DateTime? CzasZakupu { get; set; }

        public bool Status { get; set; }

        public uint? IdKlient { get; set; }

        public required KlientDTO Klient { get; set; }
    }
}
