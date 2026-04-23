using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Dtos.Reports;
using AgroOrganizer.Services.Excel;
using AgroOrganizer.Services.Excel.Interface;
using AgroOrganizer.Services.Interfaces;
using Serilog;

namespace AgroOrganizer.Services.Reports;

public class ReportFieldService
{
    private readonly IFieldService _fieldService;
    private readonly IExcelService _excelService;

    public ReportFieldService(IFieldService fieldService, IExcelService excelService)
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
        var currentYear = DateTime.Now.Year;
        return new ExcelOptions
        {
            ExcelTitle = new ExcelTitle { Content = $"Справка Ниви - {currentYear}" },
            ExcelFooter = new ExcelFooter { Content = "Генерирана от Агро Дерменджиеви" },
            Columns =
            {
                { "FieldName", new ExcelColumn { Label = "Нива" } },
                { "FieldSize", new ExcelColumn { Label = "Размер (дка)" } },
                { "FieldLocation", new ExcelColumn { Label = "Местоположение" } },
                { "CropType", new ExcelColumn { Label = "Вид култура" } },
                { "ActivityType", new ExcelColumn { Label = "Статус" } },
                { "DriverName", new ExcelColumn { Label = "Шофьор" } },
            }
        };
    }

    public List<FieldReportDto> FlattenFields(List<FieldDto> fields)
    {
        var result = new List<FieldReportDto>();
        
        var targetYear = DateTime.Now.Year; 

        foreach (var field in fields)
        {
            var currentSeason = field.Seasons?.FirstOrDefault(s => s.Year == targetYear);
            
            if (currentSeason == null)
            {
                result.Add(new FieldReportDto
                {
                    FieldName = field.FieldName,
                    FieldSize = field.FieldSize,
                    FieldLocation = field.FieldLocation ?? "-",
                    Year = targetYear,
                    CropType = "Няма активен сезон",
                    ActivityType = "Необработена",
                    DriverName = "-"
                });
                continue;
            }
            
            var latestActivity = currentSeason.Activities?.OrderByDescending(a => a.Date).FirstOrDefault();

            string driverName = "-";
            string activityName = "Необработена";
            DateTimeOffset? activityDate = null;
            string activityNotes = "";

            if (latestActivity != null)
            {
                activityName = GetOperationNameBg((int)latestActivity.Type);
                driverName = latestActivity.DriverName ?? $"ID: {latestActivity.DriverId}";
                activityDate = latestActivity.Date;
                activityNotes = latestActivity.Notes ?? "";
            }

            result.Add(new FieldReportDto
            {
                FieldName = field.FieldName,
                FieldSize = field.FieldSize,
                FieldLocation = field.FieldLocation ?? "-",
                Year = currentSeason.Year,
                CropType = GetCropNameBg((int)currentSeason.CropType),
                ActivityType = activityName,
                DriverName = driverName,
                ActivityDate = activityDate,
                ActivityNotes = activityNotes
            });
        }

        return result;
    }
    

    private string GetCropNameBg(int cropValue)
    {
        return cropValue switch
        {
            1 => "Пшеница",
            2 => "Ръж",
            3 => "Грах",
            4 => "Фацелия",
            5 => "Слънчоглед",
            6 => "Царевица",
            7 => "Угар (Празно)",
            8 => "Люцерна",
            9 => "Изкуствени ливади",
            _ => "Неизвестно"
        };
    }

    private string GetOperationNameBg(int opValue)
    {
        return opValue switch
        {
            1 => "Изорана",
            2 => "Посята",
            3 => "Наторена",
            4 => "Напръскана",
            5 => "Ожъната / Окосена",
            6 => "Издискована",
            7 => "Няма",
            _ => "Необработена"
        };
    }
}