using System.ComponentModel.DataAnnotations;

namespace Identity.Models.ViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        [MaxLength(40)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(40)]
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
