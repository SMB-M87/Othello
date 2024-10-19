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
            var userId = _userManager.GetUserId(User);

            var gameResponse = await _httpClient.GetAsync($"api/game/from/{userId}");
            if (gameResponse.IsSuccessStatusCode)
            {
                var gameTokenContent = await gameResponse.Content.ReadAsStringAsync();

                return RedirectToAction("PlayGame", "Game", new { token = gameTokenContent });
            }

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
                ModelState.AddModelError(string.Empty, "Unable to create game.");
                return View();
            }

            var gameToken = await game.Content.ReadAsStringAsync();

            return RedirectToAction("WaitForOpponent", new { token = gameToken });
        }

        public IActionResult WaitForOpponent(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model: token);
        }

        [HttpGet]
        public async Task<JsonResult> CheckGameStatus(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/{token}");
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var game = await response.Content.ReadFromJsonAsync<Game>(options);
                if (game != null && game.Status == Status.Playing)
                {
                    return Json(new { started = true });
                }
            }

            return Json(new { started = false });
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
            return RedirectToAction("Index", "Home");
        }
    }
}
