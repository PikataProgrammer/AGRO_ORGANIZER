using AgroOrganizer.Models.Entities.User;

namespace AgroOrganizer.Models.Dtos.UserDto;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public UserDto(UserEntity entity)
    {
        Id = entity.Id;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        Email = entity.Email;
    }
    
}