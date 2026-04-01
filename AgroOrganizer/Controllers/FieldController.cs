using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class FieldController
{
    public static WebApplication SetUpFieldRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet("/", async (IFieldService service, int offset = 0, int limit = 10)
            => Results.Ok(await service.GetAllAsync(offset, limit)));

        app.MapGet("/{id:int}", async (IFieldService service, int id)
            => Results.Ok(await service.GetByIdAsync(id)));

        app.MapPost("/", async (IFieldService service, CreateFieldRequestDto dto)
            => Results.Ok(await service.CreateFieldAsync(dto)));

        app.MapPut("/{id:int}", async (IFieldService service, int id, CreateFieldRequestDto dto)
            => Results.Ok(await service.UpdateFieldAsync(id, dto)));

        app.MapDelete("/{id:int}", async (IFieldService service, int id)
            => Results.Ok(await service.DeleteFieldAsync(id)));
        
        return app;
    }
}