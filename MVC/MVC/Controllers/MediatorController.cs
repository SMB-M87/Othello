using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC.Controllers
{
    [Authorize(Roles = Roles.Mediator)]
    public class MediatorController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public MediatorController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
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
                                      || p.Friends.Any(friend => friend.Equals(searchQuery, StringComparison.OrdinalIgnoreCase))
                                      || p.Requests.Any(request => request.Username.Equals(searchQuery, StringComparison.OrdinalIgnoreCase)))
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

        private async Task<List<Player>> GetPlayers()
        {
            var response = await _httpClient.GetAsync("api/mediator/player");
            List<Player> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<Player>>() ?? new();
            }
            return result;
        }

        private async Task<Player?> GetPlayer(string token)
        {
            var response = await _httpClient.GetAsync($"api/mediator/player/{token}");
            Player result = new();

            if (!response.IsSuccessStatusCode)
                response = await _httpClient.GetAsync($"api/mediator/player/name/{token}");

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
            var response = await _httpClient.GetAsync("api/mediator/game");
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

        private async Task<List<GameResult>> GetResults()
        {
            var response = await _httpClient.GetAsync("api/mediator/result");
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
    }
}
