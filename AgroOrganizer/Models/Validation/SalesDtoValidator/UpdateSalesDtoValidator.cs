using AgroOrganizer.Models.Dtos.SalesDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.SalesDtoValidator;

public class UpdateSalesDtoValidator : AbstractValidator<UpdateSalesRequestDto>
{
    public UpdateSalesDtoValidator()
    {
        RuleFor(x => x.DateSigned).NotEmpty().WithMessage("The date cannot be empty!");

        RuleFor(x => x.CropType)
            .IsInEnum().WithMessage("Invalid crop type.");

        RuleFor(x => x.PriceForKg)
            .GreaterThan(0).WithMessage("Price per kg must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.BuyerName)
            .NotEmpty().WithMessage("Buyer name is required.")
            .MaximumLength(150);
    }
}