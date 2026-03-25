using AgroOrganizer.Models.Entities.Drivers;

namespace AgroOrganizer.Models.Dtos.DriverDto;

public class DriverDto
{
    public int DriverId { get; set; }
    public string DriverName { get; set; }
    public int DriverAge { get; set; }
    public string DriverPhoneNumber { get; set; }

    public DriverDto(DriverEntity driver)
    {
        DriverId = driver.Id;
        DriverName = driver.DriverName;
        DriverAge = driver.DriverAge;
        DriverPhoneNumber = driver.DriverPhoneNumber;
    }
}