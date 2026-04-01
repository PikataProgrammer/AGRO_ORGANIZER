using AgroOrganizer.Models.Dtos.ActivityDto;
using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroOrganizer.Controllers;

public class ActivityController
{
    public static WebApplication SetUpActivityRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IActivityService service, int offset, int limit) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        });
        
        app.MapGet(baseRoute + "/{id:int}", async (IActivityService service, int id) =>
        {
            return Results.Ok(await service.GetByIdAsync(id));
        });

        app.MapPost(baseRoute + "/", async (IActivityService service, CreateActivityDto dto) =>
        {
            return Results.Ok(await service.CreateAsync(dto));
        });

        app.MapPut(baseRoute + "/{id:int}", async (IActivityService service, int id, CreateActivityDto dto) =>
        {
            return Results.Ok(await service.UpdateAsync(id, dto));
        });
           
        app.MapDelete(baseRoute + "/{id:int}", async (IActivityService service, int id) =>
        {
            return Results.Ok(await service.DeleteAsync(id));
        });
        
        return app;
    }
}