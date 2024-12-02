using Identity_Jwt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity_Jwt.Services
{
    public class AuthServices
    {
        private readonly IConfiguration configuration;

        public AuthServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(ApplicationUser User, UserManager<ApplicationUser> userManager)
        {
            //Private Claim
            var AuthClaims = new List<Claim>() {
                new Claim(ClaimTypes.GivenName, User.UserName),
                new Claim(ClaimTypes.Email, User.Email),
            };
            var UserRole = await userManager.GetRolesAsync(User);
            foreach (var Role in UserRole)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, Role));
            }
            //Registered Claim(Optional)
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("jwt")["SecretKey"]));
            var Token = new JwtSecurityToken(
                // Optional
                audience: "Website Users",
                // OPtional
                issuer: "Token Maker",
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature),
                expires: DateTime.UtcNow.AddDays(1)
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
