using System;
using System.Security.Cryptography;
using AccountApp.Infrastructure.Extensions;

namespace AccountApp.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int _deriveBytesIterationCount = 10_000;
        private static readonly int _saltSize = 40;

        public string CreateSalt(string value)
        {
            if (value.Empty())
            {
                throw new ArgumentException("Cannot generate salt from an empty string.");
            }

            var saltBytes = new byte[_saltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string CreateHash(string value, string salt)
        {
            if (value.Empty())
            {
                throw new ArgumentException("Empty string cannot be the base of hash.");
            }

            if (salt.Empty())
            {
                throw new ArgumentException("Salt cannot be an empty string.");
            }

            var key = new Rfc2898DeriveBytes(value, GetBytes(salt), _deriveBytesIterationCount);

            return Convert.ToBase64String(key.GetBytes(_saltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}