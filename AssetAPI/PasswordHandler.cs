using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AssetAPI
{
    public class PasswordHandler
    {
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public static string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            _rng.GetBytes(salt);

            byte[] subkey = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            byte[] outputBytes = new byte[1 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01;
            Buffer.BlockCopy(salt, 0, outputBytes, 1, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + salt.Length, subkey.Length);

            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                byte[] decoded = Convert.FromBase64String(hashedPassword);
                if (decoded[0] != 0x01) return false;

                byte[] salt = new byte[128 / 8];
                Buffer.BlockCopy(decoded, 1, salt, 0, salt.Length);

                byte[] expectedSubkey = new byte[256 / 8];
                Buffer.BlockCopy(decoded, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                byte[] actualSubkey = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8);

                return actualSubkey.SequenceEqual(expectedSubkey);
            }
            catch
            {
                return false;
            }
        }

    }
}
