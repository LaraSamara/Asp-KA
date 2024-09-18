using System.ComponentModel.DataAnnotations;

namespace Identity.Models.ViewModels
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress)]
        [MaxLength(40)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(40)]
        [Required]
        public  string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirme Password")]
        [MaxLength(40)]
        [Required]
        public string ConfirmedPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        [MaxLength(40)]
        [Required]
        public string Phone {  get; set; }
    }
}
