using CRUDWithFeature.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CRUDWithFeature.Dto_s.Product
{
    public class ProductDtoValidation: AbstractValidator<ProductDto>
    {
        private readonly ApplicationDbContext context;

        public ProductDtoValidation(ApplicationDbContext context ) {
            this.context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is Required")
                .MinimumLength(5).WithMessage("Name must be at least 5 character")
                .MaximumLength(30).WithMessage("Name can't exceed 30 characters")
                .MustAsync(async(Name, cancellation) =>
                {
                    return await Unique(Name);
                }).WithMessage("Name must be Unique");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is Required")
                .MinimumLength(10).WithMessage("Description must be at least 10 character");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is Required")
                .InclusiveBetween(20, 3000).WithMessage("The price must be between 20 and 3000");
        }
        private async Task<bool> Unique(string Name)
        {
            return !await context.Products.AnyAsync(p => p.Name == Name);
        }

    }
}
