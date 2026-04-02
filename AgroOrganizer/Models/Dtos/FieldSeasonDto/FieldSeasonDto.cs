using AgroOrganizer.Models.Entities.Expense;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Entities.Sales;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Dtos.FieldSeasonDto;

public class FieldSeasonDto
{
    public int Id { get; set; }
    public int Year { get; set; }
    public CropTypes CropType { get; set; }
    public ICollection<ActivityDto.ActivityDto> Activities { get;  set; }
    public ICollection<SalesDto.SalesDto> Sales { get; set; }
    public ICollection<ExpenseDto.ExpenseDto> Expenses { get; set; }
    public int FieldId { get; set; }

    public FieldSeasonDto(FieldSeasonEntity entity)
    {
        Id = entity.Id;
        Year = entity.Year;
        CropType = entity.CropType;
        FieldId = entity.FieldId;
        Sales = new List<SalesDto.SalesDto>();
        Activities = new List<ActivityDto.ActivityDto>();
        Expenses = new List<ExpenseDto.ExpenseDto>();
        foreach (var sale in entity.Sales)
        {
            Sales.Add(new SalesDto.SalesDto(sale));
        }

        foreach (var activity in entity.Activities)
        {
            Activities.Add(new ActivityDto.ActivityDto(activity));
        }

        foreach (var expense in entity.Expenses)
        {
            Expenses.Add(new ExpenseDto.ExpenseDto(expense));
        }
    }
}