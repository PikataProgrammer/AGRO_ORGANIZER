using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AgroOrganizer.Models.Dtos.LoginDto;
using AgroOrganizer.Models.Entities.User;
using AgroOrganizer.Services.Auth.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AgroOrganizer.Services.Auth;

public class JwtUtils : IJwtUtils
{
    private readonly IConfiguration _configuration;
    public JwtUtils(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateJwtToken(UserEntity user)
    {
        //generate token that is valid for 10 minutes
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["AuthJWT:PrivateKey"]);
        var expiryMinutes = int.Parse(_configuration["AuthJWT:ExpiryMinutes"] ?? "10");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                { new Claim("user", JsonSerializer.Serialize(new LoginResponseDto(user))) }),
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateJwtRefreshToken(UserEntity user)
    {
        //generate token that is valid for 20 minutes
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["AuthJWT:PrivateKey"]);
        var expiryMinutes = int.Parse(_configuration["AuthJWT:RefreshExpiryMinutes"] ?? "20");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateJwtToken(string token, out JwtSecurityToken? jwtToken)
    {
        try
        {
            if (token == null)
            {
                jwtToken = null;
                return false;
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var privateKey = _configuration["AuthJWT:PrivateKey"];
            if (string.IsNullOrEmpty(privateKey))
            {
                jwtToken = null;
                return false;
            }
            
            var refreshExpiryMinutes = int.Parse(_configuration["AuthJWT:RefreshExpiryMinutes"] ?? "20");
            var key = Encoding.UTF8.GetBytes(privateKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            
            jwtToken = (JwtSecurityToken)validatedToken;
            
            //if the token was issued before more than 'refreshExpiryMinutes' minutes than it should be considered as invalid
            if ((jwtToken.ValidTo.Ticks - jwtToken.ValidFrom.Ticks) / TimeSpan.TicksPerMillisecond >
                refreshExpiryMinutes * 60 * 1000)
            {
                jwtToken = null;
                return false;
            }
            
            return true;
        }
        catch (Exception e)
        {
            jwtToken = null;
            return false;
        }
    }
}