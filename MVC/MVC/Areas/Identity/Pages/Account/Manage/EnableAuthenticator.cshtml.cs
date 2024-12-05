#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;
using MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public class EnableAuthenticatorModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly UrlEncoder _urlEncoder;
        private readonly ILogger<EnableAuthenticatorModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public EnableAuthenticatorModel(
            UrlEncoder urlEncoder,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            ILogger<EnableAuthenticatorModel> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _logger = logger;
            _urlEncoder = urlEncoder;
            _userManager = userManager;
            _signInManager = signInManager;

            var baseUrl = configuration["ApiSettings:BaseUrl"] ?? throw new Exception("BaseUrl setting is missing in configuration.");

            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            var cookies = httpContextAccessor?.HttpContext?.Request.Cookies;

            if (cookies is not null)
            {
                foreach (var cookie in cookies)
                {
                    if (cookie.Key == "__Host-SharedAuthCookie")
                    {
                        handler.CookieContainer.Add(
                            new Uri(baseUrl),
                            new Cookie(cookie.Key, cookie.Value)
                        );
                    }
                }

                _httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseUrl)
                };
            }
            else
            {
                _httpClient = httpClientFactory.CreateClient("ApiClient");
            }
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

            await LogIt(new(user.UserName, "Identity/EnableAuthenticator", $"Player {user.UserName} fetched enable authenticator data from the identity user tokens and users database."));
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

            if (!await VerifyAuthenticatorCode(user, plainKey, verificationCode))
            {
                ModelState.AddModelError("Input.Code", "Verification code is invalid.");
                await LogIt(new(user.UserName, "FAIL:Identity/EnableAuthenticator", $"Player {user.UserName} verification code is invalid."));
                return Page();
            }
            else
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                var userId = await _userManager.GetUserIdAsync(user);
                _logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);
                await LogIt(new(user.UserName, "Identity/EnableAuthenticator", $"Player {user.UserName} has enabled 2FA with an authenticator app."));

                StatusMessage = "Your authenticator app has been verified.";

                var (plainCodes, hashedCodes) = Encryption.GenerateHashedCodes(10, 23);
                RecoveryCodes = plainCodes.ToArray();
                var hashedCodesJson = System.Text.Json.JsonSerializer.Serialize(hashedCodes);
                await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "RecoveryCodes", hashedCodesJson);

                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("./ShowRecoveryCodes");
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
            await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "RecoveryCodes", null);
            await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "Code", null);
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
                await LogIt(new(user.UserName, "TEMPERING:Identity/EnableAuthenticator", $"Someone tryed to validate player's {user.UserName} 2FA using an used code!!!"));
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
                await _httpClient.PostAsJsonAsync($"api/player/log", log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
