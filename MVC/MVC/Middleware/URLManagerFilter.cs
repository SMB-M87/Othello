﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC.Middleware
{
    public class URLManagerFilter : IActionFilter
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public URLManagerFilter(UserManager<IdentityUser> userManager, IHttpClientFactory httpClientFactory)
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
                    var token = await gameResponse.Content.ReadAsStringAsync();

                    var gameStatusResponse = await _httpClient.GetAsync($"https://localhost:7023/api/game/status/{token}");

                    if (gameStatusResponse.IsSuccessStatusCode)
                    {
                        var gameStatus = await gameStatusResponse.Content.ReadAsStringAsync();

                        var controller = context.RouteData.Values["controller"]?.ToString();
                        var action = context.RouteData.Values["action"]?.ToString();

                        if (!(controller == "Game" && gameStatus == "1" && action == "PlayGame"))
                        {
                            context.Result = new RedirectToActionResult("PlayGame", "Game", new { token });
                        }
                        else if (!(controller == "Game" && gameStatus == "0" && action == "WaitForOpponent"))
                        {
                            context.Result = new RedirectToActionResult("WaitForOpponent", "Game", new { token });
                        }
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
