using AgroOrganizer.Models.Dtos.VehiclesDto;
using AgroOrganizer.Models.Dtos.VehiclesDto.VehicleServiceDto;

namespace AgroOrganizer.Models.Entities.Vehicles;

public class VehicleServiceEntity
{
    public int Id { get; private set; }
    public DateTime ServiceDate { get; private set; }
    public string? Description { get; private set; }
    
    public int? VehicleId { get; private set; }
    public VehicleEntity Vehicle { get; private set; }

    public VehicleServiceEntity() { }

    public VehicleServiceEntity(CreateVehicleServiceDto createVehicleServiceDto)
    {
        ServiceDate = createVehicleServiceDto.ServiceDate;
        Description = createVehicleServiceDto.Description;
        VehicleId = createVehicleServiceDto.VehicleId;
    }

    public void Update(CreateVehicleServiceDto createVehicleServiceDto)
    {
        ServiceDate = createVehicleServiceDto.ServiceDate;
        Description = createVehicleServiceDto.Description;
        VehicleId = createVehicleServiceDto.VehicleId;
    }
}