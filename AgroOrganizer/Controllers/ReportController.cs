
using AgroOrganizer.Services.Reports;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AgroOrganizer.Controllers;

public class ReportController
{
    public static WebApplication SetUpReportRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/field/excel", async (ReportFieldService reportFieldService, [FromQuery] int? cropType) =>
        {
            var file = await reportFieldService.GenerateFieldExcel(cropType);

            if (file == null)
            {
                Log.Error("Fields excel generation returned null stream");
                return Results.BadRequest("Failed to generate excel file");
            }
            //Because browser didn't recognize "excel" like a word
            const string ExcelMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            
            return Results.File(
                file,
                ExcelMimeType,
                "FieldReport.xlsx");
        }).WithName("ExportFieldExcel").WithTags("Reports");

        app.MapGet(baseRoute + "/expense/excel", async (ReportExpensesService reportExpensesService) =>
        {
            var file = await reportExpensesService.GenerateExpenseExcel();

            if (file == null)
            {
                Log.Error("Expenses excel generation returned null stream");
                return Results.BadRequest("Failed to generate excel file");
            }
            const string ExcelMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return Results.File(file, ExcelMimeType, "ExpensesReport.xlsx");
        }).WithName("ExportExpenseExcel").WithTags("Reports");
        
        return app;
    }
}