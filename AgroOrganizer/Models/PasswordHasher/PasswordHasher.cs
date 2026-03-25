using System.Security.Cryptography;

namespace AgroOrganizer.Models.PasswordHasher;

public class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;

    public (string hash, string salt) Hash(string password)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

        var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256);
        
        byte[] hashBytes = pbkdf2.GetBytes(HashSize);
        
        return (
            Convert.ToBase64String(hashBytes), 
            Convert.ToBase64String(saltBytes));
    }

    public bool Verify(string password, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);
        
        var pbkdf2 = new Rfc2898DeriveBytes(password,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256);
        
        byte[] hashBytes = pbkdf2.GetBytes(HashSize);
        
        string computedHash = Convert.ToBase64String(hashBytes);
        return computedHash == storedHash;
    }
}