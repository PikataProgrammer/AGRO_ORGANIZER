using AgroOrganizer.Models.Dtos.VehiclesDto.VehicleServiceDto;

namespace AgroOrganizer.Services.Interfaces;

public interface IVehicleMaintenanceService
{
    Task<List<VehicleServiceDto>> GetAllByVehicleIdAsync(int vehicleId);
    Task<VehicleServiceDto?> GetByIdAsync(int vehicleServiceId);
    Task<List<VehicleServiceDto>> GetAllAsync(int offset, int limit);
    Task<VehicleServiceDto> CreateVehicleAsync(CreateVehicleServiceDto dto);
    Task<VehicleServiceDto?> UpdateVehicleAsync(int vehicleId, CreateVehicleServiceDto salesDto);
    Task<bool> DeleteVehicleAsync(int vehicleServiceId);
}