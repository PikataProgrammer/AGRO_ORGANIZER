using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Models.Entities.Expense;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext _context;
    
    public ExpenseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ExpenseEntity>> GetAllAsync(int offset, int limit)
    {
        return await _context.Expenses
            .Include(e => e.FieldSeason)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<ExpenseEntity?> GetByIdAsync(int id)
    {
        return await _context.Expenses
            .Include(e => e.FieldSeason)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<ExpenseEntity> CreateAsync(ExpenseEntity expense)
    {
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<ExpenseEntity?> UpdateAsync(int id, CreateExpenseDto dto)
    {
        var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense == null) return null;

        expense.Update(dto); 
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<ExpenseEntity?> DeleteAsync(int id)
    {
        var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense == null) return null;

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}