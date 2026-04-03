using AgroOrganizer.Models.Dtos.FieldDto;

namespace AgroOrganizer.Services.Excel;

public class ExcelOptions
{
    public ExcelTitle? ExcelTitle { get; set; }
    public ExcelFooter? ExcelFooter { get; set; }
    public bool AutoSizeColumns { get;  set; }
    public Dictionary<string, ExcelColumn> Columns { get; private set; } = new();
}