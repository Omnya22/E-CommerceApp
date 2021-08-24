using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(256)]
        public string UserName { get; set; }

        [Required, StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
