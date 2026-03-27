
using AgroOrganizer.Models.Dtos.FieldSeasonDto;
using AgroOrganizer.Models.Entities.FieldSeason;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IFieldSeasonRepository
{
    public Task<List<FieldSeasonEntity>> GetAllAsync(int offset, int limit);
    public Task<FieldSeasonEntity?> GetByIdAsync(int id);
    public Task<FieldSeasonEntity> CreateAsync(FieldSeasonEntity entity);
    public Task<FieldSeasonEntity?> UpdateAsync(int id, CreateFieldSeasonDto dto);
    public Task<FieldSeasonEntity?> DeleteAsync(int id);
    public Task SaveChangesAsync();
}