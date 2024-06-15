namespace GigaKino.ObjectsDTO
{
    public class SalaDTO
    {
        public uint IdSala { get; set; }

        public int Numer { get; set; }

        public uint IdKino { get; set; }

        public required KinoDTO Kino { get; set; }
    }
}
