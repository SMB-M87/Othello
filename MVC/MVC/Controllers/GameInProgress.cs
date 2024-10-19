using Microsoft.AspNetCore.Identity;
using System.Net.Http;

namespace MVC.Controllers
{
    public class GameInProgress
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;

        public GameInProgress(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var user = context.User;

                if (user is not null && user.Identity is not null && user.Identity.IsAuthenticated)
                {
                    var userId = userManager.GetUserId(user);
                    var httpClient = _httpClientFactory.CreateClient();

                    var gameResponse = await httpClient.GetAsync($"https://localhost:7023/api/game/from/{userId}");

                    if (gameResponse.IsSuccessStatusCode)
                    {
                        var gameToken = await gameResponse.Content.ReadAsStringAsync();

                        var currentPath = context.Request.Path.Value?.ToLower();
                        if (currentPath is not null && !currentPath.Contains("/game/playgame"))
                        {
                            context.Response.Redirect($"/Game/PlayGame?token={gameToken}");
                        }
                    }
                }
            }
            await _next(context);
        }
    }
}
