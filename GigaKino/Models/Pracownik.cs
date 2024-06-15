using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    public class Pracownik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdPracownik { get; set; }

        [Required]
        [StringLength(30)]
        public required string Imie { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nazwisko { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataUrodzenia { get; set; }

        [Required]
        [StringLength(30)]
        public required string Stanowisko { get; set; }

        public uint? IdKonto { get; set; }

        [ForeignKey("IdKonto")]
        public Konto? Konto { get; set; }
    }
}
