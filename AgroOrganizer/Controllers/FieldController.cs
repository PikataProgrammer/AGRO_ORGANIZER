using AgroOrganizer.Models.Dtos.FieldDto;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class FieldController
{
    public static WebApplication SetUpFieldRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IFieldService service, int offset = 0, int limit = 1000)
            => Results.Ok(await service.GetAllAsync(offset, limit))).WithName("GetAllFields").WithTags("Field");

        app.MapGet(baseRoute + "/{id:int}", async (IFieldService service, int id)
            => Results.Ok(await service.GetByIdAsync(id))).WithName("GetFieldById").WithTags("Field");

        app.MapPost(baseRoute + "/", async (IFieldService service, CreateFieldRequestDto dto)
            => Results.Ok(await service.CreateFieldAsync(dto))).WithName("CreateField").WithTags("Field");

        app.MapPut(baseRoute + "/{id:int}", async (IFieldService service, int id, CreateFieldRequestDto dto)
            => Results.Ok(await service.UpdateFieldAsync(id, dto))).WithName("UpdateField").WithTags("Field");
        
        app.MapPut(baseRoute + "/{id:int}/boundary", async (IFieldService service, int id, UpdateFieldBoundaryDto dto) =>
        {
            try
            {
                var result = await service.UpdateBoundaryAsync(id, dto);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        }).WithName("UpdateFieldBoundary").WithTags("Field");

        app.MapDelete(baseRoute + "/{id:int}", async (IFieldService service, int id)
            => Results.Ok(await service.DeleteFieldAsync(id))).WithName("DeleteField").WithTags("Field");
        
        return app;
    }
}