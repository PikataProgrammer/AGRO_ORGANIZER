using AgroOrganizer.Models.Dtos.ContractDto;
using AgroOrganizer.Models.Entities.Contracts;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IContractRepository
{
    public Task<List<ContractEntity>> GetAllContractsAsync(int offset, int limit);
    public Task<ContractEntity?> GetContractByIdAsync(int id);
    public Task<ContractEntity> CreateContractAsync(ContractEntity entity);
    public Task<ContractEntity?> UpdateContractAsync(int id, UpdateContractDto contractEntityModel);
    public Task<ContractEntity?> DeleteContractAsync(int id);
    public Task SaveChangesAsync();
}