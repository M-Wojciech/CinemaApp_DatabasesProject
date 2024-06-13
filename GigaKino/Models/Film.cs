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
        public string Tytul { get; set; }

        [Required]
        [Column("Dlugosc")]
        public int Dlugosc { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Gatunek")]
        public string Gatunek { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Rezyser")]
        public string Rezyser { get; set; }

        [Required]
        [Column("OgrWiekowe")]
        public int OgrWiekowe { get; set; }

        [Required]
        [StringLength(200)]
        [Column("Trailer")]
        public string Trailer { get; set; }

        [Required]
        [StringLength(1000)]
        [Column("Opis")]
        public string Opis { get; set; }
    }
}
