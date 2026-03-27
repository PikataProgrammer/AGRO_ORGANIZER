using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Enums.FieldOperationTypes;

namespace AgroOrganizer.Models.Dtos.ActivityDto;

public class CreateActivityDto
{
    public FieldOperationTypes Type { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? Notes { get; set; }
    public int? DriverId { get; set; }
    public int FieldSeasonId { get; set; }
}