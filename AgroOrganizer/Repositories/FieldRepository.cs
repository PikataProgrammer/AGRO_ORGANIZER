using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class FieldRepository : IFieldRepository
{
    private readonly ApplicationDbContext _context;
    
    public FieldRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Task<List<FieldEntity>> GetAllAsync(int offset, int limit)
    {
        return _context.Fields
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<FieldEntity?> GetFieldByIdAsync(int id)
    {
       return await _context.Fields.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<FieldEntity?> GetFieldByNameAsync(string name)
    {
        return await _context.Fields.AsNoTracking().FirstOrDefaultAsync(x => x.FieldName == name);
    }

    public async Task<FieldEntity> CreateFieldAsync(FieldEntity fieldModelState)
    {
        await _context.Fields.AddAsync(fieldModelState);
        await _context.SaveChangesAsync();
        return fieldModelState;
    }

    public async Task<FieldEntity?> UpdateFieldAsync(int id, CreateFieldRequestDto fieldModelState)
    {
        var field =  await _context.Fields.FirstOrDefaultAsync(x => x.Id == id);
        if (field == null)
        {
            return null;
        }
        
        field.Update(fieldModelState);
        await _context.SaveChangesAsync();
        return field;
    }

    public async Task<FieldEntity?> DeleteFieldAsync(int id)
    {
        var field =  await _context.Fields.FirstOrDefaultAsync(x => x.Id == id);
        if (field == null)
        {
            return null;
        }
        
        _context.Fields.Remove(field);
        await _context.SaveChangesAsync();
        return field;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}