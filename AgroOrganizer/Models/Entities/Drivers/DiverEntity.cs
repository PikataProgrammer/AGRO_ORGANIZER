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

    public DriverEntity(int id, string name, int age, string phoneNumber, string? licenseNumber = null, DateTimeOffset? hiredOn = null)
    {
        Id = id;
        DriverName = name;
        DriverAge = age;
        DriverPhoneNumber = phoneNumber;
        LicenseNumber = licenseNumber;
        HiredOn = hiredOn;
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