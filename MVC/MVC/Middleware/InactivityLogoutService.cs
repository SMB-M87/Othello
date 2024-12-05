using Microsoft.AspNetCore.Identity;
using MVC.Data;
using MVC.Models;
using System.Net.Http;
using System.Numerics;

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
                        if (players.Count > 1)
                        {
                            foreach (var player in players)
                            {
                                var user = await userManager.FindByNameAsync(player);
                                if (user != null)
                                {
                                    await userManager.UpdateSecurityStampAsync(user);
                                }
                            }
                            string formattedPlayers = string.Join(", ", players);
                            PlayerLog log = new("Inactivity Middleware", "Middleware/Inactivity", $"Players {formattedPlayers} are logged out.");
                            await httpClient.PostAsJsonAsync($"api/log", log);
                        }
                        else
                        {
                            string? player = players.FirstOrDefault();
                            if (player != null)
                            {
                                var user = await userManager.FindByNameAsync(player);
                                if (user != null)
                                {
                                    await userManager.UpdateSecurityStampAsync(user);
                                }
                                PlayerLog log = new("Inactivity Middleware", "Middleware/Inactivity", $"Player {player} is logged out.");
                                await httpClient.PostAsJsonAsync($"api/log", log);
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
