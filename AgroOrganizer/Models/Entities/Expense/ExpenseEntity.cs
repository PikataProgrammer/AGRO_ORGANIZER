using System.ComponentModel.DataAnnotations.Schema;
using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.Entities.FieldSeason;

namespace AgroOrganizer.Models.Entities.Expense;

public class ExpenseEntity
{
    public int Id { get; private set; }
    public string Type { get; private set; }
    public decimal Amount { get; private set; }
    public DateTimeOffset Date { get; private set; }

    public int FieldSeasonId { get; private set; }
    [ForeignKey("FieldSeasonId")]
    public FieldSeasonEntity FieldSeason { get; private set; }

    public ExpenseEntity() { }

    public ExpenseEntity(CreateExpenseDto expenseDto)
    {
        Type = expenseDto.Type;
        Amount = expenseDto.Amount;
        Date = expenseDto.Date;
        FieldSeasonId = expenseDto.FieldSeasonId ;
    }

    public void Update(UpdateExpenseDto dto)
    {
        Type = dto.Type;
        Amount = dto.Amount;
        Date = dto.Date;
        FieldSeasonId = dto.FieldSeasonId;
    }
}