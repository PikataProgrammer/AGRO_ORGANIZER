using AgroOrganizer.Models.Dtos.FieldSeasonDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.FieldSeasonDtoValidator;

public class CreateFieldSeasonDtoValidator : AbstractValidator<CreateFieldSeasonDto>
{
    public CreateFieldSeasonDtoValidator()
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(2000, DateTimeOffset.Now.Year + 1)
            .WithMessage("Year must be realistic and not in the far future.");

        RuleFor(x => x.CropType)
            .IsInEnum().WithMessage("Invalid crop type.");
    }
}