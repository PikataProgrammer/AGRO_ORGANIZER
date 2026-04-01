using AgroOrganizer.Models.Dtos.SalesDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class SaleController
{
    public static WebApplication SetUpSaleRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (ISaleService service, int offset, int limit) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        });

        app.MapGet(baseRoute + "/{id:int}", async (ISaleService service, int id) =>
        {
            return Results.Ok(await service.GetByIdAsync(id));
        });

        app.MapPost(baseRoute + "/", async (ISaleService service, UpdateSalesRequestDto dto) =>
        {
            return Results.Ok(await service.CreateSaleAsync(dto));
        });

        app.MapPut(baseRoute + "/{id:int}", async (ISaleService service, int id, UpdateSalesRequestDto dto) =>
        {
            return Results.Ok(await service.UpdateSaleAsync(id, dto));
        });

        app.MapDelete(baseRoute + "/{id:int}", async (ISaleService service, int id) =>
        {
            return Results.Ok(await service.DeleteSaleAsync(id));
        });
        
        return app;
    }
}