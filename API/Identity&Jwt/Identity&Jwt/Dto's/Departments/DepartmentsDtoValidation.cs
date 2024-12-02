using FluentValidation;

namespace Identity_Jwt.Dto_s.Departments
{
    public class DepartmentsDtoValidation: AbstractValidator<DepartmentsDto>
    {
        public DepartmentsDtoValidation() {
            RuleFor(D => D.Name).NotEmpty().WithMessage("Name is Required")
                .MinimumLength(4).WithMessage("Minimum Length is 4")
                .MaximumLength(20).WithMessage("Maximum Length is 20");
        }   
    }
}
