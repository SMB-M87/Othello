using Microsoft.AspNetCore.Identity;
using MVC.Models;

namespace MVC.Middleware
{
    public class URLManager
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;

        public URLManager(
            RequestDelegate next,
            IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var user = context.User;
            var isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<IdentityUser>>();

            if (user is not null && user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = userManager.GetUserId(user);
                var existingUser = await userManager.FindByIdAsync(userId);

                if (existingUser == null)
                {
                    await signInManager.SignOutAsync();
                    context.Response.Redirect("/Home/Index");
                    return;
                }

                if (!isAjaxRequest)
                {
                    var token = userManager.GetUserId(user);
                    var httpClient = _httpClientFactory.CreateClient("ApiClient");
                    var currentPath = context.Request.Path.Value?.ToLower();
                    var response = await httpClient.GetAsync($"api/game/{token}");

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
                        if (user.IsInRole(Roles.Administrator) && currentPath is not null &&
                           !currentPath.Contains("/home") && !currentPath.Contains("/account") && !currentPath.Contains("/admin"))
                        {
                            context.Response.Redirect($"/Home/Index");
                            return;
                        }
                        else if (user.IsInRole(Roles.Mediator) && currentPath is not null &&
                            !currentPath.Contains("/home") && !currentPath.Contains("/account") && !currentPath.Contains("/mediator"))
                        {
                            context.Response.Redirect($"/Home/Index");
                            return;
                        }
                        else if (user.IsInRole(Roles.Player) && currentPath is not null && !currentPath.Contains("/home") && !currentPath.Contains("/account"))
                        {
                            context.Response.Redirect($"/Home/Index");
                            return;
                        }
                    }
                }
            }
            await _next(context);
        }

        private static bool PlayingAllowedPath(string path)
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

        private static bool PendingAllowedPath(string path)
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
