using System.Security.Cryptography;
using System.Text;

namespace MVC.Areas.Identity.Pages.Account
{
    public static class PasswordBreach
    {
        private static HashSet<string>? BreachedHashes;

        public static void Initialize(string contentRootPath)
        {
            var filePath = Path.Combine(contentRootPath, "pwnedpasswords.txt");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Breached passwords file not found at: {filePath}");
            }

            BreachedHashes = new HashSet<string>(
                File.ReadLines(filePath).Select(line => line.Split(':')[0])
            );
        }

        public static bool IsPasswordBreached(string password)
        {
            if (BreachedHashes == null)
            {
                throw new InvalidOperationException("PasswordBreach has not been initialized. Call Initialize() first.");
            }

            using var sha1 = SHA1.Create();
            var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();

            return BreachedHashes.Contains(hash);
        }
    }
}
