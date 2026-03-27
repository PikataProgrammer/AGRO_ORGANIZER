using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.DriverDto;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly ApplicationDbContext _context;
    public DriverRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<DriverEntity>> GetAllAsync(int offset, int limit)
    {
        return await _context.Drivers
            .Skip(offset)
            .Take(limit)
            .OrderBy(x => x.DriverName)
            .ToListAsync();
    }

    public async Task<DriverEntity?> GetByIdAsync(int id)
    {
        return await _context.Drivers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<DriverEntity?> GetByName(string driverName)
    {
        return await _context.Drivers.AsNoTracking().FirstOrDefaultAsync(x => x.DriverName == driverName);
    }

    public async Task<DriverEntity> CreateAsync(DriverEntity driverEntityModel)
    {
        await  _context.Drivers.AddAsync(driverEntityModel);
        await _context.SaveChangesAsync();
        return driverEntityModel;
    }

    public async Task<DriverEntity?> UpdateAsync(int id, CreateUpdateDriverDto driverEntityModel)
    {
        var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
        if (driver == null)
        {
            return null;
        }
        
        driver.Update(driverEntityModel);
        await _context.SaveChangesAsync();
        
        return driver;
    }

    public async Task<DriverEntity?> DeleteAsync(int id)
    {
        var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
        if (driver == null)
        {
            return null;
        }
        
        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();
        return driver;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}