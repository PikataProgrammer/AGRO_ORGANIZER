using System.IdentityModel.Tokens.Jwt;
using AgroOrganizer.Models.Entities.User;

namespace AgroOrganizer.Services.Auth.Interfaces;

public interface IJwtUtils
{
    string GenerateJwtToken(UserEntity user);
    string GenerateJwtRefreshToken(UserEntity user);
    bool ValidateJwtToken(string token, out JwtSecurityToken? jwtToken);
}