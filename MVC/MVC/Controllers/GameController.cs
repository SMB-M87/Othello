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
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                var token = _userManager.GetUserId(User);
                var response = await GetView(token);

                if (response.Partial.InGame == true)
                {
                    var model = new GamePlay
                    {
                        Opponent = response.Opponent,
                        Color = response.Color,

                        Partial = new GamePartial
                        {
                            InGame = response.Partial.InGame,
                            PlayersTurn = response.Partial.PlayersTurn,
                            IsPlayersTurn = response.Partial.IsPlayersTurn,
                            PossibleMove = response.Partial.PossibleMove,
                            Board = response.Partial.Board,
                            Time = response.Partial.Time,
                            Finished = response.Partial.Finished
                        }
                    };
                    return View(model);
                }
                ModelState.AddModelError(string.Empty, "Unable to retrieve game information.");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Partial()
        {
            var token = _userManager.GetUserId(User);
            var response = await GetPartial(token);

            var model = new GamePartial
            {
                InGame = response.InGame,
                PlayersTurn = response.PlayersTurn,
                IsPlayersTurn = response.IsPlayersTurn,
                PossibleMove = response.PossibleMove,
                Board = response.Board,
                Time = response.Time,
                Finished = response.Finished
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

        private async Task<GamePlay> GetView(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/view/{token}");
            GamePlay result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var deserializedResult = await response.Content.ReadFromJsonAsync<GamePlay>(options);

                if (deserializedResult is not null)
                {
                    result = deserializedResult;
                }
            }
            else
            {
                result.Partial.InGame = false;
            }
            return result;
        }

        private async Task<GamePartial> GetPartial(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/partial/{token}");
            GamePartial result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var deserializedResult = await response.Content.ReadFromJsonAsync<GamePartial>(options);

                if (deserializedResult is not null)
                {
                    result = deserializedResult;
                }
            }
            return result;
        }
    }
}
