using AgroOrganizer.Models.Dtos.FieldDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.FieldDtoValidator;

public class CreateFieldDtoValidator : AbstractValidator<FieldDto>
{
    public CreateFieldDtoValidator()
    {
        RuleFor(x => x.FieldName)
            .NotEmpty().WithMessage("Field name is required.")
            .MaximumLength(100).WithMessage("Field name must not exceed 100 characters.");

        RuleFor(x => x.FieldSize)
            .GreaterThan(0).WithMessage("Field size must be greater than 0.");

        RuleFor(x => x.FieldLocation)
            .NotEmpty().WithMessage("Field location is required.")
            .MaximumLength(200).WithMessage("Field location must not exceed 200 characters.");

        RuleFor(x => x.CreatedOn)
            .LessThan(DateTimeOffset.Now).WithMessage("Created date cannot be in the future.");
    }
}
