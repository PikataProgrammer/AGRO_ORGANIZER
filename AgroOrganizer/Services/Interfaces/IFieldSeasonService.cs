using AgroOrganizer.Models.Dtos.FieldSeasonDto;
using AgroOrganizer.Models.Entities.FieldSeason;

namespace AgroOrganizer.Services.Interfaces;

public interface IFieldSeasonService
{
    Task<List<FieldSeasonDto>> GetAllAsync(int offset, int limit);
    Task<FieldSeasonDto?> GetByIdAsync(int fieldId);
    Task<FieldSeasonDto> CreateAsync(FieldSeasonEntity entity);
    Task<FieldSeasonDto?> UpdateAsync(int fieldId, CreateFieldSeasonDto dto);
    Task<bool> DeleteAsync(int fieldId);
}