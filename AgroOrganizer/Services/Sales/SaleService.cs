using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Models.Entities.Sales;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Sales;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }
    
    public async Task<SalesDto?> GetByIdAsync(int saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
        {
            throw new NotFoundException($"Sale with id {saleId} not found");
        }
        return new SalesDto(sale);
    }

    public async Task<List<SalesDto>> GetAllAsync(int offset, int limit)
    {
        var sales = await _saleRepository.GetAllAsync(offset, limit);
        return sales.Select(s => new SalesDto(s)).ToList();
    }

    public async Task<SalesDto> CreateSaleAsync(SaleEntity entity)
    {
        var created = await _saleRepository.CreateAsync(entity);
        return new SalesDto(created);
    }

    public async Task<SalesDto?> UpdateSaleAsync(int saleId, UpdateSalesRequestDto salesDto)
    {
        var updatedSale = await _saleRepository.UpdateAsync(saleId, salesDto);
        if (updatedSale == null)
        {
            throw new NotFoundException($"Sale with id {saleId} not found");
        }
        
        return new SalesDto(updatedSale);
    }

    public async Task<bool> DeleteSaleAsync(int saleId)
    {
        var deletedSale =  await _saleRepository.DeleteAsync(saleId);
        return deletedSale != null;
    }
}