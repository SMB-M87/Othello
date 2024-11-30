// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;
using System.Security.Cryptography;
using System.Text;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public class GenerateRecoveryCodesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<GenerateRecoveryCodesModel> _logger;

        public GenerateRecoveryCodesModel(
            UserManager<ApplicationUser> userManager,
            ILogger<GenerateRecoveryCodesModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string[] RecoveryCodes { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for user because they do not have 2FA enabled.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for user as they do not have 2FA enabled.");
            }

            var (PlainCodes, HashedCodesJson) = GenerateHashedRecoveryCodes(10);
            RecoveryCodes = PlainCodes.ToArray();

            await _userManager.SetAuthenticationTokenAsync(user, "Default", "RecoveryCodes", HashedCodesJson);

            _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);
            StatusMessage = "You have generated new recovery codes.";
            return RedirectToPage("./ShowRecoveryCodes");
        }

        private static (IEnumerable<string> PlainCodes, string HashedCodesJson) GenerateHashedRecoveryCodes(int count)
        {
            var plainCodes = new List<string>();
            var hashedCodes = new Dictionary<string, string>();

            for (var i = 0; i < count; i++)
            {
                var plainCode = GenerateSecureCode(20);
                var salt = GenerateSalt();
                var hashedCode = HashCodeWithSalt(plainCode, salt);

                plainCodes.Add(plainCode);
                hashedCodes[hashedCode] = Convert.ToBase64String(salt);
            }

            return (plainCodes, System.Text.Json.JsonSerializer.Serialize(hashedCodes));
        }

        private static string GenerateSecureCode(int length)
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes)[..length].Replace("+", "").Replace("/", "").Replace("=", "");
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
