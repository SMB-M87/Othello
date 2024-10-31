using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Text.Json;

namespace MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public GameController(IHttpClientFactory httpClientFactory, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Play()
        {
            var token = _userManager.GetUserId(User);
            var response = await _httpClient.GetAsync($"api/game/{token}");

            if (response.IsSuccessStatusCode)
            {
                var model = new Game
                {
                    Opponent = await Opponent(token),
                    Color = await Color(token),

                    Partial = new GamePartial
                    {
                        InGame = await InGame(token),
                        PlayersTurn = await PlayersTurn(token),
                        Board = await Board(token)
                    }
                };

                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Unable to retrieve game information.");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Partial()
        {
            var token = _userManager.GetUserId(User);

            var model = new GamePartial
            {
                InGame = await InGame(token),
                PlayersTurn = await PlayersTurn(token),
                Board = await Board(token)
            };
            return PartialView("_Partial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Move([FromBody] GameMove move)
        {
            var token = _userManager.GetUserId(User);
            var response = await _httpClient.PostAsJsonAsync("api/game/move", new { PlayerToken = token, move.Row, move.Column });

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to make the move." });
            }

            return Json(new { success = true, message = "Move successful." });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Pass()
        {
            var token = _userManager.GetUserId(User);
            var response = await _httpClient.PostAsJsonAsync("api/game/pass", new { Token = token });

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to pass." });
            }

            return Json(new { success = true, message = "Game passed successfully." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Forfeit()
        {
            var token = _userManager.GetUserId(User);
            var response = await _httpClient.PostAsJsonAsync("api/game/forfeit", new { Token = token });

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to forfeit game." });
            }

            return Json(new { success = true, message = "Game forfeited successfully." });
        }

        private async Task<string> Opponent(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/opponent/{token}");
            string result = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync() ?? string.Empty;
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

        private async Task<bool> InGame(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/{token}");
            var status = await _httpClient.GetAsync($"api/game/status/{token}");
            string result = string.Empty;

            if (response.IsSuccessStatusCode && status.IsSuccessStatusCode)
            {
                result = await status.Content.ReadAsStringAsync();
            }
            return result == "1";
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
            Color[,] result = new Models.Color[8, 8];

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
