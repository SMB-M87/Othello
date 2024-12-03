using System.Security.Cryptography;
using System.Text;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public static class SymmetricEncryption
    {
        private static string GetEncryptionKey()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration["ENCRYPTION_KEY"] ??
                   throw new InvalidOperationException("Encryption key is not set in the configuration.");
        }

        public static string Encrypt(string plainText)
        {
            string encryptionKey = GetEncryptionKey();

            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(encryptionKey);
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
            string encryptionKey = GetEncryptionKey();

            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(encryptionKey);
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
