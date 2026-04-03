namespace AgroOrganizer.Models.Dtos.Reports;

public class FieldReportDto
{
    public decimal FieldSize { get; set; }
    public string FieldLocation { get; set; }

    public string FieldName { get; set; }
    public int Year { get; set; }
    public string CropType { get; set; }
    public string? ActivityType { get; set; }
    public DateTimeOffset? ActivityDate { get; set; }
    public string? ActivityNotes { get; set; }
    
    public decimal? ExpenseAmount { get; set; }
    public decimal? SaleTotal { get; set; }
    public string? DriverName { get; set; }
}