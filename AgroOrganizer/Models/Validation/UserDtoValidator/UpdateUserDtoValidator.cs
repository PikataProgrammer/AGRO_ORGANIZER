using AgroOrganizer.Models.Dtos.UserDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.UserDtoValidator;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserRequestDto>
{
    public UpdateUserDtoValidator()
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
    }
}