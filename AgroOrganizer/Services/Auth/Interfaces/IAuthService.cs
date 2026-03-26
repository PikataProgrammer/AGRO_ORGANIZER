using AgroOrganizer.Models.Dtos.Auth;
using AgroOrganizer.Models.Dtos.LoginDto;

namespace AgroOrganizer.Services.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> Authenticate(HttpContext context, LoginRequestDto model);
    Task<LoginResponseDto?> GenerateTokens(HttpContext context, uint userId);
    Task<bool> ChangePassword(ChangePasswordRequestDto changePasswordRequestDto);
    Task<bool> ResetPassword(ForgottenPasswordRequestDto forgottenPasswordRequestDto);
}