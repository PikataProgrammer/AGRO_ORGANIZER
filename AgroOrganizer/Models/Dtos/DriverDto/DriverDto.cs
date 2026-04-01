using AgroOrganizer.Models.Entities.Drivers;

namespace AgroOrganizer.Models.Dtos.DriverDto;

public class DriverDto
{
    public string DriverName { get; set; }
    public int DriverAge { get; set; }
    public string DriverPhoneNumber { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTimeOffset? HiredOn { get; set; }
    

    public DriverDto(DriverEntity driver)
    {
        DriverName = driver.DriverName;
        DriverAge = driver.DriverAge;
        DriverPhoneNumber = driver.DriverPhoneNumber;
        LicenseNumber = driver.LicenseNumber;
        HiredOn = driver.HiredOn;
    }
}