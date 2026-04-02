using AgroOrganizer.Models.Entities.Field;

namespace AgroOrganizer.Models.Dtos.ContractDto;

public class UpdateContractDto
{
    public DateTimeOffset DateSigned { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public string FilePath { get; set; }
    public int FieldId { get; set; }
}