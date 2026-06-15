using AgroOrganizer.Models.Entities.Storages;
using AgroOrganizer.Models.Enums;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Dtos.StorageDto;

public class StorageDto
{
    public int Id { get; set; }
    public StorageType StorageType { get; set; }
    public CropTypes CropType { get; set; }
    public decimal QuantityInKg { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
    
    public decimal? GerminationRate { get; set; }
    public decimal? AverageMoisture { get; set; }

    public StorageDto(GrainStorageBase storage)
    {
        Id = storage.Id;
        CropType = storage.CropType;
        QuantityInKg = storage.QuantityInKg;
        LastUpdated = storage.LastUpdated;

        if (storage is SeedStorageEntity seed)
        {
            StorageType = StorageType.Seed;
            GerminationRate = seed.GerminationRate;
        }
        else if (storage is SaleStorageEntity sale)
        {
            StorageType = StorageType.Sale;
            AverageMoisture = sale.AverageMoisture;
        }
    }
}