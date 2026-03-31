namespace AgroOrganizer.Models.PasswordHasher.Interface;

public interface IPasswordHasher
{
    (string hash, string salt) Hash(string password);
    bool Verify(string password, string storedHash, string storedSalt);
    string CreatePassword(int length);
}