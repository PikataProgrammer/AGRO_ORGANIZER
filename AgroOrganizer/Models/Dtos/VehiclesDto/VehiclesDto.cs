using AgroOrganizer.Models.Entities.Vehicles;

namespace AgroOrganizer.Models.Dtos.VehiclesDto;

public class VehiclesDto
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Type { get; set; } 
    public string PlateNumber { get; set; } 
    public int? PurchaseYear { get; set; }
    public DateTime? LastServiceDate { get; set; }
    public int? UserId { get; set; }
    public string? ImageUrl { get; set; }

    public VehiclesDto(VehicleEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Type = entity.Type;
        PlateNumber = entity.PlateNumber;
        PurchaseYear = entity.PurchaseYear;
        LastServiceDate = entity.LastServiceDate;
        UserId = entity.UserId;
        ImageUrl = entity.ImageUrl;
    }
}