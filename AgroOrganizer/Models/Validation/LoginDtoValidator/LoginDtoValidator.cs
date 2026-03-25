using AgroOrganizer.Models.Dtos.LoginDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.LoginDtoValidator;

public class LoginDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.");
        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Password is required.");
    }
}