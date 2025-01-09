using API.DTOs;
using FluentValidation;

namespace API.Validators;

internal sealed class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Username is required.");
        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}