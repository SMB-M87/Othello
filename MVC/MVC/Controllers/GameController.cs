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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                ModelState.AddModelError(string.Empty, "Description cannot be empty.");
                return View();
            }

            var userToken = _userManager.GetUserId(User);
            var createGameRequest = new
            {
                Player = userToken,
                Description = description
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/create", createGameRequest);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to create game.");
                return View();
            }

            var game = await _httpClient.GetAsync($"api/game/from/{userToken}");

            if (!game.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to find game.");
                return View();
            }

            var token = await game.Content.ReadAsStringAsync();

            return RedirectToAction("WaitForOpponent", new { token });
        }

        public IActionResult WaitForOpponent(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model: token);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGame(string token)
        {
            var response = await _httpClient.PostAsJsonAsync("api/game/delete", token);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete game.");
                return RedirectToAction("WaitForOpponent", new { player = token });
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PlayGame(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/{token}");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var gameResult = await response.Content.ReadFromJsonAsync<Game>(options);
                return View(model: gameResult);
            }

            ModelState.AddModelError(string.Empty, "Unable to retrieve game information.");
            return RedirectToAction("GameResult", new { token });
        }

        public async Task<IActionResult> GameResult(string token)
        {
            var resultResponse = await _httpClient.GetAsync($"api/result/{token}");

            if (resultResponse.IsSuccessStatusCode)
            {
                GameResult result = await resultResponse.Content.ReadFromJsonAsync<GameResult>() ?? new();

                string url = "api/player/name/";

                var winResponse = await _httpClient.GetAsync($"{url}{result.Winner}");
                var loseResponse = await _httpClient.GetAsync($"{url}{result.Loser}");

                result.Winner = await winResponse.Content.ReadAsStringAsync();
                result.Loser = await loseResponse.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(result.Draw))
                {
                    var drawTokens = result.Draw.Split(' ');
                    var Drawer = drawTokens[0] == _userManager.GetUserId(User) ? drawTokens[1] : drawTokens[0];
                    var drawReponse = await _httpClient.GetAsync($"{url}{Drawer}");
                    result.Draw = await drawReponse.Content.ReadAsStringAsync();
                }

                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
