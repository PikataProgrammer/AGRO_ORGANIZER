using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Entities.Storages;

public class SeedStorageEntity : GrainStorageBase
{
    public decimal? GerminationRate { get; private set; } 

    public SeedStorageEntity() : base() { }

    public SeedStorageEntity(CropTypes cropType, decimal? germinationRate = null) 
        : base(cropType)
    {
        GerminationRate = germinationRate;
    }

    public void UpdateGerminationRate(decimal rate)
    {
        GerminationRate = rate;
    }
}