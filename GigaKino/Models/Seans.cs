using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    public class Seans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdSeans { get; set; }

        [Required]
        public DateTime Termin { get; set; }

        [Required]
        [StringLength(30)]
        public required string Technologia { get; set; }

        [Required]
        [StringLength(20)]
        public required string WersjaJezykowa { get; set; }

        [Required]
        public int CenaDomyslna { get; set; }

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
