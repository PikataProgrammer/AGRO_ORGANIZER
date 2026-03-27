using AgroOrganizer.Models.Dtos.UserDto;
using AgroOrganizer.Models.Entities.Field;

namespace AgroOrganizer.Models.Entities.User;

public class UserEntity
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public bool ShouldChangePassword { get; private set; }

    public ICollection<FieldEntity> Fields { get; private set; }

    public UserEntity()
    {
        Fields = new List<FieldEntity>();
    }

    public UserEntity(int id, string firstName, string lastName, string email, string passwordHash, string passwordSalt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        ShouldChangePassword = true;
        Fields = new List<FieldEntity>();
    }

    public void Update(UpdateUserRequestDto dto)
    {
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Email = dto.Email;
    }
}