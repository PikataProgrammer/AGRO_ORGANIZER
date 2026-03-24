namespace AgroOrganizer.Models.Entities.Drivers;

public class DriverEntity
{
    public int DriverId { get; private set; }
    public string DriverName { get; private set; }
    public int DriverAge { get; private set; }
    public string DriverPhoneNumber { get; private set; }

    public DriverEntity()
    {
        
    }

    public DriverEntity(int driverId, string diverName, int driverAge, string driverPhoneNumber)
    {
        DriverId = driverId;
        DriverName = diverName;
        DriverAge = driverAge;
        DriverPhoneNumber = driverPhoneNumber;
    }

    public void Update(string name, int age, string phoneNumber)
    {
        DriverName = name;
        DriverAge = age;
        DriverPhoneNumber = phoneNumber;
    }
}