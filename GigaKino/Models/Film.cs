using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    [Table("Film")]
    public class Film
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdFilm")]
        public uint IdFilm { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Tytul")]
        public required string Tytul { get; set; }

        [Required]
        [Column("Dlugosc")]
        public int Dlugosc { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Gatunek")]
        public required string Gatunek { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Rezyser")]
        public required string Rezyser { get; set; }

        [Required]
        [Column("Ogr_Wiekowe")]
        public int Ogr_wiekowe { get; set; }

        [Required]
        [StringLength(200)]
        [Column("Trailer")]
        public required string Trailer { get; set; }

        [Required]
        [StringLength(1000)]
        [Column("Opis")]
        public required string Opis { get; set; }
    }
}
