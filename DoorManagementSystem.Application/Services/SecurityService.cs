using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace DoorManagementSystem.Application.Services
{
    public class SecurityService : ISecurityService
    {
        public string HashPin(string pin)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pin,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        public bool VerifyPin(string providedPin, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            string hashedProvidedPin = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: providedPin,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedProvidedPin == hash;
        }
    }
}
