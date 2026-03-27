using AgroOrganizer.Models.Dtos.ContractDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.ContractDtoValidator;

public class UpdateContractDtoValidator : AbstractValidator<UpdateContractDto>
{
    public UpdateContractDtoValidator()
    {
        RuleFor(x => x.DateSigned)
            .NotEmpty().WithMessage("The signed date cannot be empty!")
            .LessThan(DateTimeOffset.Now).WithMessage("The signed date cannot be in the future.");

        RuleFor(x => x.ExpirationDate)
            .GreaterThan(x => x.DateSigned)
            .When(x => x.ExpirationDate.HasValue)
            .WithMessage("Expiration date must be after the signed date.");

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage("File path is required.")
            .MaximumLength(500).WithMessage("File path is too long.");
    }
}