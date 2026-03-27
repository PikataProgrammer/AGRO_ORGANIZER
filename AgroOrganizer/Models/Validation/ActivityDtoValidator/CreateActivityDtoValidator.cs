using AgroOrganizer.Models.Dtos.ActivityDto;
using AgroOrganizer.Models.Dtos.DriverDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.ActivityDtoValidator;

public class CreateActivityDtoValidator : AbstractValidator<CreateActivityDto>
{
    public CreateActivityDtoValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid activity type.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThan(DateTimeOffset.Now).WithMessage("Activity date cannot be in the future.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.");

        RuleFor(x => x.DriverId)
            .GreaterThan(0).When(x => x.DriverId.HasValue)
            .WithMessage("DriverId must be greater than 0 if provided.");
    }
}