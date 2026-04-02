using System.ComponentModel.DataAnnotations.Schema;
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
    [ForeignKey("FieldId")]
    public FieldEntity Field { get; private set; }

    public ContractEntity() { }

    public ContractEntity(CreateContractDto contractDto)
    {
        DateSigned = contractDto.DateSigned;
        ExpirationDate = contractDto.ExpirationDate;
        FilePath = contractDto.FilePath;
        FieldId = contractDto.FieldId;
    }

    public void Update(UpdateContractDto dto)
    {
        DateSigned = dto.DateSigned;
        ExpirationDate = dto.ExpirationDate;
        FilePath = dto.FilePath;
        FieldId = dto.FieldId;
    }
}