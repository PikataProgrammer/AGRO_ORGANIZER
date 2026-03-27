using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.ContractDto;
using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _context;
    
    public ContractRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ContractEntity>> GetAllContractsAsync(int offset, int limit)
    {
        return await _context.Contracts
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<ContractEntity?> GetContractByIdAsync(int id)
    {
        return await _context.Contracts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ContractEntity> CreateContractAsync(ContractEntity contractEntityModel)
    {
        await _context.Contracts.AddAsync(contractEntityModel);
        await _context.SaveChangesAsync();
        return contractEntityModel;
    }

    public async Task<ContractEntity?> UpdateContractAsync(int id, UpdateContractDto contractEntityModel)
    {
        var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.Id == id);
        if (contract == null)
        {
            return null;
        }
        
        contract.Update(contractEntityModel);
        await _context.SaveChangesAsync();
        return contract;
    }

    public async Task<ContractEntity?> DeleteContractAsync(int id)
    {
        var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.Id == id);
        if (contract == null)
        {
            return null;
        }
        
        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
        return contract;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}