namespace AgroOrganizer.Models.Dtos.ContractDto;

public class UpdateContractDto
{
    public DateTimeOffset DateSigned { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public string Filepath { get; set; }
}