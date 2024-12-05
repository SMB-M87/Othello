#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;
using MVC.Models;
using System.Net;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public class ResetAuthenticatorModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ResetAuthenticatorModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ResetAuthenticatorModel(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<ResetAuthenticatorModel> logger,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
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

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Home/Index");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                return RedirectToPage("/Home/Index");
            }

            await LogIt(new(user.UserName, "Identity/ResetAuthenticator", $"Player {user.UserName} fetched data from the identity user tokens database."));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Home/Index");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                return RedirectToPage("/Home/Index");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "AuthenticatorKey", null);
            await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "RecoveryCodes", null);
            await _userManager.SetAuthenticationTokenAsync(user, "[AspNetUserStore]", "Code", null);
            _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);
            await LogIt(new(user.UserName, "Identity/ResetAuthenticator", $"Player {user.UserName} has reset their 2FA authentication."));

            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your authenticator app key has been reset, you will need to configure your authenticator app using a new key.";

            return RedirectToPage("./EnableAuthenticator");
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
