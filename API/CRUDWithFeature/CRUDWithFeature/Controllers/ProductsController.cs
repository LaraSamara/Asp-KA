using CRUDWithFeature.Data;
using CRUDWithFeature.Dto_s.Product;
using CRUDWithFeature.Models;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CRUDWithFeature.Controllers
{
    [Route("Product/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost("/Create")]
        public async Task<IActionResult> CreateProduct(ProductDto Dto,[FromServices] IValidator<ProductDto> Validator)
        {
            var ProblemResult = await Validator.ValidateAsync(Dto);
            if (!ProblemResult.IsValid) {
                var ModelState = new ModelStateDictionary();
                ProblemResult.Errors.ForEach(error => 
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage)
                );
                return ValidationProblem(ModelState);
            }
            var Product = Dto.Adapt<Product>();
            await context.Products.AddAsync(Product);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), Product);
        }
        [HttpGet("/GetProduct")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            var Product = await context.Products.FindAsync(Id);
            if(Product == null)
            {
                return NotFound(new {Message = "Product Not Found"});
            }
            return Ok(Product.Adapt<GetProductDto>());
        }
        [HttpGet("/AllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var Products = await context.Products.ToListAsync();
            if(Products.Count == 0)
            {
                return NotFound(new { Message = "No Products Found" });
            }
            return Ok(Products.Adapt<IEnumerable<GetProductDto>>());
        }
        [HttpDelete("/Delete")]
        public async Task<IActionResult> Delete(int Id) {
            var Product = await context.Products.FindAsync(Id);
            if(Product == null ) {
                return NotFound(new { Message = "Product Not Found" });
            }
            context.Products.Remove(Product);
            await context.SaveChangesAsync();
            return Ok(new {Message = "Product Deleted Successfully"});
        }
        [HttpPut("/Update")]
        public async Task<IActionResult> Update(int Id, ProductDto Dto) {
            var Product = await context.Products.FindAsync(Id);
            if (Product == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }
            Product.Name = Dto.Name;
            Product.Description = Dto.Description;
            Product.Price = Dto.Price;
            await context.SaveChangesAsync();
            return Ok(new {Message = "Product Updated Successfully "});
        }
    }
}
