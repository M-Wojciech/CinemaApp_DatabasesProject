using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    [Table("Transakcja")]
    public class Transakcja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdTransakcja { get; set; }

        public int? Cena_Laczna { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Czas_Rozpoczecia { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Czas_Zakupu { get; set; }

        [Required]
        public bool Status { get; set; }

        public uint? IdKlient { get; set; }

        [ForeignKey("IdKlient")]
        public required Klient Klient { get; set; }
    }
}
