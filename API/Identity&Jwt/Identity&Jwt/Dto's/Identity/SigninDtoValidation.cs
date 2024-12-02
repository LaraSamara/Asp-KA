using FluentValidation;
using Identity_Jwt.Identity;
using Identity_Jwt.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity_Jwt.Dto_s.Identity
{
    public class SigninDtoValidation: AbstractValidator<SigninDto>
    {
        public SigninDtoValidation()
        {

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
        }
    }
}
