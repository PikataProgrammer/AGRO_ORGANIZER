namespace AgroOrganizer.Models.Dtos.VehiclesDto.VehicleServiceDto;

public class CreateVehicleServiceDto
{
    public DateTime ServiceDate { get; set; }
    public string? Description { get; set; }
    public int? VehicleId { get; set; }
}