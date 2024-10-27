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
            var isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (!isAjaxRequest)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var user = context.User;
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    if (user is not null && user.Identity is not null && user.Identity.IsAuthenticated)
                    {
                        var token = userManager.GetUserId(user);
                        var httpClient = _httpClientFactory.CreateClient();
                        var currentPath = context.Request.Path.Value?.ToLower();
                        var response = await httpClient.GetAsync($"https://localhost:7023/api/game/status/{token}");

                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();

                            if (result == "1" && currentPath is not null && !PlayingAllowedPath(currentPath))
                            {
                                context.Response.Redirect("/Game/Play");
                                return;
                            }
                            else if (result == "0" && currentPath is not null && !PendingAllowedPath(currentPath))
                            {
                                context.Response.Redirect($"/Home/Index");
                                return;
                            }
                        }
                        else
                        {
                            if (currentPath is not null && (!currentPath.Contains("/home") && !currentPath.Contains("/account")))
                            {
                                context.Response.Redirect($"/Home/Index");
                                return;
                            }
                        }
                    }
                }
            }
            await _next(context);
        }

        private bool PlayingAllowedPath(string path)
        {
            var allowedPaths = new[]
            {
                "/game/play",
                "/game/move",
                "/game/pass",
                "/game/forfeit"
            };
            return allowedPaths.Any(path.Contains);
        }

        private bool PendingAllowedPath(string path)
        {
            var allowedPaths = new[]
            {
                "/home/index",
                "/home/delete",
                "/home/sendgamerequest",
                "/home/sendfriendrequest",
                "/home/deletefriend",
                "/home/acceptfriendrequest",
                "/home/declinefriendrequest",
                "/home/partial"
            };
            return allowedPaths.Any(path.Contains);
        }
    }
}
