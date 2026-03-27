using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.Field;

public class FieldService : IFieldService
{
    private readonly IFieldRepository _fieldRepository;

    public FieldService(IFieldRepository fieldRepository)
    {
        _fieldRepository = fieldRepository;
    }
    public async Task<FieldDto?> GetByIdAsync(int fieldId)
    {
        var field = await _fieldRepository.GetFieldByIdAsync(fieldId);
        if (field == null)
        {
            throw new NotFoundException($"Field with id {fieldId} not found");
        }
        return new FieldDto(field);
    }

    public async Task<List<FieldDto>> GetAllAsync(int offset, int limit)
    {
        var fields = await _fieldRepository.GetAllAsync(offset, limit);
        return fields.Select(f => new FieldDto(f)).ToList();
    }

    public async Task<FieldDto> CreateFieldAsync(FieldEntity entity)
    {
        var createdField = await _fieldRepository.CreateFieldAsync(entity);
        return new FieldDto(createdField);
    }

    public async Task<FieldDto?> UpdateFieldAsync(int fieldId, CreateFieldRequestDto fieldDto)
    {
        var updatedField = await _fieldRepository.UpdateFieldAsync(fieldId, fieldDto);
        if (updatedField == null)
        {
            throw new NotFoundException($"Field with id {fieldId} not found");
        }
        return new FieldDto(updatedField);
    }

    public async Task<bool> DeleteFieldAsync(int fieldId)
    {
        var deletedField = await _fieldRepository.DeleteFieldAsync(fieldId);
        return  deletedField != null;
    }
}