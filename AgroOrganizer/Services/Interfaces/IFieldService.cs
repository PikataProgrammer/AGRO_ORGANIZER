using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Entities.Field;

namespace AgroOrganizer.Services.Interfaces;

public interface IFieldService
{
    Task<FieldDto?> GetByIdAsync(int fieldId);
    Task<List<FieldDto>> GetAllAsync(int offset, int limit);
    Task<FieldDto> CreateFieldAsync(FieldEntity entity);
    Task<FieldDto?> UpdateFieldAsync(int fieldId, CreateFieldRequestDto fieldDto);
    Task<bool> DeleteFieldAsync(int fieldId);
}