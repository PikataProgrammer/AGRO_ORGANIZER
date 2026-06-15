using AgroOrganizer.Models.Dtos.StorageDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class StorageController
{
    public static WebApplication SetUpStorageRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IGrainStorageService service)
                => Results.Ok(await service.GetAllStoragesAsync()))
            .WithName("GetAllStorages").WithTags("Storage");
        
        app.MapPost(baseRoute + "/add", async (IGrainStorageService service, StorageTransactionDto dto) =>
        {
            try
            {
                var result = await service.AddQuantityAsync(dto);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        }).WithName("AddStorageQuantity").WithTags("Storage");
        
        app.MapPost(baseRoute + "/remove", async (IGrainStorageService service, StorageTransactionDto dto) =>
        {
            try
            {
                var result = await service.RemoveQuantityAsync(dto);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        }).WithName("RemoveStorageQuantity").WithTags("Storage");

        return app;
    }
}