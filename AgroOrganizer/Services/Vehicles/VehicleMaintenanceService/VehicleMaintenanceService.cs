using AgroOrganizer.Models.Dtos.VehiclesDto;
using AgroOrganizer.Models.Dtos.VehiclesDto.VehicleServiceDto;
using AgroOrganizer.Models.Entities.Vehicles;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Vehicles.VehicleMaintenanceService;

public class VehicleMaintenanceService : IVehicleMaintenanceService
{
    private readonly IVehicleServiceRepository _vehicleServiceRepository;
    public VehicleMaintenanceService(IVehicleServiceRepository vehicleServiceRepository)
    {
        _vehicleServiceRepository = vehicleServiceRepository;
    }
    public async Task<List<VehicleServiceDto>> GetAllAsync(int offset, int limit)
    {
        var vehicleService = await _vehicleServiceRepository.GetAllAsync(offset, limit);
        return vehicleService.Select(x => new VehicleServiceDto(x)).ToList();
    }

    public async Task<List<VehicleServiceDto>> GetAllByVehicleIdAsync(int vehicleId)
    {
        var vehicleServices = await _vehicleServiceRepository.GetAllByVehicleIdAsync(vehicleId);
        
        return vehicleServices.Select(x => new VehicleServiceDto(x)).ToList();
    }

    public async Task<VehicleServiceDto?> GetByIdAsync(int vehicleServiceId)
    {
        var vehicleService = await _vehicleServiceRepository.GetByIdAsync(vehicleServiceId);
        if (vehicleService == null)
        {
            throw new NotFoundException($"Vehicle service with id {vehicleServiceId} not found");
        }
        return new VehicleServiceDto(vehicleService);
    }
    
    public async Task<VehicleServiceDto> CreateVehicleAsync(CreateVehicleServiceDto dto)
    {
        var vehicleServiceEntity = new VehicleServiceEntity(dto);
        var created = await _vehicleServiceRepository.CreateAsync(vehicleServiceEntity);
        return new VehicleServiceDto(created);
    }

    public async Task<VehicleServiceDto?> UpdateVehicleAsync(int vehicleId, CreateVehicleServiceDto salesDto)
    {
        var updatedVehicleService = await _vehicleServiceRepository.UpdateAsync(vehicleId, salesDto);
        if (updatedVehicleService == null)
        {
            throw new NotFoundException($"Vehicle service with id {vehicleId} not found");
        }
        
        return new VehicleServiceDto(updatedVehicleService);
    }

    public async Task<bool> DeleteVehicleAsync(int vehicleServiceId)
    {
        var deletedVehicleService =  await _vehicleServiceRepository.DeleteAsync(vehicleServiceId);
        return deletedVehicleService != null;
    }
}