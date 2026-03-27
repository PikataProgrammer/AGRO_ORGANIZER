using AgroOrganizer.Models.Dtos.DriverDto;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Driver;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;
    
    public DriverService(IDriverRepository repository)
    {
        _driverRepository = repository;
    }
    
    public async Task<DriverDto?> GetDriverByIdAsync(int driverId)
    { 
        var driver = await _driverRepository.GetByIdAsync(driverId);
        if (driver == null)
        {
            throw new NotFoundException($"Driver with id: {driverId} not found");
        }
        
        return new DriverDto(driver);
    }

    public async Task<List<DriverDto?>> GetAllAsync(int offset, int limit)
    {
        var drivers = await _driverRepository.GetAllAsync(offset, limit);
        if (drivers == null)
        {
            throw new NotFoundException("No drivers found");
        }
        return drivers.Select(driver => new DriverDto(driver)).ToList();
    }

    public async Task<DriverDto> CreateDriverAsync(CreateUpdateDriverDto driverDto)
    {
        var driverEntity = new DriverEntity(driverDto);
        var createdDriver = await _driverRepository.CreateAsync(driverEntity);
        return new DriverDto(createdDriver);
    }

    public async Task<DriverDto?> UpdateDriverAsync(int driverId, CreateUpdateDriverDto dtiverDto)
    {
        var updatedDriver = await _driverRepository.UpdateAsync(driverId, dtiverDto);
        if (updatedDriver == null)
        {
            throw new  NotFoundException($"Driver with id: {driverId} not found");
        };
        
        return new DriverDto(updatedDriver);
    }

    public async Task<bool> DeleteDriverAsync(int driverId)
    {
        var deleted = await _driverRepository.DeleteAsync(driverId);
        return deleted != null;
    }
}