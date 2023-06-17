using Financify_Api.Repositories.Interfaces;
using System.Security.Cryptography;

namespace Financify_Api.Services.Helper
{
    public class SecurityHelper : ITokenService
    {
        public void CreatePasswordHash(string password, out string passwordHash)
        {
            passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public string GenerateRandomToken()
        {
            byte[] tokenBytes = new byte[64];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            return BitConverter.ToString(tokenBytes).Replace("-", "").ToLower();
        }
    }
}
