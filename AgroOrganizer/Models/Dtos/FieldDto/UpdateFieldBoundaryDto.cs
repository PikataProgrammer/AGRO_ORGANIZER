namespace AgroOrganizer.Models.Dtos.FieldDto;

public class UpdateFieldBoundaryDto
{
    public string BoundaryJson { get; set; } = string.Empty;
    public decimal? CalculatedSize { get; set; }
}