using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Entities.Storages;

public abstract class GrainStorageBase
{
    public int Id { get; protected set; }
    public CropTypes CropType { get; protected set; }
    public decimal QuantityInKg { get; protected set; }
    public DateTimeOffset LastUpdated { get; protected set; }
    
    protected GrainStorageBase() { }

    protected GrainStorageBase(CropTypes cropType)
    {
        CropType = cropType;
        QuantityInKg = 0;
        LastUpdated = DateTimeOffset.UtcNow;
    }

    public virtual void AddQuantity(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Количеството трябва да е положително.");
        
        QuantityInKg += amount;
        LastUpdated = DateTimeOffset.UtcNow;
    }

    public virtual void RemoveQuantity(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Количеството трябва да е положително.");
        if (QuantityInKg < amount) throw new InvalidOperationException($"Няма достатъчно наличност от {CropType} в склада.");
            
        QuantityInKg -= amount;
        LastUpdated = DateTimeOffset.UtcNow;
    }
}