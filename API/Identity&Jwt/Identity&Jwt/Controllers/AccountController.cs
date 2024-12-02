using FluentValidation;
using Identity_Jwt.Dto_s.Identity;
using Identity_Jwt.Identity;
using Identity_Jwt.Models;
using Identity_Jwt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Identity_Jwt.Controllers
{
    [Route("Auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationIdentityDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AuthServices Auth;

        public AccountController(ApplicationIdentityDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, AuthServices Auth) {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.Auth = Auth;
        }
        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterDto Dto, [FromServices]IValidator<RegisterDto> Validator)
        {
            var ValidationResult = await Validator.ValidateAsync(Dto);
            if(!ValidationResult.IsValid)
            {
                var ModelStates = new ModelStateDictionary();
                ValidationResult.Errors.ForEach(error => 
                ModelStates.AddModelError(error.PropertyName, error.ErrorMessage)
                );
                return ValidationProblem(ModelStates);
            }
            var User = new ApplicationUser()
            {
                DisplayName = Dto.DisplayName,
                UserName = Dto.UserName,
                Email = Dto.Email,
                Address = Dto.Address,
            };
            var Result = await userManager.CreateAsync(User, Dto.Password);
            if (Result.Succeeded)
            {
                return Ok(new { Message = "Success " });
            }
            return BadRequest(new { Message = "Somthing Happend Error" });
        }
        [HttpPost("/Signin")]
        public async Task<IActionResult> Signin(SigninDto Dto, [FromServices] IValidator<SigninDto> Validator)
        {
            var ValidationResult = await Validator.ValidateAsync(Dto);
            if(!ValidationResult.IsValid)
            {
                var ModelStates = new ModelStateDictionary();
                ValidationResult.Errors.ForEach(error => 
                ModelStates.AddModelError(error.PropertyName, error.ErrorMessage));
                return ValidationProblem(ModelStates);
            }
            var User = await userManager.FindByEmailAsync(Dto.Email);
            if(User == null)
            {
                return Unauthorized(new {Message = "Invalid Data"});
            }
            var Result = await signInManager.CheckPasswordSignInAsync(User, Dto.Password, false);
            if(Result.Succeeded)
            {
                return Ok(new {
                    Message = "Success",
                    Token = await Auth.CreateTokenAsync(User,userManager)
                });
            }
            return Unauthorized(new {Message = "Invalid Data"});
        }
    }
}
