using API.Data;
using API.Models;

namespace API.Service
{
    public class InactiveGameService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(15);

        public InactiveGameService(IServiceProvider serviceProvider)
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
                    await CheckAndForfeitInactiveGames(context);
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private static async Task CheckAndForfeitInactiveGames(Database context)
        {
            var activeGames = context.Games.Where(g => g.Status == Status.Playing).ToList();

            foreach (var game in activeGames)
            {
                if (game.Second != null)
                {
                    var first = GetPlayer(game.First, context);
                    var second = GetPlayer(game.Second, context);

                    if (first != null && second != null)
                    {
                        double first_timer = (DateTime.UtcNow - first.LastActivity).TotalSeconds;
                        double second_timer = (DateTime.UtcNow - first.LastActivity).TotalSeconds;

                        if (game.PlayersTurn != game.FColor && first_timer >= 100)
                        {
                            ForfeitGame(game, game.Second, game.First, context);
                        }
                        else if (game.PlayersTurn != game.SColor && second_timer >= 100)
                        {
                            ForfeitGame(game, game.First, game.Second, context);
                        }
                    }
                }
            }

            await context.SaveChangesAsync();
        }

        private static void ForfeitGame(Game game, string winner, string loser, Database context)
        {
            game.Finish();

            GameResult result = new(game.Token, winner, loser, game.Board, false, true)
            {
                Date = DateTime.UtcNow
            };
            context.Results.Add(result);
            context.Games.Remove(game);

            context.Entry(game).Property(g => g.Status).IsModified = true;
            context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
        }

        private static Player? GetPlayer(string playerToken, Database context)
        {
            return context.Players.FirstOrDefault(p => p.Token == playerToken);
        }
    }
}
