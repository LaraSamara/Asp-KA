using FluentValidation;

namespace Identity_Jwt.Dto_s.Identity
{
    public class RegisterDtoValidation: AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation() {
            RuleFor(R => R.Email)
                .NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Email Must Be Valid");

            RuleFor(R => R.Password)
                .NotEmpty().WithMessage("Password Is Required")
                .MinimumLength(6).WithMessage("Password Must Contains at least 6 characters")
                .MaximumLength(20).WithMessage("Password Must Contains at most 20 characters")
                .Matches("[A-Z]").WithMessage("Password Must Contains Uppercase")
                .Matches("[a-z]").WithMessage("Password Must Contains Lowercase")
                .Matches("[0-9]").WithMessage("Password Must Contains Digits")
                .Matches("[\\W]").WithMessage("Password Must Contains Special character")
                .Matches(@"^\S+$").WithMessage("Password Must not Contains Space");

            RuleFor(R => R.Address)
                .MinimumLength(4).WithMessage("Minimum length of address must be 4")
                .MaximumLength(20).WithMessage("Maximum length of address must be 20");

            RuleFor(R => R.UserName)
                .NotEmpty().WithMessage("User Name is Required")
                .MinimumLength(3).WithMessage("Minimum length of address must be 3")
                .MaximumLength(12).WithMessage("Maximum length of address must be 12");

            RuleFor(R => R.DisplayName)
                .NotEmpty().WithMessage("User Name is Required")
                .MinimumLength(3).WithMessage("Minimum length of address must be 3")
                .MaximumLength(20).WithMessage("Maximum length of address must be 20");
        }
    }
}
