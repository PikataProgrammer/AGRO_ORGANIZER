using AgroOrganizer.Models.Dtos.DriverDto;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class DriverController
{
    public static WebApplication SetUpDriverRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IDriverService service, int offset, int limit) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        });

        app.MapGet(baseRoute + "/{id:int}", async (IDriverService service, int id) =>
        {
            return Results.Ok(await service.GetDriverByIdAsync(id));
        });

        app.MapPost(baseRoute + "/", async (IDriverService service, CreateUpdateDriverDto dto) =>
        {
            return Results.Ok(await service.CreateDriverAsync(dto));
        });

        app.MapPut(baseRoute + "/{id:int}", async (IDriverService service, int id, CreateUpdateDriverDto dto) =>
        {
            return Results.Ok(await service.UpdateDriverAsync(id, dto));
        });

        app.MapDelete(baseRoute + "/{id:int}", async (IDriverService service, int id) =>
        {
            return Results.Ok(await service.DeleteDriverAsync(id));
        });
        
        return app;
    }
}