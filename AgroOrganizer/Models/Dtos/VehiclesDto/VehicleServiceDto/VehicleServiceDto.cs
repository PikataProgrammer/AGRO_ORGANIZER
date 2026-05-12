using AgroOrganizer.Models.Entities.Vehicles;

namespace AgroOrganizer.Models.Dtos.VehiclesDto.VehicleServiceDto;

public class VehicleServiceDto
{
    public int Id { get; set; }
    public DateTime ServiceDate { get; set; }
    public string? Description { get; set; }
    public int? VehicleId { get; set; }

    public VehicleServiceDto() { }

    public VehicleServiceDto(VehicleServiceEntity entity)
    {
        Id = entity.Id;
        ServiceDate = entity.ServiceDate;
        Description = entity.Description;
        VehicleId = entity.VehicleId;
    }
}