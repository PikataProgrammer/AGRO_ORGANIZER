using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Models.Entities.Sales;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _context;
    
    public SaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<SaleEntity>> GetAllAsync(int offset, int limit)
    {
        return await _context.Sales
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<SaleEntity?> GetByIdAsync(int id)
    {
        return await _context.Sales.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<SaleEntity> CreateAsync(SaleEntity saleEntityModel)
    { 
        await _context.Sales.AddAsync(saleEntityModel);
        await _context.SaveChangesAsync();
        return saleEntityModel;
    }

    public async Task<SaleEntity?> UpdateAsync(int id, UpdateSalesRequestDto saleModelDto)
    {
        var sale =  await _context.Sales.FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null)
        {
            return null;
        }
        
        sale.Update(saleModelDto);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<SaleEntity?> DeleteAsync(int id)
    {
        var sale =  await _context.Sales.FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null)
        {
            return null;
        }
        
        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}