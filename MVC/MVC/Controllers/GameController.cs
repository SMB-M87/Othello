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

        public IActionResult Wait(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model: token);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string token)
        {
            var response = await _httpClient.PostAsJsonAsync("api/game/delete", token);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete game.");
                return RedirectToAction("Wait", new { player = token });
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Play(string token)
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
            return RedirectToAction("Result", "Home", new { token });
        }
    }
}
