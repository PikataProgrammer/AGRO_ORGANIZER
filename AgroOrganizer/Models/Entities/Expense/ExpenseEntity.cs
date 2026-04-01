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

    public ExpenseEntity(CreateExpenseDto expenseDto)
    {
        Type = expenseDto.Type;
        Amount = expenseDto.Amount;
        Date = expenseDto.Date;
        FieldSeason = expenseDto.FieldSeason;
        FieldSeasonId = expenseDto.FieldSeasonId ;
    }

    public void Update(UpdateExpenseDto dto)
    {
        Type = dto.Type;
        Amount = dto.Amount;
        Date = dto.Date;
        FieldSeason = dto.FieldSeason;
        FieldSeasonId = dto.FieldSeasonId;
    }
}