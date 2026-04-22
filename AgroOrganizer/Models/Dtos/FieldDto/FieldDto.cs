using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Entities.User;
using AgroOrganizer.Models.Enums.CropTypes;
using AgroOrganizer.Models.Enums.FieldOperationTypes;

namespace AgroOrganizer.Models.Dtos.FieldDto;

public class FieldDto
{
    public int FieldId { get; set; }
    public string FieldName { get;  set; }
    public decimal FieldSize { get;  set; }
    public string? FieldLocation { get;  set; }
    
    public DateTimeOffset? CreatedOn { get; set; } 
    public ContractDto.ContractDto? Contract { get; set; }
    public ICollection<FieldSeasonDto.FieldSeasonDto> Seasons { get; set; }
    public int? UserId { get; set; }

    public FieldDto(FieldEntity field)
    {
        FieldId = field.Id;
        FieldName = field.FieldName;
        FieldSize = field.FieldSize;
        FieldLocation = field.FieldLocation;
        UserId = field.UserId;
        CreatedOn = field.CreatedOn;
        Seasons = new List<FieldSeasonDto.FieldSeasonDto>();
        foreach (var season in field.Seasons)
        {
            Seasons.Add(new FieldSeasonDto.FieldSeasonDto(season));
        }
        if (field.Contract != null)
        {
            Contract = new ContractDto.ContractDto(field.Contract);
        }
    }

}