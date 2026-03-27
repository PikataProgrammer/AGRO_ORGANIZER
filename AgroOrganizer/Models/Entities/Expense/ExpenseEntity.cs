using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Models.Entities.FieldSeason;

namespace AgroOrganizer.Models.Entities.Expense;

public class ExpenseEntity
{
    public int Id { get; private set; }
    public string Type { get; private set; }
    public decimal Amount { get; private set; }
    public DateTimeOffset Date { get; private set; }

    public int FieldSeasonId { get; private set; }
    public FieldSeasonEntity FieldSeason { get; private set; }

    public ExpenseEntity() { }

    public ExpenseEntity(int id, string type, decimal amount, DateTimeOffset date, FieldSeasonEntity fieldSeason)
    {
        Id = id;
        Type = type;
        Amount = amount;
        Date = date;
        FieldSeason = fieldSeason;
        FieldSeasonId = fieldSeason.Id;
    }

    public void Update(CreateExpenseDto dto)
    {
        Type = dto.Type;
        Amount = dto.Amount;
        Date = dto.Date;
    }
}