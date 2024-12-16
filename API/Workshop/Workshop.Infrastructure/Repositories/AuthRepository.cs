using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workshop.Core.Interfaces;
using Workshop.Core.Models;

namespace Workshop.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signinManager;
        private readonly IConfiguration configuration;

        public AuthRepository(UserManager<Users> userManager, SignInManager<Users> signinManager, IConfiguration configuration )
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
            this.configuration = configuration;
        }

        public async Task<string> SignupAsync(Users user, string Password)
        {
            var Result = await userManager.CreateAsync(user, Password);
            if(Result.Succeeded)
            {
                return "User Registerd Successfully";
            }
            var ErrorMessages = Result.Errors.Select(e => e.Description).ToList();
            return String.Join(", ",ErrorMessages);
        }
        public async Task<string> SigninAsync(string Email, string Password)
        {
            var User = await userManager.FindByEmailAsync(Email);
            if (User == null)
            {
                return null;
            }
            var CheckPassword = await signinManager.PasswordSignInAsync(User, Password, false,false);
            if (!CheckPassword.Succeeded)
            {
                return null;
            }
            return GenerateToken(User);
        }
        public Task<string> ChangePasswordAsync(string Email, string OldPassword, string NewPassword)
        {
            throw new NotImplementedException();
        }
        private string GenerateToken(Users User) {
            var Claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, User.UserName),
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
               configuration["JWT: Issuer"],
               configuration["JWT: Audience"],
               Claims,
               signingCredentials: Credentials,
               expires:  DateTime.Now.AddMinutes(15)
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
