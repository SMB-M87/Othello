using System.Security.Cryptography;
using System.Text;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public static class Encryption
    {
        public static string GenerateHashedCode(int length)
        {
            var plainCode = GenerateSecureCode(length);

            return plainCode;
        }

        public static (IEnumerable<string> PlainCodes, IEnumerable<string> HashedCodes) GenerateHashedCodes(int count, int length)
        {
            var plainCodes = new List<string>();
            var hashedCodes = new List<string>();

            for (var i = 0; i < count; i++)
            {
                var plainCode = GenerateSecureCode(length);
                var salt = GenerateSalt();
                var hashedCode = HashCodeWithSalt(plainCode, salt);
                var storedCode = $"{Convert.ToBase64String(salt)}.{hashedCode}";

                plainCodes.Add(plainCode);
                hashedCodes.Add(storedCode);
            }

            return (plainCodes, hashedCodes);
        }

        public static byte[] Hash(string code, byte[] salt)
        {
            using var hmac = new HMACSHA256(salt);
            var codeBytes = Encoding.UTF8.GetBytes(code);
            return hmac.ComputeHash(codeBytes);
        }

        private static string GenerateSecureCode(int length)
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            return Base32Encode(bytes);
        }

        private static string Base32Encode(byte[] data)
        {
            const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            var result = new StringBuilder((data.Length * 8 + 4) / 5);

            int buffer = data[0];
            int next = 1;
            int bitsLeft = 8;

            while (bitsLeft > 0 || next < data.Length)
            {
                if (bitsLeft < 5)
                {
                    if (next < data.Length)
                    {
                        buffer <<= 8;
                        buffer |= data[next++] & 0xff;
                        bitsLeft += 8;
                    }
                    else
                    {
                        int pad = 5 - bitsLeft;
                        buffer <<= pad;
                        bitsLeft += pad;
                    }
                }
                int index = (buffer >> (bitsLeft - 5)) & 0x1f;
                bitsLeft -= 5;
                result.Append(base32Chars[index]);
            }

            return result.ToString();
        }

        private static byte[] GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }

        private static string HashCodeWithSalt(string code, byte[] salt)
        {
            using var hmac = new HMACSHA256(salt);
            var codeBytes = Encoding.UTF8.GetBytes(code);
            var hash = hmac.ComputeHash(codeBytes);
            return Convert.ToBase64String(hash);
        }
    }
}
