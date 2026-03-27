using AgroOrganizer.Models.Dtos.FieldSeasonDto;
using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Models.Entities.Sales;
using AgroOrganizer.Models.Entities.Expense;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Entities.FieldSeason;

public class FieldSeasonEntity
{
    public int Id { get; private set; }
    public int Year { get; private set; }
    public CropTypes CropType { get; private set; }

    public int FieldId { get; private set; }
    public FieldEntity Field { get; private set; }

    public ICollection<ActivityEntity> Activities { get; private set; }
    public ICollection<SaleEntity> Sales { get; private set; }
    public ICollection<ExpenseEntity> Expenses { get; private set; }

    public FieldSeasonEntity()
    {
        Activities = new List<ActivityEntity>();
        Sales = new List<SaleEntity>();
        Expenses = new List<ExpenseEntity>();
    }

    public FieldSeasonEntity(CreateFieldSeasonDto seasonDto, FieldEntity field)
    {
        Year = seasonDto.Year;
        CropType = seasonDto.CropType;
        Field = field;
        FieldId = field.Id;
        Activities = new List<ActivityEntity>();
        Sales = new List<SaleEntity>();
        Expenses = new List<ExpenseEntity>();
    }

    public void Update(CreateFieldSeasonDto dto)
    {
        Year = dto.Year;
        CropType = dto.CropType;
    }
}