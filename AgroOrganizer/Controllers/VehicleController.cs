using AgroOrganizer.Models.Dtos.VehiclesDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class VehicleController
{
    public static WebApplication SetUpVehiclesRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IVehiclesService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        }).WithName("GetAllVehicles").WithTags("Vehicle");

        app.MapGet(baseRoute + "/{id:int}", async (IVehiclesService service, int id) =>
        {
            return Results.Ok(await service.GetByIdAsync(id));
        }).WithName("GetVehicleById").WithTags("Vehicle");

        app.MapPost(baseRoute + "/", async (IVehiclesService service, CreateVehicleDto dto) =>
        {
            return Results.Ok(await service.CreateVehicleAsync(dto));
        }).WithName("CreateVehicle").WithTags("Vehicle");

        app.MapPut(baseRoute + "/{id:int}", async (IVehiclesService service, int id, CreateVehicleDto dto) =>
        {
            return Results.Ok(await service.UpdateVehicleAsync(id, dto));
        }).WithName("UpdateVehicle").WithTags("Vehicle");

        app.MapDelete(baseRoute + "/{id:int}", async (IVehiclesService service, int id) =>
        {
            return Results.Ok(await service.DeleteVehicleAsync(id));
        }).WithName("DeleteVehicle").WithTags("Vehicle");
        
        return app;
    }
}