using AgroOrganizer.Models.Dtos.ActivityDto;
using AgroOrganizer.Models.Entities.Activity;

namespace AgroOrganizer.Services.Interfaces;

public interface IActivityService
{
    Task<List<ActivityDto>> GetAllAsync(int offset, int limit);
    Task<ActivityDto?> GetByIdAsync(int activityId);
    Task<ActivityDto> CreateAsync(ActivityEntity entity);
    Task<ActivityDto?> UpdateAsync(int id, CreateActivityDto activityDto);
    Task<bool> DeleteAsync(int activityId);
}