using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    [Table("Seans")]
    public class Seans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idSeans")]
        public uint IdSeans { get; set; }

        [Required]
        public DateTime Termin { get; set; }

        [Required]
        [StringLength(30)]
        public required string Technologia { get; set; }

        [Required]
        [StringLength(20)]
        public required string Wersja_Jezykowa { get; set; }

        [Required]
        public int Cena_Domyslna { get; set; }

        [Required]
        public uint IdFilm { get; set; }

        [Required]
        public uint IdSala { get; set; }

        [ForeignKey("IdFilm")]
        public required Film Film { get; set; }

        [ForeignKey("IdSala")]
        public required Sala Sala { get; set; }
    }
}
