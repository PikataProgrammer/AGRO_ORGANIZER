using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Models.Entities.Field;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IFieldRepository
{
    public Task<List<FieldEntity>> GetAllAsync(int offset, int limit);
    public Task<FieldEntity?> GetFieldByIdAsync(int id);
    public Task<FieldEntity> CreateFieldAsync(FieldEntity entity);
    public Task<FieldEntity?> UpdateFieldAsync(int id, CreateFieldRequestDto fieldModelState);
    public Task<FieldEntity?> DeleteFieldAsync(int id);
    public Task SaveChangesAsync();
    Task<List<FieldEntity>> GetAllWithSeasonsAsync();
}