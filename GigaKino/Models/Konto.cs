using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    public class Konto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdKonto { get; set; }

        [Required]
        [StringLength(20)]
        public required string Typ { get; set; }

        [Required]
        [StringLength(30)]
        public required string Login { get; set; }

        [Required]
        [StringLength(64)]
        public required string Haslo { get; set; }

        [Required]
        [StringLength(30)]
        public required string Sol { get; set; }
    }
}
