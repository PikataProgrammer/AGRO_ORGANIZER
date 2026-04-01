using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Models.Entities.Expense;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.Entities.Sales;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Dtos.FieldSeasonDto;

public class CreateFieldSeasonDto
{
    public int Year { get; set; }
    public CropTypes CropType { get; set; }

    public int FieldId { get;  set; }
    public FieldEntity Field { get; set; }
    
}