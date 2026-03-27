namespace AgroOrganizer.Models.Dtos.ExpenseDto;

public class CreateExpenseDto
{
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset Date { get; set; }

}