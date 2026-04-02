using AgroOrganizer.Models.Entities.Contracts;

namespace AgroOrganizer.Models.Dtos.ContractDto;

public class ContractDto
{
    public int ContractId { get; set; }
    public DateTimeOffset DateSigned { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public string FilePath { get; set; }
    public int FieldId { get; private set; }

    public ContractDto(ContractEntity contract)
    {
        ContractId = contract.Id;
        DateSigned = contract.DateSigned;
        ExpirationDate = contract.ExpirationDate;
        FilePath = contract.FilePath;
        FieldId = contract.FieldId;
    }
}