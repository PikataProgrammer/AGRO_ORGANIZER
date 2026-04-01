using System.Security.Claims;
using AgroOrganizer.Models.Dtos.Auth;
using AgroOrganizer.Models.Dtos.LoginDto;
using AgroOrganizer.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity.Data;

namespace AgroOrganizer.Controllers;

public class AuthController
{
    public static WebApplication SetUpAuthRoutes(WebApplication app, string baseRoute)
    {
        app.MapPost(baseRoute + "/login", async (IAuthService service, HttpContext context, LoginRequestDto dto) =>
        {
            var result = await service.Authenticate(context, dto);
            return result is null ? Results.Unauthorized() : Results.Ok(result);
        });

        app.MapPost(baseRoute + "/refresh", async (IAuthService service, HttpContext context, ClaimsPrincipal user) =>
        {
            var userId = user.FindFirst("id")?.Value;
            if (userId == null)
            {
                return Results.Unauthorized();
            }

            var result = await service.GenerateTokens(context, int.Parse(userId));
            return Results.Ok(result);
        });

        app.MapPost(baseRoute + "/change-password", async (IAuthService service, ChangePasswordRequestDto dto) =>
        {
            return Results.Ok(await service.ChangePassword(dto));
        });

        app.MapPost(baseRoute + "/reset-password", async (IAuthService  service, ForgottenPasswordRequestDto dto) =>
        {
            return Results.Ok(await service.ResetPassword(dto));
        });
        
        return app;
    }
}