using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Dtos.FieldSeasonDto;

public class FieldSeasonDto
{
    public int Id { get; set; }
    public int Year { get; set; }
    public CropTypes CropType { get; set; }

    public int FieldId { get; set; }

    public FieldSeasonDto(FieldSeasonEntity entity)
    {
        Id = entity.Id;
        Year = entity.Year;
        CropType = entity.CropType;
        FieldId = entity.FieldId;
    }
}