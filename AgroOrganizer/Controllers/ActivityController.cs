using AgroOrganizer.Models.Dtos.ActivityDto;
using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroOrganizer.Controllers;

public class ActivityController
{
    public static WebApplication SetUpActivityRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IActivityService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        }).WithName("GetActivities").WithTags("Activity");
        
        app.MapGet(baseRoute + "/{id:int}", async (IActivityService service, int id) =>
        {
            return Results.Ok(await service.GetByIdAsync(id));
        }).WithName("GetActivityById").WithTags("Activity");

        app.MapPost(baseRoute + "/", async (IActivityService service, CreateActivityDto dto) =>
        {
            return Results.Ok(await service.CreateAsync(dto));
        }).WithName("CreateActivity").WithTags("Activity");

        app.MapPut(baseRoute + "/{id:int}", async (IActivityService service, int id, CreateActivityDto dto) =>
        {
            return Results.Ok(await service.UpdateAsync(id, dto));
        }).WithName("UpdateActivity").WithTags("Activity");
           
        app.MapDelete(baseRoute + "/{id:int}", async (IActivityService service, int id) =>
        {
            return Results.Ok(await service.DeleteAsync(id));
        }).WithName("DeleteActivity").WithTags("Activity");
        
        return app;
    }
}