using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public AdminController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            var baseUrl = configuration["ApiSettings:BaseUrl"] ?? throw new Exception("BaseUrl setting is missing in configuration.");

            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            var cookies = _httpContextAccessor?.HttpContext?.Request.Cookies;

            if (cookies is not null)
            {
                foreach (var cookie in cookies)
                {
                    if (cookie.Key == ".AspNet.SharedAuthCookie")
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

        public async Task<IActionResult> Players(string searchQuery = "")
        {
            var model = await GetPlayers();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                model = model.Where(p => p.Username.Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
                                      || p.Token.Equals(searchQuery, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }
            model = model?.OrderByDescending(p => p.LastActivity).ToList();
            return View(model);
        }

        public async Task<IActionResult> Profile(string token)
        {
            var model = await GetPlayer(token);

            if (model is not null)
            {
                return View("Profile", model);
            }

            return RedirectToAction("Players");
        }

        public async Task<IActionResult> Games(string searchQuery = "")
        {
            var model = await GetGames();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                model = model.Where(g => g.First.Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
                                      || (g.Second is not null && g.Second.Equals(searchQuery, StringComparison.OrdinalIgnoreCase))
                                      || (g.Rematch is not null && g.Rematch.Equals(searchQuery, StringComparison.OrdinalIgnoreCase)))
                             .ToList();
            }
            model = model?.OrderByDescending(g => g.Date).ToList();
            return View(model);
        }

        public async Task<IActionResult> GameView(string token)
        {
            var model = await GetGame(token);

            if (model is not null)
            {
                return View("GameView", model);
            }

            return RedirectToAction("Games");
        }

        public async Task<IActionResult> Results(string searchQuery = "")
        {
            var model = await GetResults();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                model = model.Where(r => r.Winner.Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
                                      || r.Loser.Equals(searchQuery, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }
            model = model?.OrderByDescending(r => r.Date).ToList();
            return View(model);
        }

        public async Task<IActionResult> ResultView(string token)
        {
            var model = await GetResult(token);

            if (model is not null)
            {
                return View("ResultView", model);
            }

            return RedirectToAction("Results");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> PlayerEdit([FromBody] Text text)
        {
            var response = await _httpClient.PostAsJsonAsync("api/player/activity", new { Token = text.Body });

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to update last activity player." });
            }
            return Json(new { success = true, message = "Player last activity updated." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> PlayerDelete([FromBody] Text text)
        {
            var user = await _userManager.FindByIdAsync(text.Body);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found in Identity database." });
            }
            else
            {
                var response = await _httpClient.PostAsJsonAsync("api/player/delete", new { Token = text.Body });

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new { success = false, message = "Unable to delete player." });
                }

                var identityResult = await _userManager.DeleteAsync(user);
                if (!identityResult.Succeeded)
                {
                    return Json(new { success = false, message = "Failed to delete user from Identity database." });
                }

                await _userManager.UpdateSecurityStampAsync(user);

                return Json(new { success = true, message = "Player and associated Identity user deleted successfully." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GameDelete([FromBody] Text text)
        {
            var response = await _httpClient.PostAsJsonAsync("api/admin/game/delete", new { Token = text.Body });

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to delete game." });
            }
            return Json(new { success = true, message = "Game deleted." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ResultDelete([FromBody] Text text)
        {
            var response = await _httpClient.PostAsJsonAsync("api/admin/result/delete", new { Token = text.Body });

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to delete result." });
            }
            return Json(new { success = true, message = "Result deleted." });
        }

        private async Task<List<Player>> GetPlayers()
        {
            var response = await _httpClient.GetAsync("api/admin/player");
            List<Player> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<Player>>() ?? new();
            }
            return result;
        }

        private async Task<Player?> GetPlayer(string token)
        {
            var response = await _httpClient.GetAsync($"api/admin/player/{token}");
            Player result = new();

            if (!response.IsSuccessStatusCode)
                response = await _httpClient.GetAsync($"api/admin/player/name/{token}");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                };
                var resultList = await response.Content.ReadFromJsonAsync<List<Player>>(options);
                result = resultList?.FirstOrDefault() ?? new();
            }
            return result;
        }

        private async Task<List<Game>> GetGames()
        {
            var response = await _httpClient.GetAsync("api/admin/game");
            List<Game> result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                result = await response.Content.ReadFromJsonAsync<List<Game>>(options) ?? new();
            }
            return result;
        }

        private async Task<Game> GetGame(string token)
        {
            var response = await _httpClient.GetAsync($"api/admin/game/{token}");
            Game result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                result = await response.Content.ReadFromJsonAsync<Game>(options) ?? new();
            }
            return result;
        }

        private async Task<List<GameResult>> GetResults()
        {
            var response = await _httpClient.GetAsync("api/admin/result");
            List<GameResult> result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                result = await response.Content.ReadFromJsonAsync<List<GameResult>>(options) ?? new();
            }
            return result;
        }

        private async Task<GameResult> GetResult(string token)
        {
            var response = await _httpClient.GetAsync($"api/result/{token}");
            GameResult result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                result = await response.Content.ReadFromJsonAsync<GameResult>(options) ?? new();
            }
            return result;
        }
    }
}
