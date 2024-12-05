#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;
using MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
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

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [NormalizedPassword]
            [StringLength(65, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 16)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [StringLength(65, ErrorMessage = "The {0} is not allowed to exceed {1} characters.")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            Username = userName;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Home/Index");
            }

            await LogIt(new(user.UserName, "Identity/Index", $"Player {user.UserName} fetched data from the identity user database."));
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Home/Index");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                await LogIt(new(user.UserName, "FAIL:Identity/Index/Password", $"Player {user.UserName} failed to change the account's password."));
                return Page();
            }

            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your password has been changed.";
            await LogIt(new(user.UserName, "Identity/Index/Password", $"Player {user.UserName} changed the account's password."));

            return RedirectToPage();
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
