using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class ExpenseController
{
    public static WebApplication SetUpExpenseRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IExpenseService service, int offset, int limit) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        });
        
        app.MapGet("/{id:int}", async (IExpenseService service, int id)
            => Results.Ok(await service.GetByIdAsync(id)));

        app.MapPost("/", async (IExpenseService service, CreateExpenseDto dto)
            => Results.Ok(await service.CreateAsync(dto)));

        app.MapPut("/{id:int}", async (IExpenseService service, int id, UpdateExpenseDto dto)
            => Results.Ok(await service.UpdateAsync(id, dto)));

        app.MapDelete("/{id:int}", async (IExpenseService service, int id)
            => Results.Ok(await service.DeleteAsync(id)));


        return app;
    }
}