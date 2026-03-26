using AgroOrganizer.Models.Dtos.DriverDto;

namespace AgroOrganizer.Models.Entities.Drivers;

public class DriverEntity
{
    public int Id { get; private set; }
    public string DriverName { get; private set; }
    public int DriverAge { get; private set; }
    public string DriverPhoneNumber { get; private set; }

    public DriverEntity()
    {
        
    }

    public DriverEntity(int driverId, string diverName, int driverAge, string driverPhoneNumber)
    {
        Id = driverId;
        DriverName = diverName;
        DriverAge = driverAge;
        DriverPhoneNumber = driverPhoneNumber;
    }

    public void Update(CreateUpdateDriverDto driverEntityModel)
    {
        DriverName = driverEntityModel.DriverName;
        DriverAge = driverEntityModel.DriverAge;
        DriverPhoneNumber = driverEntityModel.DriverPhoneNumber;
    }
}