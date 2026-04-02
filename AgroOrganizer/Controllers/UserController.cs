using AgroOrganizer.Models.Dtos.UserDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class UserController
{
    public static WebApplication SetUpUserRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IUserService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAll(limit, offset));
        }).WithName("GetUsers").WithTags("User");

        app.MapGet(baseRoute + "/{id:int}", async (IUserService service, int id) =>
        {
            return Results.Ok(await service.GetById(id));
        }).WithName("GetUserById").WithTags("User");

        app.MapPost(baseRoute + "/", async (IUserService service, CreateUserRequestDto dto) =>
        {
            var result = await service.CreateUser(dto);
            return Results.Ok(result);
        }).WithName("CreateUser").WithTags("User");

        app.MapPut(baseRoute + "/{id:int}", async (IUserService service, int id, UpdateUserRequestDto dto) =>
        {
            return Results.Ok(await service.UpdateUser(dto, id));
        }).WithName("UpdateUser").WithTags("User");

        app.MapDelete(baseRoute + "/{id:int}", async (IUserService service, int id) =>
        {
            return Results.Ok(await service.DeleteUser(id));
        }).WithName("DeleteUser").WithTags("User");
        
        return app;
    }
}