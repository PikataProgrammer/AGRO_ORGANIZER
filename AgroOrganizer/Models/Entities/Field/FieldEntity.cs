using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Entities.User;

namespace AgroOrganizer.Models.Entities.Field;

public class FieldEntity
{
    public int Id { get; private set; }
    public string FieldName { get; private set; }
    public decimal FieldSize { get; private set; }
    public string FieldLocation { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }

    public int UserId { get; private set; }
    public UserEntity User { get; private set; }

    public ICollection<FieldSeasonEntity> Seasons { get; private set; }

    public FieldEntity()
    {
        Seasons = new List<FieldSeasonEntity>();
    }

    public FieldEntity(CreateFieldRequestDto fieldDto, int userId)
    {
        FieldName = fieldDto.FieldName;
        FieldSize = fieldDto.FieldSize;
        FieldLocation = fieldDto.FieldLocation;
        CreatedOn = fieldDto.CreatedOn;
        UserId = userId;
        Seasons = new List<FieldSeasonEntity>();
    }

    public void Update(CreateFieldRequestDto dto)
    {
        FieldName = dto.FieldName;
        FieldSize = dto.FieldSize;
        FieldLocation = dto.FieldLocation;
        CreatedOn = dto.CreatedOn;
    }
}