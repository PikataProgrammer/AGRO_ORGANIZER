using AgroOrganizer.Models.Dtos.ExpenseDto;
using FluentValidation;

namespace AgroOrganizer.Models.Validation.ExpenseDtoValidator;

public class CreateExpenseDtoValidator :  AbstractValidator<CreateExpenseDto>
{
    public CreateExpenseDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Expense type is required.")
            .MaximumLength(100).WithMessage("Expense type must not exceed 100 characters.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThan(DateTimeOffset.Now).WithMessage("Expense date cannot be in the future.");
    }
}