using API.Data;
using API.Models;

namespace API.Service
{
    public class GameService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5);

        public GameService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<Database>();
                    await PassGameForInactivePlayer(context);
                    await FinishGame(context);
                }
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private static async Task PassGameForInactivePlayer(Database context)
        {
            var activeGames = context.Games.Where(g => g.Status == Status.Playing).ToList();

            foreach (var game in activeGames)
            {
                if (game.Second != null)
                {
                    DateTime? last = game.Date;
                    DateTime now = DateTime.UtcNow;
                    DateTime end = last.Value.AddSeconds(30);
                    double remainingSeconds = (end - now).TotalSeconds;

                    if (remainingSeconds <= 0)
                    {
                        game.Pass();
                        context.Entry(game).Property(g => g.Date).IsModified = true;
                        context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                        context.Entry(game).Property(g => g.Board).IsModified = true;
                    }
                }
            }
            await context.SaveChangesAsync();
        }

        private static async Task FinishGame(Database context)
        {
            var activeGames = context.Games.Where(g => g.Status == Status.Finished).ToList();

            foreach (var game in activeGames)
            {
                if (game.Second != null)
                {
                    Color win = game.WinningColor();
                    string winner = win == game.FColor ? game.First : game.Second;
                    string loser = win == game.FColor ? game.Second : game.First;
                    bool draw = win == Color.None;
                    GameResult result = new(game.Token, winner, loser, game.Board, draw)
                    {
                        Date = DateTime.UtcNow
                    };
                    context.Results.Add(result);
                    context.Games.Remove(game);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
