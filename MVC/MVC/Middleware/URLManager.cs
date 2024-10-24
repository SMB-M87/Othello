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

                    var gameResponse = await httpClient.GetAsync($"https://localhost:7023/api/game/{userId}");

                    if (gameResponse.IsSuccessStatusCode)
                    {
                        var token = await gameResponse.Content.ReadAsStringAsync();

                        var gameStatusResponse = await httpClient.GetAsync($"https://localhost:7023/api/game/status/{userId}");

                        if (gameStatusResponse.IsSuccessStatusCode)
                        {
                            var gameStatus = await gameStatusResponse.Content.ReadAsStringAsync();

                            var currentPath = context.Request.Path.Value?.ToLower();
                            if (gameStatus == "1" && currentPath is not null && !currentPath.Contains("/game/play"))
                            {
                                context.Response.Redirect($"/Game/Play?token={token}");
                                return;
                            }
                            else if (gameStatus == "0" && currentPath is not null && !currentPath.Contains("/home/wait"))
                            {
                                context.Response.Redirect($"/Home/Wait?token={token}");
                                return;
                            }
                        }
                    }
                    else
                    {
                        var currentPath = context.Request.Path.Value?.ToLower();

                        if (currentPath is not null && (!currentPath.Contains("/home") && !currentPath.Contains("/account")))
                        {
                            context.Response.Redirect($"/Home/Index");
                            return;
                        }
                    }
                }
            }
            await _next(context);
        }
    }
}
