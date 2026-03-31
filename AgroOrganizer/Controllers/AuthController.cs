using AgroOrganizer.Models.Dtos.LoginDto;
using AgroOrganizer.Services.Auth.Interfaces;

namespace AgroOrganizer.Controllers;

public class AuthController
{
    public static void MapAuthEndpoints(WebApplication app, string baseRoute)
    {
        app.MapPost(baseRoute + "/auth/login",
            async (
                LoginRequestDto dto, IAuthService authService) =>
            {
                var result  = await authService.Login(dto)
            })
    }
}