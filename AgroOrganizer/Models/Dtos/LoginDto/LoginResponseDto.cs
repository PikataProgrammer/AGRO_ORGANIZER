using AgroOrganizer.Models.Entities.User;

namespace AgroOrganizer.Models.Dtos.LoginDto;

public class LoginResponseDto
{
    public int UserId { get; set; }
    public bool ShouldChangePassword { get; set; }
    public string? Names { get; set; }

    public LoginResponseDto()
    {
    }

    public LoginResponseDto(UserEntity entity)
    {
        UserId = entity.Id;
        ShouldChangePassword = entity.ShouldChangePassword;
        Names = entity.FirstName + " " + entity.LastName;
    }
}