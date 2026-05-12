using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.VehiclesDto.VehicleServiceDto;
using AgroOrganizer.Models.Entities.Vehicles;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class VehicleServiceRepository : IVehicleServiceRepository
{
    private readonly ApplicationDbContext _context;
    
    public VehicleServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleServiceEntity>> GetAllByVehicleIdAsync(int vehicleId)
    {
        return await _context.VehicleServices
            .Where(x => x.VehicleId == vehicleId)
            .OrderByDescending(x => x.ServiceDate)
            .ToListAsync();
    }

    public async Task<List<VehicleServiceEntity>> GetAllAsync(int offset, int limit)
    {
        return await _context.VehicleServices
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<VehicleServiceEntity?> GetByIdAsync(int id)
    {
        var vehicleService = await  _context.VehicleServices.FirstOrDefaultAsync(x => x.Id == id);
        return vehicleService;
    }

    public async Task<VehicleServiceEntity> CreateAsync(VehicleServiceEntity vehicleServiceEntity)
    {
        await _context.VehicleServices.AddAsync(vehicleServiceEntity);
        await _context.SaveChangesAsync();
        return vehicleServiceEntity;
    }

    public async Task<VehicleServiceEntity?> UpdateAsync(int id, CreateVehicleServiceDto vehicleServiceDto)
    {
        var vehicleService = await  _context.VehicleServices.FirstOrDefaultAsync(x => x.Id == id);
        if (vehicleService == null)
        {
            return null;
        }
        
        vehicleService.Update(vehicleServiceDto);
        await _context.SaveChangesAsync();
        return vehicleService;
    }

    public async Task<VehicleServiceEntity?> DeleteAsync(int id)
    {
        var vehicleService = await  _context.VehicleServices.FirstOrDefaultAsync(x => x.Id == id);
        if (vehicleService == null)
        {
            return null;
        }
        
        _context.VehicleServices.Remove(vehicleService);
        
        await _context.SaveChangesAsync();
        return vehicleService;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}