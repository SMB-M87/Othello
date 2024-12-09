using Microsoft.AspNetCore.Identity;
using MVC.Data;
using MVC.Models;
using System.Net;

namespace MVC.Middleware
{
    public class URLManager
    {
        private readonly RequestDelegate _next;
        private readonly HttpClient _httpClient;

        public URLManager(
            RequestDelegate next,
            IHttpClientFactory httpClientFactory
            )
        {
            _next = next;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var user = context.User;
            var currentPath = context.Request.Path.Value?.ToLower();
            var isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (currentPath != null && currentPath.StartsWith("/api/"))
            {
                await ProxyApiRequest(context);
                return;
            }

            if (context.GetEndpoint() == null)
            {
                context.Response.Redirect("/Home/Index");
                return;
            }

            if (user is not null && user.Identity is not null && user.Identity.IsAuthenticated)
            {
                using var scope = serviceProvider.CreateScope();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

                var userId = userManager.GetUserId(user);
                var checkUser = await userManager.FindByIdAsync(userId);

                if (checkUser == null)
                {
                    await signInManager.SignOutAsync();
                    context.Response.Redirect("/Home/Index");
                    return;
                }

                if (!isAjaxRequest)
                {
                    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                    var baseUrl = configuration["ApiSettings:BaseUrl"];

                    var handler = new HttpClientHandler
                    {
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    };

                    var cookies = context.Request.Cookies;
                    if (cookies.TryGetValue("__Host-SharedAuthCookie", out var cookieValue))
                    {
                        handler.CookieContainer.Add(new Uri(baseUrl), new Cookie("__Host-SharedAuthCookie", cookieValue));
                    }

                    var httpClient = new HttpClient(handler)
                    {
                        BaseAddress = new Uri(baseUrl)
                    };

                    try
                    {
                        var response = await httpClient.GetAsync($"api/game/{userId}");

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
                            if (user.IsInRole(Roles.Admin) && currentPath is not null &&
                               !currentPath.Contains("/home") && !currentPath.Contains("/manage") && !currentPath.Contains("/admin"))
                            {
                                context.Response.Redirect($"/Home/Index");
                            }
                            else if (user.IsInRole(Roles.Mod) && currentPath is not null &&
                                !currentPath.Contains("/home") && !currentPath.Contains("/manage") && !currentPath.Contains("/mod"))
                            {
                                context.Response.Redirect($"/Home/Index");
                            }
                            else if (user.IsInRole(Roles.User) && !user.IsInRole(Roles.Admin) && !user.IsInRole(Roles.Mod) &&
                                currentPath is not null && !currentPath.Contains("/home") && !currentPath.Contains("/manage"))
                            {
                                context.Response.Redirect($"/Home/Index");
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine(ex.Message);
                        await signInManager.SignOutAsync();
                        context.Response.Redirect($"/Home/Index");
                        return;
                    }
                }
            }
            await _next(context);
        }

        private async Task ProxyApiRequest(HttpContext context)
        {
            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiBaseUrl = configuration["ApiSettings:BaseUrl"];

            var targetUrl = $"{apiBaseUrl.TrimEnd('/')}{context.Request.Path}{context.Request.QueryString}";

            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(context.Request.Method),
                RequestUri = new Uri(targetUrl)
            };

            foreach (var header in context.Request.Headers)
            {
                if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                {
                    requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }

            if (context.Request.ContentType != null && context.Request.ContentLength > 0)
            {
                requestMessage.Content = new StreamContent(context.Request.Body);
                requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(context.Request.ContentType);
            }

            var responseMessage = await _httpClient.SendAsync(requestMessage);

            context.Response.StatusCode = (int)responseMessage.StatusCode;
            foreach (var header in responseMessage.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            foreach (var header in responseMessage.Content.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            context.Response.Headers.Remove("transfer-encoding");

            await responseMessage.Content.CopyToAsync(context.Response.Body);
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
