using AgroOrganizer.Models.Entities.Drivers;

namespace AgroOrganizer.Models.Dtos.DriverDto;

public class DriverDto
{
    public int DriverId { get; set; }
    public string DriverName { get; set; }
    public int DriverAge { get; set; }
    public string DriverPhoneNumber { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTimeOffset? HiredOn { get; set; }
    public ICollection<ActivityDto.ActivityDto> Activities { get; set; }
    

    public DriverDto(DriverEntity driver)
    {
        DriverId = driver.Id;
        DriverName = driver.DriverName;
        DriverAge = driver.DriverAge;
        DriverPhoneNumber = driver.DriverPhoneNumber;
        LicenseNumber = driver.LicenseNumber;
        HiredOn = driver.HiredOn;
        Activities = new List<ActivityDto.ActivityDto>();
        foreach (var activity in driver.Activities)
        {
            Activities.Add(new ActivityDto.ActivityDto(activity));
        }
    }
}