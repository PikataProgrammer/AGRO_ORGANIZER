using AgroOrganizer.Models.Dtos.FieldSeasonDto;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services.FieldSeason;

public class FieldSeasonDtoService : IFieldSeasonService
{
    private readonly IFieldSeasonRepository _fieldSeasonRepository;

    public FieldSeasonDtoService(IFieldSeasonRepository fieldSeasonRepository)
    {
        _fieldSeasonRepository = fieldSeasonRepository;
    }
    public async Task<List<FieldSeasonDto>> GetAllAsync(int offset, int limit)
    {
        var fieldSeasons = await _fieldSeasonRepository.GetAllAsync(offset, limit);
        return fieldSeasons.Select(x => new FieldSeasonDto(x)).ToList();
    }

    public async Task<FieldSeasonDto?> GetByIdAsync(int fieldId)
    {
        var fieldSeason = await _fieldSeasonRepository.GetByIdAsync(fieldId);
        if (fieldSeason == null)
        {
            throw new NotFoundException($"FieldSeason with id {fieldId} does not exist");
        }
        return new FieldSeasonDto(fieldSeason);
    }

    public async Task<FieldSeasonDto> CreateAsync(FieldSeasonEntity entity)
    {
        var createdField = await _fieldSeasonRepository.CreateAsync(entity);
        return new FieldSeasonDto(createdField);
    }

    public async Task<FieldSeasonDto?> UpdateAsync(int fieldId, CreateFieldSeasonDto dto)
    {
        var updatedField = await _fieldSeasonRepository.UpdateAsync(fieldId, dto);
        if (updatedField == null)
        {
            throw new NotFoundException($"FieldSeason with id {fieldId} does not exist");
        }
        return new FieldSeasonDto(updatedField);
    }

    public async Task<bool> DeleteAsync(int fieldId)
    {
        var deleted = await _fieldSeasonRepository.DeleteAsync(fieldId);
        return deleted != null;
    }
}