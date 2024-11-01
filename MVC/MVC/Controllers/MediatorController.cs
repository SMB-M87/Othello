using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Middleware;
using MVC.Models;
using System.Net;
using System.Text.Json;

namespace MVC.Controllers
{
    [Authorize(Roles = Roles.Mediator)]
    public class MediatorController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UpdateActivity> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MediatorController(
            IConfiguration configuration,
            ILogger<UpdateActivity> logger,
            IHttpClientFactory httpClientFactory, 
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
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
        }

        public async Task<IActionResult> Players()
        {
            var model = await GetPlayers();
            return View(model);
        }

        public async Task<IActionResult> Games()
        {
            var model = await GetGames();
            return View(model);
        }

        public async Task<IActionResult> Results()
        {
            var model = await GetResults();
            return View(model);
        }

        private async Task<List<Player>> GetPlayers()
        {
            var response = await _httpClient.GetAsync("api/mediator/player");
            List<Player> result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                result = await response.Content.ReadFromJsonAsync<List<Player>>(options) ?? new();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Request failed with status code {response.StatusCode}: {errorContent}");
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
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Request failed with status code {response.StatusCode}: {errorContent}");
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
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Request failed with status code {response.StatusCode}: {errorContent}");
            }
            return result;
        }
    }
}
