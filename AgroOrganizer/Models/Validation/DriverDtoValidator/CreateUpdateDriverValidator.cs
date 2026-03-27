using AgroOrganizer.Models.Dtos.DriverDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.DriverDtoValidator;

public class CreateUpdateDriverValidator : AbstractValidator<CreateUpdateDriverDto>
{
    public CreateUpdateDriverValidator()
    {
        RuleFor(x => x.DriverName)
            .NotEmpty().WithMessage("Driver name is required.")
            .MaximumLength(100).WithMessage("Driver name must not exceed 100 characters.");

        RuleFor(x => x.DriverAge)
            .InclusiveBetween(18, 75).WithMessage("Driver age must be between 18 and 75.");

        RuleFor(x => x.DriverPhoneNumber)
            .NotEmpty().WithMessage("Driver phone number is required.")
            .Matches(@"^\+?\d{7,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.LicenseNumber)
            .MaximumLength(20).WithMessage("License number too long.");

        RuleFor(x => x.HiredOn)
            .LessThanOrEqualTo(DateTimeOffset.Now)
            .When(x => x.HiredOn.HasValue)
            .WithMessage("Hired date cannot be in the future.");
    }
}