namespace AgroOrganizer.Models.Dtos.DriverDto;

public class CreateUpdateDriverDto
{
    public string DriverName { get; set; }
    public int DriverAge { get; set; }
    public string DriverPhoneNumber { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTimeOffset? HiredOn { get; set; }
}