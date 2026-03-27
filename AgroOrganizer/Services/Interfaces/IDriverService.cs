using AgroOrganizer.Models.Dtos.DriverDto;

namespace AgroOrganizer.Services.Interfaces;

public interface IDriverService
{
    Task<DriverDto?> GetDriverByIdAsync(int driverId);
    Task<List<DriverDto?>> GetAllAsync(int offset, int limit);
    Task<DriverDto> CreateDriverAsync(CreateUpdateDriverDto driverDto);
    Task<DriverDto?> UpdateDriverAsync(int driverId, CreateUpdateDriverDto driverDto);
    Task<bool> DeleteDriverAsync(int driverId);
}