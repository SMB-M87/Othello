#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Areas.Identity.Pages.Account.Manage;
using MVC.Data;
using MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace MVC.Areas.Identity.Pages.Account
{
    public class LoginWith2faModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginWith2faModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginWith2faModel(
            IConfiguration configuration,
            ILogger<LoginWith2faModel> logger,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            var apiKey = configuration["ApiSettings:KEY"];
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public bool Breached { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool breached)
        {
            _ = await _signInManager.GetTwoFactorAuthenticationUserAsync() ?? throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            Breached = breached;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
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
                await LogIt(new(user.UserName, "FAIL:Identity/LoginWith2fa", $"Authenticator key is missing for player {user.UserName}."));
                _logger.LogWarning("Authenticator key is missing for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }

            var plainKey = SymmetricEncryption.Decrypt(encryptedKey);

            if (await VerifyAuthenticatorCode(user, plainKey, authenticatorCode))
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignInAsync(user, isPersistent: false);
                await LogIt(new(user.UserName, "Identity/LoginWith2fa", $"Player {user.UserName} logged in with 2FA."));

                if (Breached)
                {
                    TempData["StatusMessage"] = "Error: The password you entered has been found in a data breach. Please change your password immediately.";
                    return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
                }

                return RedirectToPage("/Home/Index");
            }

            await LogIt(new(user.UserName, "FAIL:Identity/LoginWith2fa", $"Player {user.UserName} entered invalid 2FA code."));
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

            if (!pass)
            {
                await LogIt(new(user.UserName, "TEMPERING:Identity/LoginWith2fa", $"Someone tryed to loging on player's {user.UserName} account using an used 2FA code!!!"));
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

        private async Task LogIt(PlayerLog log)
        {
            try
            {
                await _httpClient.PostAsJsonAsync($"api/log", log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
