using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.FieldSeasonDto;
using AgroOrganizer.Models.Entities.FieldSeason;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories.Interfaces;

public class FieldSeasonRepository : IFieldSeasonRepository
{
    private readonly ApplicationDbContext _context;
    
    public FieldSeasonRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<FieldSeasonEntity>> GetAllAsync(int offset, int limit)
    {
        return await _context.FieldSeasons
            .Include(fs => fs.Field)
            .Include(fs => fs.Activities)
            .Include(fs => fs.Sales)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
            
    }

    public async Task<FieldSeasonEntity?> GetByIdAsync(int id)
    {
        return await _context.FieldSeasons
            .Include(fs => fs.Field)
            .Include(fs => fs.Activities)
            .Include(fs => fs.Sales)
            .AsNoTracking()
            .FirstOrDefaultAsync(fs => fs.Id == id);
    }

    public async Task<FieldSeasonEntity> CreateAsync(FieldSeasonEntity fieldSeason)
    {
        await _context.FieldSeasons.AddAsync(fieldSeason);
        await _context.SaveChangesAsync();
        return fieldSeason;
    }

    public async Task<FieldSeasonEntity?> UpdateAsync(int id, CreateFieldSeasonDto dto)
    {
        var season = await _context.FieldSeasons.FirstOrDefaultAsync(fs => fs.Id == id);
        if (season == null) return null;

        season.Update(dto); 
        await _context.SaveChangesAsync();
        return season;
    }

    public async Task<FieldSeasonEntity?> DeleteAsync(int id)
    {
        var season = await _context.FieldSeasons.FirstOrDefaultAsync(fs => fs.Id == id);
        if (season == null) return null;

        _context.FieldSeasons.Remove(season);
        await _context.SaveChangesAsync();
        return season;
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}