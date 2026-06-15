using AgroOrganizer.Models.Dtos.StorageDto;
using AgroOrganizer.Models.Entities.Storages;
using AgroOrganizer.Models.Enums;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Storage;

public class GrainStorageService(IGrainStorageRepository _repository) : IGrainStorageService
{
    
    public async Task<List<StorageDto>> GetAllStoragesAsync()
    {
        var storages = await _repository.GetAllAsync();
        return storages.Select(s => new StorageDto(s)).ToList();
    }

    public async Task<StorageDto> AddQuantityAsync(StorageTransactionDto dto)
    {
        var storage = await _repository.GetSpecificStorageAsync(dto.StorageType, dto.CropType);
        
        if (storage == null)
        {
            if (dto.StorageType == StorageType.Seed)
                storage = new SeedStorageEntity(dto.CropType, dto.SpecificParameter);
            else
                storage = new SaleStorageEntity(dto.CropType, dto.SpecificParameter);

            await _repository.CreateStorageAsync(storage);
        }
        
        storage.AddQuantity(dto.Amount);
        await _repository.SaveChangesAsync();

        return new StorageDto(storage);
    }

    public async Task<StorageDto> RemoveQuantityAsync(StorageTransactionDto dto)
    {
        var storage = await _repository.GetSpecificStorageAsync(dto.StorageType, dto.CropType);

        if (storage == null)
            throw new Exception("Този артикул не съществува в склада.");
        
        storage.RemoveQuantity(dto.Amount);
        await _repository.SaveChangesAsync();

        return new StorageDto(storage);
    }
}