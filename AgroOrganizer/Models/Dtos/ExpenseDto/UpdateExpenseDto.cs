using AgroOrganizer.Models.Entities.FieldSeason;

namespace AgroOrganizer.Models.Dtos.ExpenseDto;

public class UpdateExpenseDto
{
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset Date { get; set; }
    public int FieldSeasonId { get; set; }
    public FieldSeasonEntity FieldSeason { get;  set; }
}