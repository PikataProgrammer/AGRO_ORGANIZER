using AgroOrganizer.Models.Dtos.UserDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.UserDtoValidator;

public class CreateUserDtoValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name max length is 50 characters");

        RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name max length is 50 characters");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
        
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password minimum length is 6")
            .MaximumLength(50).WithMessage("Password maximum length is 50 characters");
        
    }
}