#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public class EnableAuthenticatorModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EnableAuthenticatorModel> _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public EnableAuthenticatorModel(
            UserManager<ApplicationUser> userManager,
            ILogger<EnableAuthenticatorModel> logger,
            UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }

        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }

        [TempData]
        public string[] RecoveryCodes { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Verification Code")]
            public string Code { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Home/Index");
            }

            await LoadSharedKeyAndQrCodeUriAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Home/Index");
            }
            await LoadSharedKeyAndQrCodeUriAsync(user);

            var verificationCode = Input.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var encryptedKey = await _userManager.GetAuthenticatorKeyAsync(user);

            var plainKey = SymmetricEncryption.Decrypt(encryptedKey);

            if (!VerifyAuthenticatorCode(plainKey, verificationCode))
            {
                ModelState.AddModelError("Input.Code", "Verification code is invalid.");
                return Page();
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var userId = await _userManager.GetUserIdAsync(user);
            _logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);

            StatusMessage = "Your authenticator app has been verified.";

            if (await CountRecoveryCodes(user) <= 9)
            {
                var (plainCodes, hashedCodes) = Encryption.GenerateHashedCodes(10, 20);
                RecoveryCodes = plainCodes.ToArray();
                var hashedCodesJson = System.Text.Json.JsonSerializer.Serialize(hashedCodes);
                await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "RecoveryCodes", hashedCodesJson);
                return RedirectToPage("./ShowRecoveryCodes");
            }
            else
            {
                return RedirectToPage("./TwoFactorAuthentication");
            }
        }

        private async Task LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user)
        {
            var encryptedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            string unformattedKey;

            if (string.IsNullOrEmpty(encryptedKey))
            {
                unformattedKey = await ResetAuthenticatorKeyEncryptedAsync(user);
            }
            else
            {
                unformattedKey = SymmetricEncryption.Decrypt(encryptedKey);
            }

            SharedKey = FormatKey(unformattedKey);
            var identifier = await _userManager.GetUserNameAsync(user);
            AuthenticatorUri = GenerateQrCodeUri(identifier, unformattedKey);
        }

        private async Task<string> ResetAuthenticatorKeyEncryptedAsync(ApplicationUser user)
        {
            var plainCode = Encryption.GenerateHashedCode(30);
            var encryptedKey = SymmetricEncryption.Encrypt(plainCode);
            await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "AuthenticatorKey", encryptedKey);
            return plainCode;
        }

        private static string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition, 4)).Append(' ');
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string identifier, string unformattedKey)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                AuthenticatorUriFormat,
                _urlEncoder.Encode("Othello"),
                _urlEncoder.Encode(identifier),
                unformattedKey);
        }

        private static bool VerifyAuthenticatorCode(string key, string providedCode)
        {
            var decodedKey = Base32Decode(key);
            long timeStep = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 30;

            for (int offset = -1; offset <= 1; offset++)
            {
                var expectedCode = GenerateTotp(decodedKey, timeStep + offset);
                if (expectedCode == providedCode)
                {
                    return true;
                }
            }

            return false;
        }

        private static string GenerateTotp(byte[] key, long timeStep)
        {
            using var hmac = new HMACSHA1(key);
            var timeBytes = BitConverter.GetBytes(timeStep);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timeBytes);
            }

            var hash = hmac.ComputeHash(timeBytes);
            int offset = hash[^1] & 0x0F;
            int binaryCode = (hash[offset] & 0x7F) << 24 |
                             (hash[offset + 1] & 0xFF) << 16 |
                             (hash[offset + 2] & 0xFF) << 8 |
                             (hash[offset + 3] & 0xFF);

            return (binaryCode % 1_000_000).ToString("D6");
        }

        private static byte[] Base32Decode(string base32)
        {
            if (string.IsNullOrWhiteSpace(base32))
            {
                throw new ArgumentException("Base32 string is null or empty.", nameof(base32));
            }

            const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            var output = new List<byte>();
            int buffer = 0, bitsLeft = 0;

            foreach (char c in base32.ToUpperInvariant().Where(base32Chars.Contains))
            {
                buffer <<= 5;
                buffer |= base32Chars.IndexOf(c) & 31;
                bitsLeft += 5;

                if (bitsLeft >= 8)
                {
                    output.Add((byte)(buffer >> (bitsLeft - 8)));
                    bitsLeft -= 8;
                }
            }

            if (bitsLeft > 0)
            {
                output.Add((byte)(buffer << (8 - bitsLeft)));
            }

            return output.ToArray();
        }

        private async Task<int> CountRecoveryCodes(ApplicationUser user)
        {
            var hashedCodesJson = await _userManager.GetAuthenticationTokenAsync(user, "[AspNetUserStore]", "RecoveryCodes");
            if (string.IsNullOrEmpty(hashedCodesJson))
            {
                return 0;
            }

            var hashedCodes = System.Text.Json.JsonSerializer.Deserialize<List<string>>(hashedCodesJson);
            return hashedCodes.Count;
        }
    }
}
