using AgroOrganizer.Models.Dtos.ActivityDto;
using AgroOrganizer.Models.Enums.FieldOperationTypes;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.FieldSeason;

namespace AgroOrganizer.Models.Entities.Activity;

public class ActivityEntity
{
    public int Id { get; private set; }
    public FieldOperationTypes Type { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public string? Notes { get; private set; }

    public int FieldSeasonId { get; private set; }
    public FieldSeasonEntity FieldSeason { get; private set; }

    public int? DriverId { get; private set; }
    public DriverEntity? Driver { get; private set; }

    public ActivityEntity() { }

    public ActivityEntity(int id, FieldOperationTypes type, DateTimeOffset date, FieldSeasonEntity fieldSeason, DriverEntity? driver = null, string? notes = null)
    {
        Id = id;
        Type = type;
        Date = date;
        FieldSeason = fieldSeason;
        FieldSeasonId = fieldSeason.Id;
        Driver = driver;
        DriverId = driver?.Id;
        Notes = notes;
    }

    public void Update(CreateActivityDto dto)
    {
        Type = dto.Type;
        Date = dto.Date;
        Notes = dto.Notes;
        DriverId = dto.DriverId;
    }
}