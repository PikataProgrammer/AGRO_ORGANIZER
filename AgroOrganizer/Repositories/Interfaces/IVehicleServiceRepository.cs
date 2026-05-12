using AgroOrganizer.Models.Dtos.VehiclesDto.VehicleServiceDto;
using AgroOrganizer.Models.Entities.Vehicles;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IVehicleServiceRepository
{
    Task<List<VehicleServiceEntity>> GetAllByVehicleIdAsync(int vehicleId);
    public Task<List<VehicleServiceEntity>> GetAllAsync(int offset, int limit);
    public Task<VehicleServiceEntity?> GetByIdAsync(int id);
    public Task<VehicleServiceEntity> CreateAsync(VehicleServiceEntity vehicleServiceEntity);
    public Task<VehicleServiceEntity?> UpdateAsync(int id, CreateVehicleServiceDto vehicleServiceDto);
    public Task<VehicleServiceEntity?> DeleteAsync(int id);
    public Task SaveChangesAsync();
}