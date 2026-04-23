using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Entities.User;
using AgroOrganizer.Models.Enums.CropTypes;
using AgroOrganizer.Models.Enums.FieldOperationTypes;

namespace AgroOrganizer.Models.Dtos.FieldDto;

public class CreateFieldRequestDto
{
    public string FieldName { get; set; }
    public decimal FieldSize { get; set; }
    public string? FieldLocation { get; set; }
    public string? BoundaryJson { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public int UserId { get; set; }

}