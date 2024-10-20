using Microsoft.AspNetCore.Identity;

namespace MVC.Middleware
{
    public class URLManager
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;

        public URLManager(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var user = context.User;
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                if (user is not null && user.Identity is not null && user.Identity.IsAuthenticated)
                {
                    var userId = userManager.GetUserId(user);
                    var httpClient = _httpClientFactory.CreateClient();

                    var gameResponse = await httpClient.GetAsync($"https://localhost:7023/api/game/from/{userId}");

                    if (gameResponse.IsSuccessStatusCode)
                    {
                        var token = await gameResponse.Content.ReadAsStringAsync();

                        var gameStatusResponse = await httpClient.GetAsync($"https://localhost:7023/api/game/status/{token}");

                        if (gameStatusResponse.IsSuccessStatusCode)
                        {
                            var gameStatus = await gameStatusResponse.Content.ReadAsStringAsync();

                            var currentPath = context.Request.Path.Value?.ToLower();
                            if (gameStatus == "1" && currentPath is not null && !currentPath.Contains("/game/playgame"))
                            {
                                context.Response.Redirect($"/Game/PlayGame?token={token}");
                            }
                            else if (gameStatus == "0" && currentPath is not null && !currentPath.Contains("/game/waitforopponent"))
                            {
                                context.Response.Redirect($"/Game/WaitForOpponent?token={token}");
                            }
                        }
                    }
                }
            }
            await _next(context);
        }
    }
}
