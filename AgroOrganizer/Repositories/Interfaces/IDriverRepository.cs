using AgroOrganizer.Models.Dtos.DriverDto;
using AgroOrganizer.Models.Entities.Drivers;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IDriverRepository
{
    public Task<List<DriverEntity>> GetAllAsync(int offset, int limit);
    public Task<DriverEntity?> GetByIdAsync(int id);
    public Task<DriverEntity?> GetByName(string driverName);
    public Task<DriverEntity> CreateAsync(DriverEntity driverEntityModel);
    public Task<DriverEntity?> UpdateAsync(int id, CreateUpdateDriverDto driverEntityModel);
    public Task<DriverEntity?> DeleteAsync(int id);
    public Task SaveChangesAsync();
    
}