using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    [Table("Kino")]
    public class Kino
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdKino { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nazwa { get; set; }

        [Required]
        [StringLength(30)]
        public required string Miasto { get; set; }

        [Required]
        [StringLength(30)]
        public required string Ulica { get; set; }

        [Required]
        [StringLength(5)]
        public required string Numer_Budynku { get; set; }
    }
}
