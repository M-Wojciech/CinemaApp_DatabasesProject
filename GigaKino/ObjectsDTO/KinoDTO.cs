namespace GigaKino.ObjectsDTO
{
    public class KinoDTO
    {
        public uint IdKino { get; set; }

        public required string Nazwa { get; set; }

        public required string Miasto { get; set; }

        public required string Ulica { get; set; }

        public required string NumerBudynku { get; set; }
    }
}
