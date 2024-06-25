using System.ComponentModel.DataAnnotations;

namespace GigaKino.ViewModels
{
    public class RegistrationViewModel
    {
        /*[Required]
        [StringLength(20)]
        public string Typ { get; set; }

        [Required]
        [StringLength(30)]
        public string Login { get; set; }*/

        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }

        /*[Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }*/

        [Required]
        [EmailAddress]
        [StringLength(60)]
        public string Mail { get; set; }

        [StringLength(30)]
        public string? Imie { get; set; }

        [StringLength(50)]
        public string? Nazwisko { get; set; }
    }
}
