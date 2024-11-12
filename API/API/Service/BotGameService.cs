namespace API.Service
{
    public class BotGameService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(1);

        public BotGameService(IServiceProvider services)
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
                    await botService.MakeMovesAsync();
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
