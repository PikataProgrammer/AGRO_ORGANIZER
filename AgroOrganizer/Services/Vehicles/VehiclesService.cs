using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Models.Dtos.VehiclesDto;
using AgroOrganizer.Models.Entities.Vehicles;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Vehicles;

public class VehiclesService : IVehiclesService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehiclesService(IVehicleRepository vehicleRepository){
        _vehicleRepository  = vehicleRepository;
    }

    public async Task<VehiclesDto?> GetByIdAsync(int vehicleId)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
        if (vehicle == null)
        {
            throw new NotFoundException($"Vehicle with id {vehicleId} not found");
        }
        return new VehiclesDto(vehicle);
    }

    public async Task<List<VehiclesDto>> GetAllAsync(int offset, int limit)
    {
        var vehicles = await  _vehicleRepository.GetAllAsync(offset, limit);
        return vehicles.Select(x => new VehiclesDto(x)).ToList();
    }

    public async Task<VehiclesDto> CreateVehicleAsync(CreateVehicleDto dto)
    {
        var vehicleEntity = new VehicleEntity(dto);
        var created = await _vehicleRepository.CreateAsync(vehicleEntity);
        return new VehiclesDto(created);
    }

    public async Task<VehiclesDto?> UpdateVehicleAsync(int vehicleId, CreateVehicleDto salesDto)
    {
        var updatedVehicle = await _vehicleRepository.UpdateAsync(vehicleId, salesDto);
        if (updatedVehicle == null)
        {
            throw new NotFoundException($"Vehicle with id {vehicleId} not found");
        }
        
        return new VehiclesDto(updatedVehicle);
    }

    public async Task<bool> DeleteVehicleAsync(int vehicleId)
    {
        var deletedVehicle =  await _vehicleRepository.DeleteAsync(vehicleId);
        return deletedVehicle != null;
    }

    public async Task<bool> UpdateImageUrlAsync(int vehicleId, string imageUrl)
    {
        return await _vehicleRepository.UpdateImageUrlAsync(vehicleId, imageUrl);
    }
}