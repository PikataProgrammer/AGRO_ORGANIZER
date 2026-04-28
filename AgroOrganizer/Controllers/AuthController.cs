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
        }).WithName("Login").WithTags("Auth");

        app.MapPost(baseRoute + "/refresh", async (IAuthService service, IJwtUtils jwtUtils, HttpContext context) =>
        {
            var refreshToken = context.Request.Cookies["Refresh-Token"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Results.Unauthorized();
            }

            if (!jwtUtils.ValidateJwtToken(refreshToken, out var jwtToken))
            {
                return Results.Unauthorized();
            }

            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            var result = await service.GenerateTokens(context, int.Parse(userId));

            return Results.Ok(result);
        }).WithName("GenerateTokens").WithTags("Auth");
        
        app.MapPost(baseRoute + "/logout", (HttpContext context) =>
        {
            var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            
            var cookieOptions = new CookieOptions
            {
                SameSite = isDev ? SameSiteMode.Unspecified : SameSiteMode.None,
                Secure = !isDev,
                HttpOnly = true,
                IsEssential = true
            };
            context.Response.Cookies.Delete("Refresh-Token", cookieOptions);
            context.Response.Cookies.Delete("Access-Token", cookieOptions); 

            return Results.Ok(new { message = "Успешно излизане" });
        }).WithName("Logout").WithTags("Auth");

        app.MapPost(baseRoute + "/change-password", async (IAuthService service, ChangePasswordRequestDto dto) =>
        {
            return Results.Ok(await service.ChangePassword(dto));
        }).WithName("ChangePassword").WithTags("Auth");

        app.MapPost(baseRoute + "/reset-password", async (IAuthService  service, ForgottenPasswordRequestDto dto) =>
        {
            return Results.Ok(await service.ResetPassword(dto));
        }).WithName("ResetPassword").WithTags("Auth");
        
        return app;
    }
}