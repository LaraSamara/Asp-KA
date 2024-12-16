using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;

namespace Workshop.API.Controllers
{
    [Route("Workshop/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository itemsRepository;

        public ItemsController(IItemsRepository ItemsRepository)
        {
            itemsRepository = ItemsRepository;
        }
        [HttpGet]
        [Route("/GetItems")]
        public async  Task<IActionResult> Get() { 
            var Items = await itemsRepository.GetItemsAsync();
            if(Items == null)
            {
                return NotFound(new { Message = "No Items Founds"});
            }
            return Ok(new {Message = Items});
        }
    }
}
