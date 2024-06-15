using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    public class Sala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdSala { get; set; }

        [Required]
        public int Numer { get; set; }

        [Required]
        public uint IdKino { get; set; }

        [ForeignKey("IdKino")]
        public required Kino Kino { get; set; }
    }
}
