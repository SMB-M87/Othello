using MVC.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public GameController(IHttpClientFactory httpClientFactory, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7023/");
        }

        public async Task<IActionResult> Play()
        {
            var token = _userManager.GetUserId(User);
            var response = await _httpClient.GetAsync($"api/game/{token}");

            if (response.IsSuccessStatusCode)
            {
                var model = new GameView
                {
                    Opponent = await Opponent(token),
                    Color = await Color(token),
                    PlayersTurn = await PlayersTurn(token),
                    Board = await Board(token)
                };

                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Unable to retrieve game information.");
            return RedirectToAction("Index", "Home");
        }

        private async Task<string> Opponent(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/opponent/{token}");
            string result = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<string>() ?? string.Empty;
            }
            return result;
        }

        private async Task<Color> Color(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/color/{token}");
            Color result = Models.Color.None;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<Color>();
            }
            return result;
        }

        private async Task<Color> PlayersTurn(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/turn/{token}");
            Color result = Models.Color.None;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<Color>();
            }
            return result;
        }

        private async Task<Color[,]> Board(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/board/{token}");
            Color[,] result = new Models.Color[8,8];

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var deserializedResult = await response.Content.ReadFromJsonAsync<Color[,]>(options);

                if (deserializedResult is not null)
                {
                    result = deserializedResult;
                }
            }
            return result;
        }
    }
}
