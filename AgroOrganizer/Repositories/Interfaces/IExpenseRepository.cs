using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Models.Entities.Expense;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IExpenseRepository
{
    public Task<List<ExpenseEntity>> GetAllAsync(int offset, int limit);
    public Task<ExpenseEntity?> GetByIdAsync(int id);
    public Task<ExpenseEntity> CreateAsync(ExpenseEntity entity);
    public Task<ExpenseEntity?> UpdateAsync(int id,  CreateExpenseDto dto);
    public Task<ExpenseEntity?> DeleteAsync(int id);
    public Task SaveChangesAsync();
}