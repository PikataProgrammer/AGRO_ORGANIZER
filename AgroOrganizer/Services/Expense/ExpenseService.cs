using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Models.Entities.Expense;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Expense;

public class ExpenseService : IExpenseService 
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }
    
    public async Task<List<ExpenseDto>> GetAllAsync(int offset, int limit)
    {
        var expenses = await _expenseRepository.GetAllAsync(offset, limit);
        return expenses.Select(x => new ExpenseDto(x)).ToList();
    }

    public async Task<ExpenseDto?> GetByIdAsync(int expenseId)
    {
        var expense = await _expenseRepository.GetByIdAsync(expenseId);
        if (expense == null)
        {
            throw new NotFoundException($"Expense with id {expenseId} not found");
        }
        return new ExpenseDto(expense);
    }

    public async Task<ExpenseDto> CreateAsync(ExpenseEntity entity)
    {
        var expense = await _expenseRepository.CreateAsync(entity);
        return new ExpenseDto(expense);
    }

    public async Task<ExpenseDto?> UpdateAsync(int expenseId, CreateExpenseDto expenseDto)
    {
        var updatedExpense = await _expenseRepository.UpdateAsync(expenseId, expenseDto);
        if (updatedExpense == null)
        {
            throw new NotFoundException($"Expense with id {expenseId} not found");
        }
        return new ExpenseDto(updatedExpense);
    }

    public async Task<bool> DeleteAsync(int expenseId)
    {
        var deletedExpense = await _expenseRepository.DeleteAsync(expenseId);
        return deletedExpense != null;
    }
}