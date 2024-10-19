using MVC.Models;
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

        [HttpPost]
        public async Task<IActionResult> Create(string description)
        {
            if (ModelState.IsValid)
            {
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
                    return View(description);
                }

                // Optionally, get the game token and redirect to the game page
                // var gameToken = await response.Content.ReadAsStringAsync();
                // return RedirectToAction("PlayGame", "Game", new { token = gameToken });

                return RedirectToAction("Index", "Home");
            }

            return View(description);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGame()
        {
            var userToken = _userManager.GetUserId(User);

            var response = await _httpClient.PostAsJsonAsync("api/game/delete", userToken);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete game.");
            }

            return RedirectToAction("Index");
        }
    }
}
