using AgroOrganizer.Models.Dtos.ContractDto;
using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Contract;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;

    public ContractService(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }
    
    public async Task<ContractDto?> GetByIdAsync(int contractId)
    {
        var contract =  await _contractRepository.GetContractByIdAsync(contractId);
        if (contract == null)
        {
            throw new NotFoundException($"Contract with id {contractId} does not exist");
        }
        
        return new ContractDto(contract);
    }

    public async Task<List<ContractDto>> GetAllAsync(int offset, int limit)
    {
        var contracts = await _contractRepository.GetAllContractsAsync(offset, limit);
        return contracts.Select(c => new ContractDto(c)).ToList();
    }

    public async Task<ContractDto> CreateContractAsync(ContractEntity entity)
    {
        var created = await _contractRepository.CreateContractAsync(entity);
        return new ContractDto(created);
    }

    public async Task<ContractDto?> UpdateContractAsync(int contractId, UpdateContractDto dto)
    {
        var contract = await _contractRepository.UpdateContractAsync(contractId, dto);
        if (contract == null)
        {
            throw new NotFoundException($"Contract with id {contractId} does not exist");
        }
        
        return new ContractDto(contract);
    }

    public async Task<bool> DeleteContractAsync(int contractId)
    {
        var deleted = await _contractRepository.DeleteContractAsync(contractId);
        return deleted != null;
    }
}