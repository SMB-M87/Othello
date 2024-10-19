using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class GameInProgressFilter : IActionFilter
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly HttpClient _httpClient;

        public GameInProgressFilter(UserManager<IdentityUser> userManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7023/");
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            if (user is not null && user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(user);

                var gameResponse = await _httpClient.GetAsync($"api/game/from/{userId}");
                if (gameResponse.IsSuccessStatusCode)
                {
                    var gameToken = await gameResponse.Content.ReadAsStringAsync();

                    var controller = context.RouteData.Values["controller"]?.ToString();
                    var action = context.RouteData.Values["action"]?.ToString();

                    if (!(controller == "Game" && action == "PlayGame"))
                    {
                        context.Result = new RedirectToActionResult("PlayGame", "Game", new { token = gameToken });
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
