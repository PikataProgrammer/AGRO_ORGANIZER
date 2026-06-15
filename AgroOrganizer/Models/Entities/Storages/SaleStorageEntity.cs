using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Entities.Storages;

public class SaleStorageEntity : GrainStorageBase
{
    public decimal? AverageMoisture { get; private set; }

    public SaleStorageEntity() : base() { }

    public SaleStorageEntity(CropTypes cropType, decimal? averageMoisture = null) 
        : base(cropType)
    {
        AverageMoisture = averageMoisture;
    }

    public void UpdateMoisture(decimal moisture)
    {
        AverageMoisture = moisture;
    }
}