namespace AgroOrganizer.Models.Entities.User;

public class UserEntity
{
    public int UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PaswwordHash { get; private set; }
    public string PasswordSalt { get; private set; }

    public UserEntity()
    {
        
    }

    public UserEntity(int userId, string firstName, string lastName, string email, string paswwordHash, string passwordSalt)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PaswwordHash = paswwordHash;
        PasswordSalt = passwordSalt;
        
    }

    public void Update(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email; 
    }
    
}