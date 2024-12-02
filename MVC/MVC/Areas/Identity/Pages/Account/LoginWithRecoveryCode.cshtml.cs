#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Areas.Identity.Pages.Account.Manage;
using MVC.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
namespace MVC.Areas.Identity.Pages.Account
{
    public class LoginWithRecoveryCodeModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> _logger;

        public LoginWithRecoveryCodeModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginWithRecoveryCodeModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [BindProperty]
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            _ = await _signInManager.GetTwoFactorAuthenticationUserAsync() ?? throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            ReturnUrl = returnUrl;

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
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                return RedirectToPage("/Home/Index");
            }
            else
            {
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
                var hash = RecoveryEncryption.Hash(code, salt);

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
    }
}
