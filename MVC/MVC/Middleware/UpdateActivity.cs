using System.Security.Claims;

namespace MVC.Middleware
{
    public class UpdateActivity
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UpdateActivity> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateActivity(RequestDelegate next, IHttpClientFactory httpClientFactory, ILogger<UpdateActivity> logger)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var user = context.User;
            try
            {
                if (user?.Identity != null && user.Identity.IsAuthenticated)
                {
                    var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId != null)
                    {
                        var httpClient = _httpClientFactory.CreateClient();

                        var response = await httpClient.PostAsJsonAsync($"https://localhost:7023/api/player/activity", new { Token = userId });

                        if (!response.IsSuccessStatusCode)
                        {
                            _logger.LogWarning("Failed to update player activity for user: {UserId}", userId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in UpdateActivity middleware.");
            }

            await _next(context);
        }
    }
}
