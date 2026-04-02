using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class SaleController
{
    public static WebApplication SetUpSaleRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (ISaleService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        }).WithName("GetAllSales").WithTags("Sale");

        app.MapGet(baseRoute + "/{id:int}", async (ISaleService service, int id) =>
        {
            return Results.Ok(await service.GetByIdAsync(id));
        }).WithName("GetSaleById").WithTags("Sale");

        app.MapPost(baseRoute + "/", async (ISaleService service, UpdateSalesRequestDto dto) =>
        {
            return Results.Ok(await service.CreateSaleAsync(dto));
        }).WithName("CreateSale").WithTags("Sale");

        app.MapPut(baseRoute + "/{id:int}", async (ISaleService service, int id, UpdateSalesRequestDto dto) =>
        {
            return Results.Ok(await service.UpdateSaleAsync(id, dto));
        }).WithName("UpdateSale").WithTags("Sale");

        app.MapDelete(baseRoute + "/{id:int}", async (ISaleService service, int id) =>
        {
            return Results.Ok(await service.DeleteSaleAsync(id));
        }).WithName("DeleteSale").WithTags("Sale");
        
        return app;
    }
}