using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Entities.User;
using AgroOrganizer.Models.Enums.CropTypes;
using AgroOrganizer.Models.Enums.FieldOperationTypes;

namespace AgroOrganizer.Models.Dtos.FieldDto;

public class FieldDto
{
    public int FieldId { get; set; }
    public string FieldName { get;  set; }
    public decimal FieldSize { get;  set; }
    public string FieldLocation { get;  set; }
    
    public DateTimeOffset? CreatedOn { get; set; } 
    public ICollection<FieldSeasonEntity> Seasons { get; set; }
    public int? UserId { get; set; }

    public FieldDto(FieldEntity field)
    {
        FieldId = field.Id;
        FieldName = field.FieldName;
        FieldSize = field.FieldSize;
        FieldLocation = field.FieldLocation;
        Seasons = field.Seasons;
        UserId = field.UserId;
        CreatedOn = field.CreatedOn;
    }

}