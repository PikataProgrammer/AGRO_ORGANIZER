using AgroOrganizer.Models.Dtos.ContractDto;
using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class ContractController
{
    public static WebApplication SetUpContractRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IContractService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        }).WithName("GetContracts").WithTags("Contract");

        app.MapGet(baseRoute + "/{id:int}", async (IContractService service, int id) =>
        {
            return Results.Ok(await service.GetByIdAsync(id));
        }).WithName("GetContractById").WithTags("Contract");

        app.MapPost(baseRoute, async (IContractService service, CreateContractDto dto) =>
        { 
            return Results.Ok(await service.CreateContractAsync(dto));
        }).WithName("CreateContract").WithTags("Contract");

        app.MapPut(baseRoute + "/{id:int}", async (IContractService service, int id, UpdateContractDto dto) =>
        {
            return Results.Ok(await service.UpdateContractAsync(id, dto));
        }).WithName("UpdateContract").WithTags("Contract");

        app.MapDelete(baseRoute + "/{id:int}", async (IContractService service, int id) =>
        {
            return Results.Ok(await service.DeleteContractAsync(id));
        }).WithName("DeleteContract").WithTags("Contract");
        
        return app;
    }
}