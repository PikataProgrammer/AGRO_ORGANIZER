
using AgroOrganizer.Services.Reports;
using Serilog;

namespace AgroOrganizer.Controllers;

public class ReportController
{
    public static WebApplication SetUpReportRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/excel", async (ReportService reportService) =>
        {
            var file = await reportService.GenerateFieldExcel();

            if (file == null)
            {
                Log.Error("Excel generation returned null stream");
                return Results.BadRequest("Failed to generate excel file");
            }
            
            return Results.File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "FieldReport.xlsx");
        }).WithName("Export-Excel").WithTags("Reports");
        
        return app;
    }
}