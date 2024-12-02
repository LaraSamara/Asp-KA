using System.ComponentModel.DataAnnotations;

namespace Identity_Jwt.Dto_s.Identity
{
    public class SigninDto
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }
        [Required]
        public string Password { get; set; }    
        
    }
}
