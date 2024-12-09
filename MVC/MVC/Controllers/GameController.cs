using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;
using System.Net;

namespace MVC.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class GameController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public GameController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            var baseUrl = configuration["ApiSettings:BaseUrl"] ?? throw new Exception("BaseUrl setting is missing in configuration.");

            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            var cookies = _httpContextAccessor?.HttpContext?.Request.Cookies;

            if (cookies is not null)
            {
                foreach (var cookie in cookies)
                {
                    if (cookie.Key == "__Host-SharedAuthCookie")
                    {
                        handler.CookieContainer.Add(
                            new Uri(baseUrl),
                            new Cookie(cookie.Key, cookie.Value)
                        );
                    }
                }

                _httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseUrl)
                };

                var apiKey = configuration["ApiSettings:KEY"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("API key is missing in configuration.");
                }

                if (!_httpClient.DefaultRequestHeaders.Contains("X-API-KEY"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
                }
            }
            else
            {
                _httpClient = httpClientFactory.CreateClient("ApiClient");
            }
        }

        public IActionResult Play()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                var api = /*"https://othello.hbo-ict.org/"*/"https://localhost:7269/" + "api/";
                var token = _userManager.GetUserId(User);

                ViewBag.ApiUrl = api;
                ViewBag.Token = token;

                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
