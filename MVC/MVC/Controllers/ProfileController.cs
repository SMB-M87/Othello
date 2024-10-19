using MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace MVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7023/");
        }

        public async Task<IActionResult> Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            // Fetch the token by username
            var respons = await _httpClient.GetAsync($"api/player/token/{username}");
            if (!respons.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            // Extract the token from the response
            var token = await respons.Content.ReadAsStringAsync();

            // Fetch player stats by token
            var statsResponse = await _httpClient.GetAsync($"api/result/stats/{token}");
            string stats = string.Empty;

            if (statsResponse.IsSuccessStatusCode)
            {
                stats = await statsResponse.Content.ReadAsStringAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch player stats.");
            }

            // Fetch match history by token
            var historyResponse = await _httpClient.GetAsync($"api/result/{token}");
            List<GameResult> matchHistory = new();

            if (historyResponse.IsSuccessStatusCode)
            {
                matchHistory = await historyResponse.Content.ReadFromJsonAsync<List<GameResult>>() ?? new();
            }

            if (matchHistory.Count > 0)
            {
                foreach (var game in matchHistory)
                {
                    game.Winner = await GetPlayersName(game.Winner);
                    game.Loser = await GetPlayersName(game.Loser);

                    if (!string.IsNullOrEmpty(game.Draw))
                    {
                        var drawTokens = game.Draw.Split(' ');
                        game.Draw = $"{await GetPlayersName(drawTokens[0] == username ? drawTokens[1] : drawTokens[0])}";
                    }
                }
            }

            var model = new ProfileView
            {
                Stats = stats,
                Username = username,
                MatchHistory = matchHistory
            };

            return View(model);
        }

        [HttpGet]
        public async Task<string> GetPlayersName(string token)
        {
            var response = await _httpClient.GetAsync($"api/player/name/{token}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "Unknown";
        }
    }
}
