using System;
using System.Linq;
using System.Security.Cryptography;

namespace Bitdiff.Utils
{
    public class PasswordHash
    {
        public string Hash(string password)
        {
            var salt = new byte[8];
            var cryptoServiceProvider = new RNGCryptoServiceProvider();
            cryptoServiceProvider.GetBytes(salt);

            return HashSalted(salt, password);
        }

        public bool Valid(string password, string hash)
        {
            return hash == HashSalted(Convert.FromBase64String(hash.Split('$')[0]), password);
        }

        private static string HashSalted(byte[] salt, string password)
        {
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password).ToList();
            var md5 = MD5.Create();
            passwordBytes.AddRange(salt);

            var passwordHash = passwordBytes.ToArray();
            for (var i = 0; i < 1000; i++)
                passwordHash = md5.ComputeHash(passwordHash);

            return Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(passwordHash);
        }
    }
}