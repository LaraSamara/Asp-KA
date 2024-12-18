using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Workshop.API.Helper;
using Workshop.Core.Dto_s.Cart;
using Workshop.Core.Interfaces;

namespace Workshop.API.Controllers
{
    [Route("Workshop/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        //private readonly ICartRepository cartRepository;
        private readonly IUnitOfWork unitOfWork;

        public CartsController(IUnitOfWork unitOfWork)
        {
            //cartRepository = CartRepository;
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("/AddToCart")]
        public async Task<IActionResult> AddBulkQuantityToCart(CartItemDto Dto)
        {
            var Token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (Token.IsNullOrEmpty())
            {
                return Unauthorized("Missing Token!!");
            }
            try
            {
                var UserId = ExtractClaim.ExtractUserId(Token);
                if(!UserId.HasValue)
                {
                    return Unauthorized("Invalid Token!!");
                }
                var Result = await unitOfWork.CartRepository.AddBulkQuantityToCartAsync(Dto, UserId.Value);
                if(Result.Equals("Item Add To Cart Successfully"))
                {
                    return Ok(new { Message = "Item Add To Cart Successfully" });
                }
                else
                {
                    return BadRequest(Result);
                }
            }catch (Exception ex)
            {
                return Unauthorized("Invalid Token " + ex.Message);
            }
        }
        [HttpPost]
        [Route("/UpdateQuantityByOne")]
        public async Task<IActionResult> AddOneQuantityToCartAsync(CartItemDto Dto)
        {
            var Token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if(Token.IsNullOrEmpty())
            {
                return Unauthorized("Token not Found");
            }
            var UserId = ExtractClaim.ExtractUserId(Token);
            if (!UserId.HasValue)
            {
                return Unauthorized("No Permetion!!!!");
            }
            try
            {
                var Result = await unitOfWork.CartRepository.AddOneQuantityToCartAsync(Dto, UserId.Value);
                if (Result.StartsWith("Success"))
                {
                    return Ok(Result);
                }
                else
                {
                    return BadRequest(Result);
                }
            }catch(Exception ex)
            {
                return Unauthorized("Invalid Operation");
            }
        }
        [HttpGet]
        [Route("/GetCartItems")]
        public async Task<IActionResult> GetAllItemsFromCartAsync()
        {
            var Token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if(Token.IsNullOrEmpty())
            {
                return Unauthorized("Missing Token :(");
            }
            var UserId = ExtractClaim.ExtractUserId(Token);
            if(!UserId.HasValue)
            {
                return Unauthorized("Operation Not Available :(");
            }
            try
            {
                var Result = await unitOfWork.CartRepository.GetAllItemsFromCartAsync(UserId.Value);
                if (Result.Any())
                {
                    return Ok(Result);
                }
                return BadRequest("No Items Found:(");
            }
            catch (Exception ex)
            {
                return BadRequest("Server Error" + ex.Message);
            }
        }
    }
}
