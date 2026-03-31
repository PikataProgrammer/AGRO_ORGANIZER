using System.Security.Cryptography;
using System.Text;
using AgroOrganizer.Models.PasswordHasher.Interface;
using Microsoft.AspNetCore.Identity;

namespace AgroOrganizer.Models.PasswordHasher;

public class PasswordHasher : IPasswordHasher
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
    public string CreatePassword(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }

        return res.ToString();
    }
}