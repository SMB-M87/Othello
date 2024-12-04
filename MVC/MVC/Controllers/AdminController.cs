using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public AdminController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
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

            if (model is not null && model.Token != "")
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

            if (model is not null && model.Board is not null && model.Board.GetLength(0) > 0)
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

            if (model is not null && model.Token != "")
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
            try
            {
                var user = await _userManager.FindByIdAsync(text.Body);
                if (user == null)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "FAIL:Admin/SuspendPlayer", $"Tryed to suspend player {text.Body} but couldn't find the player."));
                    return Json(new { success = false, message = "User not found in Identity database." });
                }

                var suspend = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddDays(30));
                if (!suspend.Succeeded)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "FAIL:Admin/SuspendPlayer", $"Tryed to suspend player {text.Body} to no avail."));
                    return Json(new { success = false, message = "Failed to suspend user." });
                }

                await _userManager.UpdateSecurityStampAsync(user);

                await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/SuspendPlayer", $"Suspended player {text.Body} for a month."));
                return Json(new { success = true, message = "User suspended for a month." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Failed to suspend user." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UnsuspendPlayer([FromBody] Text text)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(text.Body);
                if (user == null)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "FAIL:Admin/UnsuspendPlayer", $"Tryed to unsuspend player {text.Body} but couldn't find the player."));
                    return Json(new { success = false, message = "User not found in Identity database." });
                }

                var suspend = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                if (!suspend.Succeeded)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "FAIL:Admin/UnsuspendPlayer", $"Tryed to unsuspend player {text.Body} to no avail."));
                    return Json(new { success = false, message = "Failed to unsuspend user." });
                }

                user.AccessFailedCount = 0;
                await _userManager.UpdateAsync(user);

                await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/UnsuspendPlayer", $"Unsuspended player {text.Body} immediatly."));
                return Json(new { success = true, message = "User unsuspended immediatly." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Failed to unsuspend user." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ElevatePlayer([FromBody] Text text)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(text.Body);
                if (user == null)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/ElevatePlayer", $"Tryed to elevated player {text.Body} to moderator, but couldn't find the player."));
                    return Json(new { success = false, message = "User not found in Identity database." });
                }

                var isModerator = await _userManager.IsInRoleAsync(user, Roles.Mod);
                if (isModerator)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/ElevatePlayer", $"Tryed to elevated player {text.Body} to moderator, but the player is already moderator."));
                    return Json(new { success = false, message = "User is already a moderator." });
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, Roles.Mod);
                if (!addRoleResult.Succeeded)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/ElevatePlayer", $"Tryed to elevated player {text.Body} to moderator, to no avail."));
                    return Json(new { success = false, message = "Failed to elevate user to moderator role." });
                }

                await _userManager.UpdateSecurityStampAsync(user);

                await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/ElevatePlayer", $"Elevated player {text.Body} to moderator."));
                return Json(new { success = true, message = "User elevated to moderator role." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Failed to elevate user to moderator role." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DelevatePlayer([FromBody] Text text)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(text.Body);
                if (user == null)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/DelevatePlayer", $"Tryed to downgrade player {text.Body} to simple user, but couldn't find the player."));
                    return Json(new { success = false, message = "User not found in Identity database." });
                }

                var isModerator = await _userManager.IsInRoleAsync(user, Roles.Mod);
                if (!isModerator)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/DelevatePlayer", $"Tryed to downgrade player {text.Body} to simple user, but the player is not moderator."));
                    return Json(new { success = false, message = "User is not a moderator." });
                }

                var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, Roles.Mod);
                if (!removeRoleResult.Succeeded)
                {
                    await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/DelevatePlayer", $"Tryed to downgrade player {text.Body} to simple user, to no avail."));
                    return Json(new { success = false, message = "Failed to remove moderator role from user." });
                }

                await _userManager.UpdateSecurityStampAsync(user);

                await LogIt(new(User?.Identity?.Name ?? "Anonymous", "Admin/DelevatePlayer", $"Downgraded player {text.Body} to simple user."));
                return Json(new { success = true, message = "Moderator role removed from user." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Failed to remove moderator role from user." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> PlayerDelete([FromBody] Text text)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(text.Body);
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found in Identity database." });
                }
                else
                {
                    var response = await _httpClient.PostAsJsonAsync("api/admin/player/delete", new { Token = text.Body });

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Unable to delete player." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GameDelete([FromBody] Text text)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/admin/game/delete", new { Token = text.Body });

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new { success = false, message = "Unable to delete game." });
                }
                return Json(new { success = true, message = "Game deleted." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Unable to delete game." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ResultDelete([FromBody] Text text)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/admin/result/delete", new { Token = text.Body });

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new { success = false, message = "Unable to delete result." });
                }
                return Json(new { success = true, message = "Result deleted." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Unable to delete result." });
            }
        }

        private async Task<List<PlayerView>> GetPlayers()
        {
            List<PlayerView> result = new();

            try
            {
                var response = await _httpClient.GetAsync("api/admin/player");

                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadFromJsonAsync<List<Player>>() ?? new();

                    foreach (var player in results)
                    {
                        PlayerView temp = new()
                        {
                            Token = player.Token,
                            Username = player.Username,
                            LastActivity = player.LastActivity,
                            Friends = player.Friends,
                            Requests = player.Requests,
                            Bot = player.Bot,
                            Roles = new List<string>()
                        };

                        var identityUser = await _userManager.FindByIdAsync(player.Token);
                        if (identityUser != null)
                        {
                            var roles = await _userManager.GetRolesAsync(identityUser);
                            temp.Roles = roles;
                        }

                        result.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private async Task<Player?> GetPlayer(string token)
        {
            Player result = new();

            try
            {
                var response = await _httpClient.GetAsync($"api/admin/player/{token}");

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private async Task<List<Game>> GetGames()
        {
            List<Game> result = new();

            try
            {
                var response = await _httpClient.GetAsync("api/admin/game");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new ColorArrayConverter() }
                    };

                    result = await response.Content.ReadFromJsonAsync<List<Game>>(options) ?? new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private async Task<Game> GetGame(string token)
        {
            Game result = new();

            try
            {
                var response = await _httpClient.GetAsync($"api/admin/game/{token}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new ColorArrayConverter() }
                    };

                    result = await response.Content.ReadFromJsonAsync<Game>(options) ?? new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private async Task<List<GameResult>> GetResults()
        {
            List<GameResult> result = new();

            try
            {
                var response = await _httpClient.GetAsync("api/admin/result");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new ColorArrayConverter() }
                    };

                    result = await response.Content.ReadFromJsonAsync<List<GameResult>>(options) ?? new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private async Task<GameResult> GetResult(string token)
        {
            GameResult result = new();

            try
            {
                var response = await _httpClient.GetAsync($"api/result/{token}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new ColorArrayConverter() }
                    };

                    result = await response.Content.ReadFromJsonAsync<GameResult>(options) ?? new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private async Task LogIt(PlayerLog log)
        {
            try
            {
                await _httpClient.PostAsJsonAsync($"api/admin/log", log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<List<PlayerLog>> GetLogs(string token)
        {
            List<PlayerLog> result = new();

            try
            {
                var response = await _httpClient.GetAsync($"api/admin/logs/{token}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<List<PlayerLog>>() ?? new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private async Task<PlayerLog> GetLog(string token)
        {
            PlayerLog result = new();

            try
            {
                var response = await _httpClient.GetAsync($"api/admin/log/{token}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<PlayerLog>() ?? new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
