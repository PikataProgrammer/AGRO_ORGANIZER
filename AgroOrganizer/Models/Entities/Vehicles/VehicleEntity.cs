using AgroOrganizer.Models.Dtos.VehiclesDto;

namespace AgroOrganizer.Models.Entities.Vehicles;

public class VehicleEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; } 
    public string Type { get; private set; } 
    public string PlateNumber { get; private set; } 
    public int? PurchaseYear { get; private set; }
    public DateTime? LastServiceDate { get; private set; }
    public string? ImageUrl { get; private set; }
    public int? UserId { get; private set; }
    
    public ICollection<VehicleServiceEntity> Services { get; private set; } = new List<VehicleServiceEntity>();

    public VehicleEntity()
    {
        
    }

    public VehicleEntity(CreateVehicleDto dto)
    {
        Name = dto.Name;
        Type = dto.Type;
        PlateNumber = dto.PlateNumber;
        PurchaseYear = dto.PurchaseYear;
        LastServiceDate = dto.LastServiceDate;
        UserId = dto.UserId;
        ImageUrl = dto.ImageUrl;
    }

    public void Update(CreateVehicleDto dto)
    {
        Name = dto.Name;
        Type = dto.Type;
        PlateNumber = dto.PlateNumber;
        PurchaseYear = dto.PurchaseYear;
        LastServiceDate = dto.LastServiceDate;
        UserId = dto.UserId;
        ImageUrl = dto.ImageUrl;
    }
    
    public void SetImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}