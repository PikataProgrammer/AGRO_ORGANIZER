using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Enums.CropTypes;
using AgroOrganizer.Models.Enums.FieldOperationTypes;

namespace AgroOrganizer.Models.Dtos.FieldDto;

public class CreateFieldRequestDto
{
    public string FieldName { get;  set; }
    public decimal FieldSize { get;  set; }
    public string FieldLocation { get;  set; }

    public DateTimeOffset CreatedOn { get; set; } 

}