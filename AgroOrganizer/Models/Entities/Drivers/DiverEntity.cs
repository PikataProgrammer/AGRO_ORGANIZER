using AgroOrganizer.Models.Dtos.DriverDto;
using AgroOrganizer.Models.Entities.Activity;

namespace AgroOrganizer.Models.Entities.Drivers;

public class DriverEntity
{
    public int Id { get; private set; }
    public string DriverName { get; private set; }
    public int DriverAge { get; private set; }
    public string DriverPhoneNumber { get; private set; }
    public string? LicenseNumber { get; private set; }
    public DateTimeOffset? HiredOn { get; private set; }

    public ICollection<ActivityEntity> Activities { get; private set; }

    public DriverEntity()
    {
        Activities = new List<ActivityEntity>();
    }

    public DriverEntity(CreateUpdateDriverDto driverDto)
    {
        DriverName = driverDto.DriverName;
        DriverAge = driverDto.DriverAge;
        DriverPhoneNumber = driverDto.DriverPhoneNumber;
        LicenseNumber = driverDto.LicenseNumber;
        HiredOn = driverDto.HiredOn;
        Activities = new List<ActivityEntity>();
    }

    public void Update(CreateUpdateDriverDto dto)
    {
        DriverName = dto.DriverName;
        DriverAge = dto.DriverAge;
        DriverPhoneNumber = dto.DriverPhoneNumber;
        LicenseNumber = dto.LicenseNumber;
        HiredOn = dto.HiredOn;
    }
}