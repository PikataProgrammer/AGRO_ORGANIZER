using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.ActivityDto;
using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly ApplicationDbContext _context;
    public ActivityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ActivityEntity>> GetAllAsync(int offset, int limit)
    {
        return await _context.Activities
            .Include(a => a.Driver)
            .Include(a => a.FieldSeason)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<ActivityEntity?> GetByIdAsync(int id)
    {
        return await _context.Activities
            .Include(a => a.Driver)
            .Include(a => a.FieldSeason)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<ActivityEntity> CreateAsync(ActivityEntity activity)
    {
        await _context.Activities.AddAsync(activity);
        await _context.SaveChangesAsync();
        return activity;
    }

    public async Task<ActivityEntity?> UpdateAsync(int id, CreateActivityDto dto)
    {
        var activity = await _context.Activities.FirstOrDefaultAsync(a => a.Id == id);
        if (activity == null) return null;

        activity.Update(dto); 
        await _context.SaveChangesAsync();
        return activity;
    }

    public async Task<ActivityEntity?> DeleteAsync(int id)
    {
        var activity = await _context.Activities.FirstOrDefaultAsync(a => a.Id == id);
        if (activity == null) return null;

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();
        return activity;
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}