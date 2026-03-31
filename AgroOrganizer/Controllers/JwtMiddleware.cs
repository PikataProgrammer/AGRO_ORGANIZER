using System.Text.Json;
using AgroOrganizer.Models.Dtos.LoginDto;
using AgroOrganizer.Services.Auth.Interfaces;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAuthService authService, IJwtUtils jwtUtils)
    {
        // Skip swagger and auth endpoints
        var path = context.Request.Path.Value?.ToLower() ?? "";
        if (path.Contains("swagger") || path.EndsWith("/auth/login") || path.EndsWith("/auth/changepassword") || path.EndsWith("/auth/resetpassword"))
        {
            await _next(context);
            return;
        }

        var accessToken = context.Request.Cookies["Access-Token"];
        var refreshToken = context.Request.Cookies["Refresh-Token"];

        if (string.IsNullOrEmpty(accessToken) && string.IsNullOrEmpty(refreshToken))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        // Validate Access Token
        if (!string.IsNullOrEmpty(accessToken) && jwtUtils.ValidateJwtToken(accessToken, out var jwtAccessToken))
        {
            var userClaim = jwtAccessToken?.Claims.FirstOrDefault(c => c.Type == "user")?.Value;
            if (!string.IsNullOrEmpty(userClaim))
            {
                var userDto = JsonSerializer.Deserialize<LoginResponseDto>(userClaim);
                if (userDto != null)
                {
                    context.Items["User"] = userDto;
                    await _next(context);
                    return;
                }
            }
        }

        // If Access Token invalid, try Refresh Token
        if (!string.IsNullOrEmpty(refreshToken) && jwtUtils.ValidateJwtToken(refreshToken, out var jwtRefreshToken))
        {
            var userIdClaim = jwtRefreshToken?.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                // Generate new tokens and set cookies
                var userDto = await authService.GenerateTokens(context, userId);
                if (userDto != null)
                {
                    context.Items["User"] = userDto;
                    await _next(context);
                    return;
                }
            }
        }

        // If both tokens invalid
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }
}