namespace AgroOrganizer.Models.Entities.Contracts;

public class ContractEntity
{
    public int Id { get; private set; }
    public DateTimeOffset DateSigned { get; private set; }
    public DateTimeOffset? ExpirationDate { get; private set; }

    public string FilePath { get; private set; }

    public ContractEntity()
    {
        
    }
    
    public ContractEntity(int contractId, DateTimeOffset dateSigned, DateTimeOffset? expirationDate, string filePath)
    {
        Id = contractId;
        DateSigned = dateSigned;
        ExpirationDate = expirationDate;
        FilePath = filePath;
    }
}