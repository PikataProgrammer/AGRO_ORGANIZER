using AgroOrganizer.Models.Dtos.FieldSeasonDto;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class FieldSeasonController
{
    public static WebApplication SetUpFieldSeasonRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet("/", async (IFieldSeasonService service, int offset = 0, int limit = 10)
            => Results.Ok(await service.GetAllAsync(offset, limit)));

        app.MapGet("/{id:int}", async (IFieldSeasonService service, int id)
            => Results.Ok(await service.GetByIdAsync(id)));

        app.MapPost("/", async (IFieldSeasonService service, CreateFieldSeasonDto dto)
            => Results.Ok(await service.CreateAsync(dto)));

        app.MapPut("/{id:int}", async (IFieldSeasonService service, int id, CreateFieldSeasonDto dto)
            => Results.Ok(await service.UpdateAsync(id, dto)));

        app.MapDelete("/{id:int}", async (IFieldSeasonService service, int id)
            => Results.Ok(await service.DeleteAsync(id)));
        
        return app;
    }
}