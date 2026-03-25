using AgroOrganizer.Models.Dtos.ContractDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.ContractDtoValidator;

public class ContractDtoValidator : AbstractValidator<ContractDto>
{
    public ContractDtoValidator()
    {
        RuleFor(x => x.DateSigned)
            .NotEmpty().WithMessage("The signed date cannot be empty!");
    }
}