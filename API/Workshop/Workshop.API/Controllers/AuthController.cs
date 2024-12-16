using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Dto_s.Auth;
using Workshop.Core.Interfaces;
using Workshop.Core.Models;

namespace Workshop.API.Controllers
{
    [Route("Workshop/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository AuthRepository)
        {
            authRepository = AuthRepository;
        }
        [Route("/SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var User = new Users()
            {
                Email = Dto.Email,
                UserName = Dto.UserName,
                Goverments_Id = Dto.Goverments_Id,
                Cities_Id = Dto.Cities_Id,
                Zones_Id = Dto.Zones_Id,
                Classifications_Id = Dto.Classifications_Id,
            };
            var Result = await authRepository.SignupAsync(User, Dto.Password);
            return new JsonResult (Result);
        }
        [Route("/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInDto Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Token = await authRepository.SigninAsync(Dto.Email, Dto.Password);
                if (Token == null)
                {
                    return Unauthorized(new { Message = "Invalid Data" });
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
    
            }
        }
    }
}
