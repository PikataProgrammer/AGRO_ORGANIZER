using AgroOrganizer.Models.Dtos.StorageDto;

namespace AgroOrganizer.Services.Interfaces;

public interface IGrainStorageService
{
    public Task<List<StorageDto>> GetAllStoragesAsync();
    public Task<StorageDto> AddQuantityAsync(StorageTransactionDto dto);
    public Task<StorageDto> RemoveQuantityAsync(StorageTransactionDto dto);
    
}