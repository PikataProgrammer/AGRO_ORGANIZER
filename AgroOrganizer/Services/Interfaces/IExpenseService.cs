using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Models.Entities.Expense;

namespace AgroOrganizer.Services.Interfaces;

public interface IExpenseService
{
    Task<List<ExpenseDto>> GetAllAsync(int offset, int limit);
    Task<ExpenseDto?> GetByIdAsync(int expenseId);
    Task<ExpenseDto> CreateAsync(ExpenseEntity entity);
    Task<ExpenseDto?> UpdateAsync(int expenseId, CreateExpenseDto expenseDto);
    Task<bool> DeleteAsync(int expenseId);
}