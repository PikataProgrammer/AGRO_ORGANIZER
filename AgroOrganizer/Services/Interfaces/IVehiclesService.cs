using AgroOrganizer.Models.Dtos.VehiclesDto;

namespace AgroOrganizer.Services.Interfaces;

public interface IVehiclesService
{
    Task<VehiclesDto?> GetByIdAsync(int vehicleId);
    Task<List<VehiclesDto>> GetAllAsync(int offset, int limit);
    Task<VehiclesDto> CreateVehicleAsync(CreateVehicleDto dto);
    Task<VehiclesDto?> UpdateVehicleAsync(int vehicleId, CreateVehicleDto salesDto);
    Task<bool> DeleteVehicleAsync(int vehicleId);
    Task<bool> UpdateImageUrlAsync(int vehicleId, string imageUrl);
}