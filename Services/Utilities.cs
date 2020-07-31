using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JWTAuthorizationASPNETCoreDemo.Services
{
    public static class Utilities
    {
        public static string HashedPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }

    public class AppSettings
    {
        public string SecretKey { get; set; }
    }
}