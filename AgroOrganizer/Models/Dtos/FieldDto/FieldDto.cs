using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.Enums.CropTypes;
using AgroOrganizer.Models.Enums.FieldOperationTypes;

namespace AgroOrganizer.Models.Dtos.FieldDto;

public class FieldDto
{
    public int FieldId { get; set; }
    public string FieldName { get;  set; }
    public decimal FieldSize { get;  set; }
    public string FieldLocation { get;  set; }
    
    public CropTypes CropType { get;  set; }
    public FieldOperationTypes FieldOperation { get;  set; }
    public DateTimeOffset? CreatedOn { get; set; } 
    
    public DriverEntity Driver { get; set; }

    public FieldDto(FieldEntity field)
    {
        FieldId = field.FieldId;
        FieldName = field.FieldName;
        FieldSize = field.FieldSize;
        FieldLocation = field.FieldLocation;
        CropType = field.CropType;
        FieldOperation = field.FieldOperation;
        CreatedOn = field.CreatedOn;
        Driver = field.Driver;
    }

}