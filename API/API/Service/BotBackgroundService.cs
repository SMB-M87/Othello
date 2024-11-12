﻿namespace API.Service
{
    public class BotBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(10);

        public BotBackgroundService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var botService = scope.ServiceProvider.GetRequiredService<BotService>();
                    await botService.CreateGamesAsync();
                    await botService.AcceptFriendRequestsAsync();
                    await botService.SendGameRequestsAsync();
                    await botService.UpdateActivityBotAsync();
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
