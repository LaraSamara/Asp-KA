using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.Helper;
using Workshop.Core.Interfaces;

namespace Workshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        //private readonly IInvoiceRepository invoiceRepository;
        private readonly IUnitOfWork unitOfWork;

        public InvoiceController(IUnitOfWork unitOfWork)
        {
            //invoiceRepository = InvoiceRepository;
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("/CreateInvoice")]
        public async Task<IActionResult> CreateInvoice()
        {
            var Token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if(string.IsNullOrEmpty(Token) )
            {
                return Unauthorized("Misisng Token!!");
            }
            var UserId = ExtractClaim.ExtractUserId(Token);
            if(UserId == null || !UserId.HasValue) {
                return Unauthorized("Invalid User Id");
            }
            var Result = await unitOfWork.InvoiceRepository.CreateInvoiceAsync(UserId.Value);
            if(Result.StartsWith("Success, "))
            {
                return Ok(Result);
            }
            return BadRequest(Result);
        }
        [HttpGet]
        public async Task<IActionResult> GetInvoiceRecipt(int InvoiceId)
        {
            var Token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if(string.IsNullOrEmpty(Token) )
            {
                return Unauthorized("Missing Token!!");
            }
            var UserId = ExtractClaim.ExtractUserId(Token);
            if (UserId == null || !UserId.HasValue)
            {
                return Unauthorized("Invalid User Id");
            }
            var result = await unitOfWork.InvoiceRepository.GetInvoiceRecipt(UserId.Value, InvoiceId);
            if(result == null)
            {
                return NotFound("Not Found!!");
            }
            return Ok(result);  
        }
    }
}
