using AgroOrganizer.Models.Dtos.VehiclesDto.VehicleServiceDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class VehicleServiceController
{
    public static WebApplication SetUpVehicleServiceRoutes(WebApplication app ,string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IVehicleMaintenanceService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        }).WithName("GetAllVehicleServices").WithTags("VehicleService");

        app.MapGet(baseRoute + "/{id:int}", async (IVehicleMaintenanceService service, int id) =>
        {
            return Results.Ok(await service.GetAllByVehicleIdAsync(id)); 
        }).WithName("GetAllServicesForVehicle").WithTags("VehicleService");

        app.MapPost(baseRoute + "/", async (IVehicleMaintenanceService service, CreateVehicleServiceDto dto) =>
        {
            return Results.Ok(await service.CreateVehicleAsync(dto));
        }).WithName("CreateVehicleService").WithTags("VehicleService");

        app.MapPut(baseRoute + "/{id:int}", async (IVehicleMaintenanceService service, int id, CreateVehicleServiceDto dto) =>
        {
            return Results.Ok(await service.UpdateVehicleAsync(id, dto));
        }).WithName("UpdateVehicleService").WithTags("VehicleService");

        app.MapDelete(baseRoute + "/{id:int}", async (IVehicleMaintenanceService service, int id) =>
        {
            return Results.Ok(await service.DeleteVehicleAsync(id));
        }).WithName("DeleteVehicleService").WithTags("VehicleService");
        
        return app;
    }
}