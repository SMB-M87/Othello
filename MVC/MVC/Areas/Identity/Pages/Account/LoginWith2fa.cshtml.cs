#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Areas.Identity.Pages.Account.Manage;
using MVC.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace MVC.Areas.Identity.Pages.Account
{
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginWith2faModel> _logger;

        public LoginWith2faModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginWith2faModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; }

            [Display(Name = "Remember this machine")]
            public bool RememberMachine { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            _ = await _signInManager.GetTwoFactorAuthenticationUserAsync() ?? throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool rememberMe)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync() ?? throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var encryptedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(encryptedKey))
            {
                _logger.LogWarning("Authenticator key is missing for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
            
            var plainKey = SymmetricEncryption.Decrypt(encryptedKey);

            if (await VerifyAuthenticatorCode(user, plainKey, authenticatorCode))
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignInAsync(user, isPersistent: rememberMe);
                return RedirectToPage("/Home/Index");
            }

            _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
            await _userManager.AccessFailedAsync(user);
            ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
            return Page();
        }

        private async Task<bool> VerifyAuthenticatorCode(ApplicationUser user, string key, string providedCode)
        {
            var decodedKey = Base32Decode(key);
            long timeStep = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 30;

            var expectedCode = GenerateTotp(decodedKey, timeStep);
            var storedCode = await _userManager.GetAuthenticationTokenAsync(user, "[AspNetUserStore]", "Code");
            bool pass;

            if (string.IsNullOrEmpty(storedCode))
                pass = true;
            else
            {
                var parts = storedCode.Split('.');
                var salt = Convert.FromBase64String(parts[0]);
                var storedHash = Convert.FromBase64String(parts[1]);
                var hash = Encryption.Hash(providedCode, salt);
                pass = !hash.SequenceEqual(storedHash);
            }

            if (expectedCode == providedCode && pass)
            {
                var hashedCode = Encryption.GenerateHashedCode(providedCode);
                await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "Code", hashedCode);
                return true;
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
    }
}
