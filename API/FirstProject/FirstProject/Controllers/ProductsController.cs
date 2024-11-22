using FirstProject.Data;
using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    [Route("Products/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("/GetAll")]
        public IActionResult GetAll()
        {
            var products = context.Products.ToList();   
            return Ok(products);
        }
        [HttpGet("/GetProduct")]
        public IActionResult Get(int Id) {
            var product  = context.Products.Find(Id);
            if(product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost("/Create")]
        public IActionResult Create(Product request)
        {
            context.Products.Add(request);
            context.SaveChanges();
            return CreatedAtAction(nameof(Get), request);
        }
        [HttpPut("/Update")]
        public IActionResult Update(int Id, Product request) {
            var product = context.Products.Find(Id);
            if(product is null)
            {
                return NotFound();
            }
            product.Name = request.Name;
            product.Description = request.Description;
            context.SaveChanges();
            return NoContent(); 
        }
        [HttpDelete("/Delete")]
        public IActionResult Delete(int Id)
        {
            var product = context.Products.Find(Id);
            if(product is null) {
                return NotFound();
            }
            context.Products.Remove(product);
            context.SaveChanges();
            return NoContent();
        }
    }
}
