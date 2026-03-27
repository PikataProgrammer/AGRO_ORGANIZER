using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Enums.FieldOperationTypes;

namespace AgroOrganizer.Models.Dtos.ActivityDto;

public class ActivityDto
{
    public int Id { get; set; }
    public FieldOperationTypes Type { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? Notes { get; set; }

    public int FieldSeasonId { get; set; }
    public int? DriverId { get; set; }

    public ActivityDto(ActivityEntity entity)
    {
        Id = entity.Id;
        Type = entity.Type;
        Date = entity.Date;
        Notes = entity.Notes;
        FieldSeasonId = entity.FieldSeasonId;
        DriverId = entity.DriverId;
    }
}