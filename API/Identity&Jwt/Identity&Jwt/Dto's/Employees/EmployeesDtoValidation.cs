using FluentValidation;
using Identity_Jwt.Data;

namespace Identity_Jwt.Dto_s.Employees
{
    public class EmployeesDtoValidation: AbstractValidator<EmployeesDto>
    {
        private readonly ApplicationDbContext context;

        public EmployeesDtoValidation(ApplicationDbContext context)
        {
            this.context = context;
            RuleFor(E => E.Name).NotEmpty()
                .WithMessage("Name is Required")
                .MinimumLength(3).WithMessage("Minimum Length is 3")
                .MaximumLength(10).WithMessage("Maximum length is 10");

            RuleFor(E => E.Description).NotEmpty()
                .WithMessage("Description is Required")
                .MinimumLength(10).WithMessage("Minimum Length is 10")
                .MaximumLength(40).WithMessage("Maximum length is 40");

            RuleFor(E => E.Department_Id).NotEmpty()
                .MustAsync(async (Id, Cancellation) =>
                {
                    return await IsExist(Id);
                }).WithMessage("Department Id Not exist");
        }
        private async ValueTask<bool> IsExist(int Id)
        {
            var Department = await context.Departments.FindAsync(Id);
            return Department != null;
        }
    }
}
