using AgroOrganizer.Models.Entities.Expense;

namespace AgroOrganizer.Models.Dtos.ExpenseDto;

public class ExpenseDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset Date { get; set; }

    public int FieldSeasonId { get; set; }

    public ExpenseDto(ExpenseEntity entity)
    {
        Id = entity.Id;
        Type = entity.Type;
        Amount = entity.Amount;
        Date = entity.Date;
        FieldSeasonId = entity.FieldSeasonId;
    }
}