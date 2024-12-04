using Microsoft.AspNetCore.Identity;
using MVC.Data;
using System.Net.Http;

namespace MVC.Middleware
{
    public class InactivityLogoutService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public InactivityLogoutService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckInactiveUsersAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task CheckInactiveUsersAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var baseUrl = configuration["ApiSettings:BaseUrl"];

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };

            var apiKey = configuration["ApiSettings:KEY"];
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            try
            {
                var response = await httpClient.GetAsync($"api/middleware");

                if (response.IsSuccessStatusCode)
                {
                    var players = await response.Content.ReadFromJsonAsync<List<string>>();

                    if (players is not null)
                    {
                        foreach (var player in players)
                        {
                            var user = await userManager.FindByNameAsync(player);
                            if (user != null)
                            {
                                await userManager.UpdateSecurityStampAsync(user);
                            }
                        }
                    }

                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
