using System;
using System.Security.Cryptography;
using System.Text;

class PasswordHasher
{
    static void Main(string[] args)
    {
        Console.WriteLine("Password Hash Generator (SHA256)");
        Console.WriteLine("=================================\n");
        
        string[] testPasswords = { "Admin@123", "User@123", "test123" };
        
        foreach (var password in testPasswords)
        {
            string hash = HashPassword(password);
            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Hash: {hash}");
            Console.WriteLine();
        }
        
        Console.WriteLine("\nUpdate SeedData.sql with these hashes!");
    }
    
    static string HashPassword(string password)
    {
        using (SHA256 sha = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

