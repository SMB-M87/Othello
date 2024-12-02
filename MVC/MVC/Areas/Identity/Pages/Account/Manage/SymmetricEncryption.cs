using System.Security.Cryptography;
using System.Text;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public static class SymmetricEncryption
    {
        private static readonly Lazy<string> EncryptionKey = new(() =>
            Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ??
            throw new InvalidOperationException("Encryption key is not set in the environment variables.")
        );

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(EncryptionKey.Value);
            aes.Key = key.Take(32).ToArray();
            aes.IV = key.Take(16).ToArray();

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(EncryptionKey.Value);
            aes.Key = key.Take(32).ToArray();
            aes.IV = key.Take(16).ToArray();

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            {
                return sr.ReadToEnd();
            }
        }
    }
}
