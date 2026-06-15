using AgroOrganizer.Models.Entities.Storages;
using AgroOrganizer.Models.Enums;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IGrainStorageRepository
{
    Task<List<GrainStorageBase>> GetAllAsync();
    Task<GrainStorageBase?> GetSpecificStorageAsync(StorageType type, CropTypes cropType);
    Task<GrainStorageBase> CreateStorageAsync(GrainStorageBase storage);
    Task SaveChangesAsync();
}