namespace GigaKino.ObjectsDTO
{
    public class BiletDTO
    {
        public uint IdBilet { get; set; }
        
        public int CenaFaktyczna { get; set; }
        
        public string? Ulga { get; set; }
        
        public uint IdSeans { get; set; }
        
        public uint IdMiejsce { get; set; }
        
        public uint IdTransakcja { get; set; }

        public required SeansDTO Seans { get; set; }
        
        public required MiejsceDTO Miejsce { get; set; }
        
        public required TransakcjaDTO Transakcja { get; set; }
    }
}
