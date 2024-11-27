using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC.Controllers
{
    [Authorize(Roles = Roles.Mod)]
    public class ModController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public ModController(
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
                model = model.Where(r => r.Winner.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                                      || r.Loser.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
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

        public async Task<IActionResult> Logs(string searchQuery = "null")
        {
            var model = await GetLogs(searchQuery);
            return View(model);
        }

        public async Task<IActionResult> Log(string token)
        {
            var model = await GetLog(token);

            if (model is not null && model.Token != "")
            {
                return View("Log", model);
            }

            return RedirectToAction("Players");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SuspendPlayer([FromBody] Text text)
        {
            var user = await _userManager.FindByIdAsync(text.Body);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found in Identity database." });
            }

            var suspend = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddDays(30));
            if (!suspend.Succeeded)
            {
                return Json(new { success = false, message = "Failed to suspend user." });
            }

            await _userManager.UpdateSecurityStampAsync(user);

            return Json(new { success = true, message = "User suspended for a month." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UnsuspendPlayer([FromBody] Text text)
        {
            var user = await _userManager.FindByIdAsync(text.Body);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found in Identity database." });
            }

            var suspend = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            if (!suspend.Succeeded)
            {
                return Json(new { success = false, message = "Failed to unsuspend user." });
            }

            user.AccessFailedCount = 0;
            await _userManager.UpdateAsync(user);

            return Json(new { success = true, message = "User unsuspended immediatly." });
        }

        private async Task<List<Player>> GetPlayers()
        {
            var response = await _httpClient.GetAsync("api/mod/player");
            List<Player> result = new();

            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadFromJsonAsync<List<Player>>() ?? new();

                foreach (var player in results)
                {
                    var identityUser = await _userManager.FindByIdAsync(player.Token);
                    if (identityUser != null)
                    {
                        var roles = await _userManager.GetRolesAsync(identityUser);

                        if (!roles.Contains(Roles.Admin) && !roles.Contains(Roles.Mod))
                            result.Add(player);
                    }
                    else
                    {
                        result.Add(player);
                    }
                }
            }
            return result;
        }

        private async Task<Player?> GetPlayer(string token)
        {
            var identityUser = await _userManager.FindByIdAsync(token);

            if (identityUser != null)
            {
                var roles = await _userManager.GetRolesAsync(identityUser);

                if (roles.Contains(Roles.Admin) || roles.Contains(Roles.Mod))
                    return null;
            }
            else
            {
                identityUser = await _userManager.FindByNameAsync(token);

                if (identityUser != null)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);

                    if (roles.Contains(Roles.Admin) || roles.Contains(Roles.Mod))
                        return null;
                }
            }

            var response = await _httpClient.GetAsync($"api/mod/player/{token}");
            var result = new Player();

            if (!response.IsSuccessStatusCode)
                response = await _httpClient.GetAsync($"api/mod/player/name/{token}");

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
            var response = await _httpClient.GetAsync("api/mod/game");
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
            var response = await _httpClient.GetAsync($"api/mod/game/{token}");
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
            var response = await _httpClient.GetAsync("api/mod/result");
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

        private async Task<List<PlayerLog>> GetLogs(string token)
        {
            var response = await _httpClient.GetAsync($"api/mod/logs/{token}");
            List<PlayerLog> result = new();

            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadFromJsonAsync<List<PlayerLog>>() ?? new();

                foreach (var log in results)
                {
                    var identityUser = await _userManager.FindByNameAsync(log.Username);
                    if (identityUser != null)
                    {
                        var roles = await _userManager.GetRolesAsync(identityUser);

                        if (!roles.Contains(Roles.Admin) && !roles.Contains(Roles.Mod))
                            result.Add(log);
                    }
                }
            }
            return result;
        }

        private async Task<PlayerLog> GetLog(string token)
        {
            var response = await _httpClient.GetAsync($"api/mod/log/{token}");
            PlayerLog result = new();

            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadFromJsonAsync<PlayerLog>() ?? new();

                var identityUser = await _userManager.FindByNameAsync(result.Username);

                if (identityUser != null)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);

                    if (!roles.Contains(Roles.Admin) && !roles.Contains(Roles.Mod))
                        return results;
                }
            }
            return result;
        }
    }
}
