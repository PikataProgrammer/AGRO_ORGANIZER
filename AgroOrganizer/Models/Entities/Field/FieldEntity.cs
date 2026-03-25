using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Enums.CropTypes;
using AgroOrganizer.Models.Enums.FieldOperationTypes;

namespace AgroOrganizer.Models.Entities.Field;

public class FieldEntity
{
    public int Id { get; private set; }
    public string FieldName { get; private set; }
    public decimal FieldSize { get; private set; }
    public string FieldLocation { get; private set; }
    
    public CropTypes CropType { get; private set; }
    public FieldOperationTypes FieldOperation { get; private set; }
    public DateTimeOffset? CreatedOn { get; private set; } 
    
    public DriverEntity Driver { get; private set; }

    public FieldEntity()
    {
        
    }

    public FieldEntity(int fieldId, string fieldName, decimal fieldSize, 
        string fieldLocation, CropTypes cropType, FieldOperationTypes fieldOperation, 
        DateTimeOffset? createdOn, DriverEntity driver)
    {
        Id = fieldId;
        FieldName = fieldName;
        FieldSize = fieldSize;
        FieldLocation = fieldLocation;
        CropType = cropType;
        FieldOperation = fieldOperation;
        CreatedOn = createdOn;
        Driver = driver;
    }
}