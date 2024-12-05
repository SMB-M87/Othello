#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Areas.Identity.Pages.Account.Manage;
using MVC.Data;
using MVC.Models;
using System.ComponentModel.DataAnnotations;
namespace MVC.Areas.Identity.Pages.Account
{
    public class LoginWithRecoveryCodeModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginWithRecoveryCodeModel(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginWithRecoveryCodeModel> logger,
            SignInManager<ApplicationUser> signInManager
            )
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
            [BindProperty]
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
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
            var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);
            var isRecoveryCodeValid = await VerifyAndConsumeRecoveryCodeAsync(user, recoveryCode);

            if (isRecoveryCodeValid)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                await LogIt(new(user.UserName, "Identity/LoginWithRecoveryCode", $"Player {user.UserName} logged in with a valid recovery code."));

                if (Breached)
                {
                    TempData["StatusMessage"] = "Error: The password you entered has been found in a data breach. Please change your password immediately.";
                    return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
                }

                return RedirectToPage("/Home/Index");
            }
            else
            {
                await LogIt(new(user.UserName, "FAIL:Identity/LoginWithRecoveryCode", $"Player {user.UserName} entered an invalid recovery code."));
                _logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return Page();
            }
        }

        private async Task<bool> VerifyAndConsumeRecoveryCodeAsync(ApplicationUser user, string code)
        {
            var hashedCodesJson = await _userManager.GetAuthenticationTokenAsync(user, "[AspNetUserStore]", "RecoveryCodes");
            if (string.IsNullOrEmpty(hashedCodesJson))
            {
                return false;
            }

            var hashedCodes = System.Text.Json.JsonSerializer.Deserialize<List<string>>(hashedCodesJson);
            bool codeMatched = false;
            string matchedHashedCode = null;

            foreach (var storedCode in hashedCodes)
            {
                var parts = storedCode.Split('.');
                if (parts.Length != 2)
                {
                    continue;
                }

                var salt = Convert.FromBase64String(parts[0]);
                var storedHash = Convert.FromBase64String(parts[1]);
                var hash = Encryption.Hash(code, salt);

                if (hash.SequenceEqual(storedHash))
                {
                    codeMatched = true;
                    matchedHashedCode = storedCode;
                    break;
                }
            }

            if (codeMatched)
            {
                hashedCodes.Remove(matchedHashedCode);
                var updatedHashedCodesJson = System.Text.Json.JsonSerializer.Serialize(hashedCodes);
                await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "RecoveryCodes", updatedHashedCodesJson);
                return true;
            }

            return false;
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
