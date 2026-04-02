using AgroOrganizer.Models.Dtos.DriverDto;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class DriverController
{
    public static WebApplication SetUpDriverRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IDriverService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        }).WithName("GetDrivers").WithTags("Driver");

        app.MapGet(baseRoute + "/{id:int}", async (IDriverService service, int id) =>
        {
            return Results.Ok(await service.GetDriverByIdAsync(id));
        }).WithName("GetDriverById").WithTags("Driver");

        app.MapPost(baseRoute + "/", async (IDriverService service, CreateUpdateDriverDto dto) =>
        {
            return Results.Ok(await service.CreateDriverAsync(dto));
        }).WithName("CreateDriver").WithTags("Driver");

        app.MapPut(baseRoute + "/{id:int}", async (IDriverService service, int id, CreateUpdateDriverDto dto) =>
        {
            return Results.Ok(await service.UpdateDriverAsync(id, dto));
        }).WithName("UpdateDriver").WithTags("Driver");

        app.MapDelete(baseRoute + "/{id:int}", async (IDriverService service, int id) =>
        {
            return Results.Ok(await service.DeleteDriverAsync(id));
        }).WithName("DeleteDriver").WithTags("Driver");
        
        return app;
    }
}