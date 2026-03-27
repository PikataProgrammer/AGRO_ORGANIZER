using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Models.Entities.Sales;

namespace AgroOrganizer.Services.Interfaces;

public interface ISaleService
{
    Task<SalesDto?> GetByIdAsync(int saleId);
    Task<List<SalesDto>> GetAllAsync(int offset, int limit);
    Task<SalesDto> CreateSaleAsync(SaleEntity entity);
    Task<SalesDto?> UpdateSaleAsync(int saleId, UpdateSalesRequestDto salesDto);
    Task<bool> DeleteSaleAsync(int saleId);
}