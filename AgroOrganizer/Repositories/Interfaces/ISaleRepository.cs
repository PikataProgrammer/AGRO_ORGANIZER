using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Models.Entities.Sales;

namespace AgroOrganizer.Repositories.Interfaces;

public interface ISaleRepository
{
    public Task<List<SaleEntity>> GetAllAsync(int offset, int limit);
    public Task<SaleEntity?> GetByIdAsync(int id);
    public Task<SaleEntity> CreateAsync(SaleEntity saleEntityModel);
    public Task<SaleEntity?> UpdateAsync(int id, UpdateSalesRequestDto saleModelDto);
    public Task<SaleEntity?> DeleteAsync(int id);
    public Task SaveChangesAsync();
}