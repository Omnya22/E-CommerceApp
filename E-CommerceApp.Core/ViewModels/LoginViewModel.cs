using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.Core.ViewModels
{
    public class LoginViewModel
    {
        [Required, StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
