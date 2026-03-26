using AgroOrganizer.Models.Entities.User;

namespace AgroOrganizer.Models.Dtos.Auth;

public class TokensResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public int AccessTokenExpiryMinutes { get; set; } 
    public int RefreshTokenExpiryMinutes { get; set; }
    public UserEntity UserEntity { get; set; }
    public TokensResponseDto()
    {
        
    }
}