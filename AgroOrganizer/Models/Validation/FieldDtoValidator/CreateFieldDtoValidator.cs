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
            .MaximumLength(200);

        RuleFor(x => x.CropType)
            .IsInEnum().WithMessage("Invalid crop type.");

        RuleFor(x => x.FieldOperation)
            .IsInEnum().WithMessage("Invalid field operation.");

        RuleFor(x => x.Driver)
            .NotNull().WithMessage("Driver is required.");
        
        RuleFor(x => x.CreatedOn).NotEmpty().WithMessage("The date cannot be empty!");
    }
}
