namespace GigaKino.ObjectsDTO
{
    public class KontoDTO
    {
        public uint IdKonto { get; set; }

        public required string Typ { get; set; }

        public required string Login { get; set; }

        public required string Haslo { get; set; }

        public required string Sol { get; set; }
    }
}
