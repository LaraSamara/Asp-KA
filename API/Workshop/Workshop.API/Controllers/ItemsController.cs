using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;

namespace Workshop.API.Controllers
{
    [Route("Workshop/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        //private readonly IItemsRepository itemsRepository;
        private readonly IUnitOfWork unitOfWork;

        public ItemsController(IUnitOfWork unitOfWork)
        {
            //itemsRepository = ItemsRepository;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("/GetItems")]
        public async  Task<IActionResult> Get(int PageIndex, int PageSize) { 
            var Items = await unitOfWork.ItemsRepository.GetItemsAsync(PageIndex, PageSize);
            if(Items == null)
            {
                return NotFound(new { Message = "No Items Founds"});
            }
            return Ok(new {Message = Items});
        }
    }
}
