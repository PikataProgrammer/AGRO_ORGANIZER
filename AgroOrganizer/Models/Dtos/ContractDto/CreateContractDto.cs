using AgroOrganizer.Models.Entities.Field;

namespace AgroOrganizer.Models.Dtos.ContractDto;

public class CreateContractDto
{
    public DateTimeOffset DateSigned { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public string FilePath { get; set; }

    public int FieldId { get; set; }
}