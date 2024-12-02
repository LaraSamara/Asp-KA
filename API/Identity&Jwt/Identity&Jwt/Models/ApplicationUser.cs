using Microsoft.AspNetCore.Identity;

namespace Identity_Jwt.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; } 
        public string? Address { get; set; }
    }
}
