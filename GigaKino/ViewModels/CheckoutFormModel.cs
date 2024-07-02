using System.ComponentModel.DataAnnotations;

namespace GigaKino.ViewModels
{
    public class CheckoutFormModel
    {
        [Required]
        public uint IdSeans { get; set; }

        [Required]
        public int CenaDomyslna { get; set; }

        [Required]
        public int CenaLaczna { get; set; }

        [Required]
        public List<uint> SelectedSeats { get; set; } = new();

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }
    }
}
