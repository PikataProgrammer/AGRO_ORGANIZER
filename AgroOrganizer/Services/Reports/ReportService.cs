using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Dtos.Reports;
using AgroOrganizer.Services.Excel;
using AgroOrganizer.Services.Excel.Interface;
using AgroOrganizer.Services.Interfaces;
using Serilog;

namespace AgroOrganizer.Services.Reports;

public class ReportService
{
    private readonly IFieldService _fieldService;
    private readonly IExcelService _excelService;

    public ReportService(IFieldService fieldService, IExcelService excelService)
    {
        _excelService = excelService;
        _fieldService = fieldService;
    }

    public async Task<MemoryStream?> GenerateFieldExcel()
    {
        var fields = await _fieldService.GetAllFieldsWithSeasons();

        if (fields == null || !fields.Any())
        {
            Log.Warning("No fields found in database.");
            return null;
        }

        foreach (var field in fields)
        {
            if (field.Seasons == null || !field.Seasons.Any())
                Log.Warning("Field {FieldName} has no seasons.", field.FieldName);

            foreach (var season in field.Seasons)
            {
                if (season.Activities == null || !season.Activities.Any())
                    Log.Information("Season {Year} of field {FieldName} has no activities.", season.Year, field.FieldName);

                if (season.Expenses == null || !season.Expenses.Any())
                    Log.Information("Season {Year} of field {FieldName} has no expenses.", season.Year, field.FieldName);

                if (season.Sales == null || !season.Sales.Any())
                    Log.Information("Season {Year} of field {FieldName} has no sales.", season.Year, field.FieldName);
            }
        }
        Log.Information("Total fields fetched: {Count}", fields.Count);

        var reportData = FlattenFields(fields);

        if (reportData == null || !reportData.Any())
        {
            Log.Warning("After flattening, no report data was generated.");
            return null;
        }

        var excelOptions = GetExcelOptions();
        return _excelService.GenerateExcel(reportData, excelOptions);
    }

    private ExcelOptions GetExcelOptions()
    {
        return new ExcelOptions
        {
            ExcelTitle = new ExcelTitle { Content = "Справка Ниви" },
            ExcelFooter = new ExcelFooter { Content = "Генерирана от Агро Дерменджиеви" },
            Columns =
            {
                { "FieldName", new ExcelColumn { Label = "Нива" } },
                { "FieldSize", new ExcelColumn { Label = "Размер (дка)" } },
                { "FieldLocation", new ExcelColumn { Label = "Местоположение" } },
                { "Year", new ExcelColumn { Label = "Година" } },
                { "CropType", new ExcelColumn { Label = "Вид култура" } },
                { "ActivityType", new ExcelColumn { Label = "Статус" } },
                { "DriverName", new ExcelColumn { Label = "Шофьор" } },
                { "ExpenseAmount", new ExcelColumn { Label = "Общо разходи" } },
                { "SaleTotal", new ExcelColumn { Label = "Общо приходи" } },
            }
        };
    }


    public List<FieldReportDto> FlattenFields(List<FieldDto> fields)
{
    var result = new List<FieldReportDto>();

    foreach (var field in fields)
    {
        // Ако нивата няма сезони, пак можем да я покажем като празен ред (по желание)
        if (field.Seasons == null || !field.Seasons.Any())
        {
            result.Add(new FieldReportDto
            {
                FieldName = field.FieldName,
                FieldSize = field.FieldSize,
                FieldLocation = field.FieldLocation,
                Year = 0,
                CropType = "N/A"
            });
            continue;
        }

        foreach (var season in field.Seasons)
        {
            var driverNames = season.Activities != null && season.Activities.Any()
                ? string.Join(", ", season.Activities
                    .Select(a => a.DriverName ?? $"ID: {a.DriverId}") 
                    .Distinct())
                : "No driver";
            // Обединяваме всички активности в един стринг, за да не заема много редове
            var activityNames = season.Activities != null && season.Activities.Any()
                ? string.Join(", ", season.Activities.Select(a => a.Type.ToString()))
                : "No activities";

            // Вземаме последната дата на активност (или null)
            var lastActivityDate = season.Activities?.OrderByDescending(a => a.Date).FirstOrDefault()?.Date;

            // Сумираме разходите и продажбите за сезона
            decimal totalExpenses = season.Expenses?.Sum(e => e.Amount) ?? 0;
            decimal totalSales = season.Sales?.Sum(s => s.TotalPrice) ?? 0;

            var dto = new FieldReportDto
            {
                FieldName = field.FieldName,
                FieldSize = field.FieldSize,
                FieldLocation = field.FieldLocation,
                
                Year = season.Year,
                CropType = season.CropType.ToString(),
                
                ActivityType = activityNames,
                DriverName = driverNames,
                ActivityDate = lastActivityDate,
                
                // Финансови данни (сумирани)
                ExpenseAmount = totalExpenses,
                SaleTotal = totalSales,
                
                // Бележка (вземаме първата налична или празно)
                ActivityNotes = season.Activities?.FirstOrDefault(a => !string.IsNullOrEmpty(a.Notes))?.Notes ?? ""
            };

            Log.Debug("Adding flattened row for Field: {FieldName}, Year: {Year}", dto.FieldName, dto.Year);
            result.Add(dto);
        }
    }

    return result;
}
}