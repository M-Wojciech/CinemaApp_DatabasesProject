using System.ComponentModel.DataAnnotations;

namespace GigaKino.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(60)]
        public string Email { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
