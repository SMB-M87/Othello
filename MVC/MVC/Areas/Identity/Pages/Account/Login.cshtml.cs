#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;
using MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(
            ILogger<LoginModel> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
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

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            public string Username { get; set; }

            [Required]
            [StringLength(65)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                bool breached = PasswordBreach.IsPasswordBreached(Input.Password);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Input.Username);
                    if (user != null)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        var name = await _userManager.FindByNameAsync(Input.Username);
                        await _signInManager.SignInAsync(name, Input.RememberMe);
                        await LogIt(new(Input.Username, "Identity/Login", $"Player {user.UserName} logged in."));

                        if (breached)
                        {
                            TempData["StatusMessage"] = "Error: The password you entered has been found in a data breach. Please change your password immediately.";
                            return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
                        }

                        return RedirectToPage("/Home/Index");
                    }
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { Input.RememberMe, breached });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    await LogIt(new(Input.Username, "FAIL:Identity/Login", $"Player {Input.Username} failed to log in."));
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            return Page();
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
