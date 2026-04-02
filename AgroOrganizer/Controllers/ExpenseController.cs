using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class ExpenseController
{
    public static WebApplication SetUpExpenseRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IExpenseService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        }).WithName("GetExpenses").WithTags("Expense");
        
        app.MapGet(baseRoute + "/{id:int}", async (IExpenseService service, int id)
            => Results.Ok(await service.GetByIdAsync(id))).WithName("GetExpenseById").WithTags("Expense");

        app.MapPost(baseRoute + "/", async (IExpenseService service, CreateExpenseDto dto)
            => Results.Ok(await service.CreateAsync(dto))).WithName("CreateExpense").WithTags("Expense");

        app.MapPut(baseRoute + "/{id:int}", async (IExpenseService service, int id, UpdateExpenseDto dto)
            => Results.Ok(await service.UpdateAsync(id, dto))).WithName("UpdateExpense").WithTags("Expense");

        app.MapDelete(baseRoute + "/{id:int}", async (IExpenseService service, int id)
            => Results.Ok(await service.DeleteAsync(id))).WithName("DeleteExpense").WithTags("Expense");

        return app;
    }
}