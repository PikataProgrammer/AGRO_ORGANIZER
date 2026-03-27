using AgroOrganizer.Models.Dtos.ContractDto;
using AgroOrganizer.Models.Entities.Field;

namespace AgroOrganizer.Models.Entities.Contracts;

public class ContractEntity
{
    public int Id { get; private set; }
    public DateTimeOffset DateSigned { get; private set; }
    public DateTimeOffset? ExpirationDate { get; private set; }
    public string FilePath { get; private set; }

    public int FieldId { get; private set; }
    public FieldEntity Field { get; private set; }

    public ContractEntity() { }

    public ContractEntity(UpdateContractDto contractDto, FieldEntity field)
    {
        DateSigned = contractDto.DateSigned;
        ExpirationDate = contractDto.ExpirationDate;
        FilePath = contractDto.FilePath;
        Field = field;
        FieldId = field.Id;
    }

    public void Update(UpdateContractDto dto)
    {
        DateSigned = dto.DateSigned;
        ExpirationDate = dto.ExpirationDate;
        FilePath = dto.FilePath;
    }
}