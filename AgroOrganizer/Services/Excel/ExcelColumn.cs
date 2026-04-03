

using NPOI.SS.UserModel;

namespace AgroOrganizer.Services.Excel;

public class ExcelColumn
{
    public string Label { get; set; }
    public HorizontalAlignment? HorizontalAlignment { get; set; }
    public double? Width { get; set; }
}