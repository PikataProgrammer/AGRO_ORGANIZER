using AgroOrganizer.Models.Enums.CropTypes;

namespace AgroOrganizer.Models.Dtos.FieldSeasonDto;

public class CreateFieldSeasonDto
{
    public int Year { get; set; }
    public CropTypes CropType { get; set; }
}