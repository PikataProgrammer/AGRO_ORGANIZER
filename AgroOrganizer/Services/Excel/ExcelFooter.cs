namespace AgroOrganizer.Services.Excel;

public class ExcelFooter
{
    public string Content { get; set; }
    public int RowHeightInPoints { get; set; } = 20;
    public int Offset { get; set; } = 1;
}