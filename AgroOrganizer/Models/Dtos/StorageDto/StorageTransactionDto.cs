using AgroOrganizer.Models.Enums;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Dtos.StorageDto;

public class StorageTransactionDto
{
    public StorageType StorageType { get; set; }
    public CropTypes CropType { get; set; }
    public decimal Amount { get; set; }
    
    public decimal? SpecificParameter { get; set; }
    
}