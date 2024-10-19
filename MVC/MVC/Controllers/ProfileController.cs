using MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace MVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(

            IHttpClientFactory httpClientFactory,
            UserManager<IdentityUser> userManager
            )
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7023/");
        }

        public async Task<IActionResult> Index()
        {
            if (User is null || User.Identity is null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var userId = _userManager.GetUserId(User);

            // Fetch player stats
            var statsResponse = await _httpClient.GetAsync($"api/result/stats/{userId}");
            string stats = string.Empty;

            if (statsResponse.IsSuccessStatusCode)
            {
                stats = await statsResponse.Content.ReadAsStringAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch player stats.");
            }

            // Fetch match history
            var historyResponse = await _httpClient.GetAsync($"api/result/{userId}");
            List<GameResult> matchHistory = new();

            if (historyResponse.IsSuccessStatusCode)
            {
                matchHistory = await historyResponse.Content.ReadFromJsonAsync<List<GameResult>>() ?? new();
            }

            var model = new ProfileView
            {
                Stats = stats,
                MatchHistory = matchHistory
            };

            return View(model);
        }
    }
}
