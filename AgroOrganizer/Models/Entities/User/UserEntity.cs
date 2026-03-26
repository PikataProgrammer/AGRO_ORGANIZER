using AgroOrganizer.Models.Dtos.UserDto;

namespace AgroOrganizer.Models.Entities.User;

public class UserEntity
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PaswwordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public bool ShouldChangePassword { get; private set; }

    public UserEntity()
    {
        
    }

    public UserEntity(int userId, string firstName, string lastName, string email, string paswwordHash, string passwordSalt)
    {
        Id = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PaswwordHash = paswwordHash;
        PasswordSalt = passwordSalt;
        ShouldChangePassword = true;
    }

    public void Update(UpdateUserRequestDto dto)
    {
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Email = dto.Email;
    }
    
}