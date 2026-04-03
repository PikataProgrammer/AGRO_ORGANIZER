namespace AgroOrganizer.Services.Excel.Interface;

public interface IExcelService
{
    MemoryStream? GenerateExcel<T>(List<T> data, ExcelOptions excelOptions);
}