using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.VehiclesDto;
using AgroOrganizer.Models.Entities.Vehicles;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;
    
    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<VehicleEntity>> GetAllAsync(int offset, int limit)
    {
        return await _context.Vehicles
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<VehicleEntity?> GetByIdAsync(int id)
    {
        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
        return vehicle;
    }

    public async Task<VehicleEntity> CreateAsync(VehicleEntity vehicleEntity)
    {
        await _context.Vehicles.AddAsync(vehicleEntity);
        await _context.SaveChangesAsync();
        return vehicleEntity;
    }

    public async Task<VehicleEntity?> UpdateAsync(int id, CreateVehicleDto vehicleDto)
    {
        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
        if (vehicle == null)
        {
            return null;   
        }
        
        vehicle.Update(vehicleDto);
        
        await _context.SaveChangesAsync();
        return vehicle;
    }

    public async Task<VehicleEntity?> DeleteAsync(int id)
    {
        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
        if (vehicle == null)
        {
            return null;
        }
        
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();
        return vehicle;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}