using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    [Table("Bilet")]
    public class Bilet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdBilet { get; set; }

        [Required]
        public int Cena_Faktyczna { get; set; }

        [StringLength(20)]
        public string? Ulga { get; set; }

        [ForeignKey("Seans")]
        public uint IdSeans { get; set; }

        [ForeignKey("Miejsce")]
        public uint IdMiejsce { get; set; }

        [ForeignKey("Transakcja")]
        public uint IdTransakcja { get; set; }
        public virtual required Seans Seans { get; set; }
        public virtual required Miejsce Miejsce { get; set; }
        public virtual required Transakcja Transakcja { get; set; }
    }
}
