using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaKino.Models
{
    public class Transakcja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint IdTransakcja { get; set; }

        public int? CenaLaczna { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CzasRozpoczecia { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CzasZakupu { get; set; }

        [Required]
        public bool Status { get; set; }

        public uint? IdKlient { get; set; }

        [ForeignKey("IdKlient")]
        public required Klient Klient { get; set; }
    }
}
