using System.ComponentModel.DataAnnotations.Schema;
using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Entities.User;

namespace AgroOrganizer.Models.Entities.Field;

public class FieldEntity
{
    public int Id { get; private set; }
    public string FieldNumber { get; private set; }
    public string FieldName { get; private set; }
    public decimal FieldSize { get; private set; }
    public string? FieldLocation { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    
    public string? BoundaryJson { get; private set; }
    public int UserId { get; private set; }
    [ForeignKey("UserId")]
    public UserEntity User { get; private set; }
    
    public ContractEntity? Contract { get; private set; }

    public ICollection<FieldSeasonEntity> Seasons { get; private set; }

    public FieldEntity()
    {
        Seasons = new List<FieldSeasonEntity>();
    }

    public FieldEntity(CreateFieldRequestDto fieldDto)
    {
        FieldNumber = fieldDto.FieldNumber;
        FieldName = fieldDto.FieldName;
        FieldSize = fieldDto.FieldSize;
        FieldLocation = fieldDto.FieldLocation;
        CreatedOn = fieldDto.CreatedOn;
        UserId = fieldDto.UserId;
        Seasons = new List<FieldSeasonEntity>();
    }

    public void Update(CreateFieldRequestDto dto)
    {
        FieldNumber = dto.FieldNumber;
        FieldName = dto.FieldName;
        FieldSize = dto.FieldSize;
        FieldLocation = dto.FieldLocation;
        CreatedOn = dto.CreatedOn;
        UserId = dto.UserId;
    }
    
    public void UpdateBoundary(string? json, decimal? calculatedSize = null) 
    {
        if (string.IsNullOrEmpty(json) || json == "null" || json == "[]")
        {
            BoundaryJson = null;
        }
        else
        {
            BoundaryJson = json;
        }
        if (calculatedSize.HasValue && calculatedSize.Value > 0)
        {
            FieldSize = calculatedSize.Value;
        }
    }
}