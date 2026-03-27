using AgroOrganizer.Models.Dtos.ActivityDto;
using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Activity;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    public ActivityService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }
    
    public async Task<List<ActivityDto>> GetAllAsync(int offset, int limit)
    {
        var activities = await _activityRepository.GetAllAsync(offset, limit);
        return activities.Select(a => new ActivityDto(a)).ToList();
    }

    public async Task<ActivityDto?> GetByIdAsync(int activityId)
    {
        var activity = await _activityRepository.GetByIdAsync(activityId);
        if (activity == null)
        {
            throw new NotFoundException($"The activity with id {activityId} was not found.");
        }
        return new ActivityDto(activity);
    }

    public async Task<ActivityDto> CreateAsync(ActivityEntity entity)
    {
        var createdActivity = await _activityRepository.CreateAsync(entity);
        return new ActivityDto(createdActivity);
    }

    public async Task<ActivityDto?> UpdateAsync(int id, CreateActivityDto activityDto)
    {
        var updatedActivity = await _activityRepository.UpdateAsync(id, activityDto);
        if (updatedActivity == null)
        {
            throw new NotFoundException($"The activity with id {updatedActivity} was not found.");
        }
        return new ActivityDto(updatedActivity);
    }

    public async Task<bool> DeleteAsync(int activityId)
    {
        var deletedActivivty = await _activityRepository.DeleteAsync(activityId);
        return deletedActivivty != null;
    }
}