#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Data;
using MVC.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MVC.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(
            ILogger<LoginModel> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            var apiKey = configuration["ApiSettings:KEY"];
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                PlayerLog log = new(user.UserName, "Identity/Logout", $"Player {user.UserName} logged out.");
                await _httpClient.PostAsJsonAsync($"api/log", log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (user != null)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToPage("/Home/Index");
        }
    }
}
