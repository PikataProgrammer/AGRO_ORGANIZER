using AgroOrganizer.Models.Dtos.ActivityDto;
using AgroOrganizer.Models.Entities.Activity;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IActivityRepository
{
    public Task<List<ActivityEntity>> GetAllAsync(int offset, int limit);
    public Task<ActivityEntity?> GetByIdAsync(int id);
    public Task<ActivityEntity> CreateAsync(ActivityEntity entity);
    public Task<ActivityEntity?> UpdateAsync(int id, CreateActivityDto activityDto);
    public Task<ActivityEntity?> DeleteAsync(int id);
    public Task SaveChangesAsync();
}