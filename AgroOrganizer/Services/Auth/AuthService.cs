using AgroOrganizer.Models.Dtos.Auth;
using AgroOrganizer.Models.Dtos.LoginDto;
using AgroOrganizer.Services.Auth.Interfaces;

namespace AgroOrganizer.Services.Auth;

public class AuthService : IAuthService
{
    public Task<LoginResponseDto?> Authenticate(HttpContext context, LoginRequestDto model)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto?> GenerateTokens(HttpContext context, uint userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangePassword(ChangePasswordRequestDto changePasswordRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPassword(ForgottenPasswordRequestDto forgottenPasswordRequestDto)
    {
        throw new NotImplementedException();
    }
}