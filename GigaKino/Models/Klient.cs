using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    [Table("Klient")]
    public class Klient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdKlient { get; set; }

        [Required]
        [StringLength(60)]
        public required string Mail { get; set; }

        [StringLength(30)]
        public string? Imie { get; set; }

        [StringLength(50)]
        public string? Nazwisko { get; set; }

        public uint? IdKonto { get; set; }

        [ForeignKey("IdKonto")]
        public Konto? Konto { get; set; }
    }
}
