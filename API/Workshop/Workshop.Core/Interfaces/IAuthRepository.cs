using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Models;

namespace Workshop.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> SignupAsync(Users user, string Password); 
        Task<string> SigninAsync(string Email, string Password);
        Task<string>ChangePasswordAsync(string Email, string OldPassword, string NewPassword);
    }
}
