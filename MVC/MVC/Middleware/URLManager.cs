using Microsoft.AspNetCore.Identity;
using MVC.Models;
using System.Net;

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
            var currentPath = context.Request.Path.Value?.ToLower();
            var isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (context.GetEndpoint() == null)
            {
                context.Response.Redirect("/Home/Index");
                return;
            }

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

                    var httpClient = _httpClientFactory.CreateClient();
                    var baseUrl = "https://localhost:7023/";
                    var handler = new HttpClientHandler
                    {
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    };

                    var cookies = context.Request.Cookies;
                    if (cookies.TryGetValue(".AspNet.SharedAuthCookie", out var cookieValue))
                    {
                        handler.CookieContainer.Add(new Uri(baseUrl), new Cookie(".AspNet.SharedAuthCookie", cookieValue));
                    }

                    var httpClientWithCookies = new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
                    var response = await httpClientWithCookies.GetAsync($"api/game/{userId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        if ((result == "1" || result == "2") && currentPath is not null && !PlayingAllowedPath(currentPath))
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
                        if (user.IsInRole(Roles.Admin) && currentPath is not null &&
                           !currentPath.Contains("/home") && !currentPath.Contains("/account") && !currentPath.Contains("/admin"))
                        {
                            context.Response.Redirect($"/Home/Index");
                            return;
                        }
                        else if (user.IsInRole(Roles.Mod) && currentPath is not null &&
                            !currentPath.Contains("/home") && !currentPath.Contains("/account") && !currentPath.Contains("/mod"))
                        {
                            context.Response.Redirect($"/Home/Index");
                            return;
                        }
                        else if (user.IsInRole(Roles.User) && !user.IsInRole(Roles.Admin) && !user.IsInRole(Roles.Mod) && 
                            currentPath is not null && !currentPath.Contains("/home") && !currentPath.Contains("/account"))
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
