using AgroOrganizer.Context;
using AgroOrganizer.Models.Entities.Storages;
using AgroOrganizer.Models.Enums;
using AgroOrganizer.Models.Enums.CropTypes;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class GrainStorageRepository :IGrainStorageRepository
{
    private readonly ApplicationDbContext _context;

    public GrainStorageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GrainStorageBase>> GetAllAsync()
    {
        return await _context.GrainStorages.ToListAsync();
    }

    public async Task<GrainStorageBase?> GetSpecificStorageAsync(StorageType type, CropTypes cropType)
    {
        if (type == StorageType.Seed)
        {
            return await _context.SeedStorages.FirstOrDefaultAsync(x => x.CropType == cropType);
        }
        return await _context.SaleStorages.FirstOrDefaultAsync(x => x.CropType == cropType);
    }

    public async Task<GrainStorageBase> CreateStorageAsync(GrainStorageBase storage)
    {
        await _context.GrainStorages.AddAsync(storage);
        return storage;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}