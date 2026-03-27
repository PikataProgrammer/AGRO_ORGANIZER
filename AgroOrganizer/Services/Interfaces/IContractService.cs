using AgroOrganizer.Models.Dtos.ContractDto;
using AgroOrganizer.Models.Entities.Contracts;

namespace AgroOrganizer.Services.Interfaces;

public interface IContractService
{
    Task<ContractDto?> GetByIdAsync(int contractId);
    Task<List<ContractDto>> GetAllAsync(int offset, int limit);
    Task<ContractDto> CreateContractAsync(ContractEntity entity);
    Task<ContractDto?> UpdateContractAsync(int contractId, UpdateContractDto dto);
    Task<bool> DeleteContractAsync(int contractId);
}