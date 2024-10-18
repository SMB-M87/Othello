#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MVC.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(
            ILogger<LoginModel> logger,
            IHttpClientFactory httpClientFactory,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User); // Get the currently logged-in user
            if (user != null)
            {
                // Call the API to update the player's online status using User ID
                var playerStatusUrl = "https://localhost:7023/api/player/logout";
                var response = await _httpClient.PostAsJsonAsync(playerStatusUrl, user.Id); // Send the User ID
                if (!response.IsSuccessStatusCode)
                {
                    // Handle potential API failure here
                    _logger.LogError("Failed to update player status in API.");
                }
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
