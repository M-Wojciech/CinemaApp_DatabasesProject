using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    [Table("Miejsce")]
    public class Miejsce
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdMiejsce { get; set; }

        [Required]
        public int Rzad { get; set; }

        [Required]
        public int Kolumna { get; set; }

        [Required]
        public uint IdSala { get; set; }

        [ForeignKey("IdSala")]
        public required Sala Sala { get; set; }
    }
}
