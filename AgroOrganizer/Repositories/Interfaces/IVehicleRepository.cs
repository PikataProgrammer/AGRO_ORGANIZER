using AgroOrganizer.Models.Dtos.VehiclesDto;
using AgroOrganizer.Models.Entities.Vehicles;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IVehicleRepository
{
    public Task<List<VehicleEntity>> GetAllAsync(int offset, int limit);
    public Task<VehicleEntity?> GetByIdAsync(int id);
    public Task<VehicleEntity> CreateAsync(VehicleEntity vehicleEntity);
    public Task<VehicleEntity?> UpdateAsync(int id, CreateVehicleDto vehicleDto);
    public Task<VehicleEntity?> DeleteAsync(int id);
    public Task SaveChangesAsync();
    Task<bool> UpdateImageUrlAsync(int vehicleId, string imageUrl);
}