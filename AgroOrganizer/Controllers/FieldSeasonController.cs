using AgroOrganizer.Models.Dtos.FieldSeasonDto;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class FieldSeasonController
{
    public static WebApplication SetUpFieldSeasonRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IFieldSeasonService service, int offset = 0, int limit = 10)
            => Results.Ok(await service.GetAllAsync(offset, limit))).WithName("GetAllFieldSeasons").WithTags("FieldSeason");

        app.MapGet(baseRoute + "/{id:int}", async (IFieldSeasonService service, int id)
            => Results.Ok(await service.GetByIdAsync(id))).WithName("GetFieldSeasonById").WithTags("FieldSeason");

        app.MapPost(baseRoute + "/", async (IFieldSeasonService service, CreateFieldSeasonDto dto)
            => Results.Ok(await service.CreateAsync(dto))).WithName("CreateFieldSeason").WithTags("FieldSeason");

        app.MapPut(baseRoute + "/{id:int}", async (IFieldSeasonService service, int id, CreateFieldSeasonDto dto)
            => Results.Ok(await service.UpdateAsync(id, dto))).WithName("UpdateFieldSeason").WithTags("FieldSeason");

        app.MapDelete(baseRoute + "/{id:int}", async (IFieldSeasonService service, int id)
            => Results.Ok(await service.DeleteAsync(id))).WithName("DeleteFieldSeason").WithTags("FieldSeason");
        
        return app;
    }
}