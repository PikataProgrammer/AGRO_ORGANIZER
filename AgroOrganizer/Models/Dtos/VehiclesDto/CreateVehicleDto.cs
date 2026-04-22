namespace AgroOrganizer.Models.Dtos.VehiclesDto;

public class CreateVehicleDto
{
    public string Name { get; set; } 
    public string Type { get; set; } 
    public string PlateNumber { get; set; } 
    public int? PurchaseYear { get; set; }
    public DateTime? LastServiceDate { get; set; }
    public int? UserId { get; set; }
}