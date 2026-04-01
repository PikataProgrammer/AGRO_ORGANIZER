using AgroOrganizer.Models.Dtos.ContractDto;
using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class ContractController
{
    public static WebApplication SetUpContractRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IContractService service, int offset, int limit) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        });

        app.MapGet(baseRoute + "/{id:int}", async (IContractService service, int id) =>
        {
            return Results.Ok(await service.GetByIdAsync(id));
        });

        app.MapPost(baseRoute, async (IContractService service, CreateContractDto dto) =>
        { 
            return Results.Ok(await service.CreateContractAsync(dto));
        });

        app.MapPut(baseRoute + "/{id:int}", async (IContractService service, int id, UpdateContractDto dto) =>
        {
            return Results.Ok(await service.UpdateContractAsync(id, dto));
        });

        app.MapDelete(baseRoute + "/{id:int}", async (IContractService service, int id) =>
        {
            return Results.Ok(await service.DeleteContractAsync(id));
        });
        
        return app;
    }
}