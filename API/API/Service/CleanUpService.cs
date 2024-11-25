using API.Data;
using API.Models;

namespace API.Service
{
    public class CleanUpService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(10);

        public CleanUpService(IServiceProvider serviceProvider)
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
                    await DeleteGameInvites(context);
                    await CleanUpInactiveGames(context);
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private static async Task DeleteGameInvites(Database context)
        {
            var activePlayers = context.Players
                    .AsEnumerable()
                    .Where(p => p.Requests.Any(r => r.Type == Inquiry.Game)).ToList();

            foreach (var player in activePlayers)
            {
                var expiredRequests = player.Requests
                    .Where(request => request.Type == Inquiry.Game && (DateTime.UtcNow - request.Date).TotalSeconds >= 60)
                    .ToList();

                foreach (var expiredRequest in expiredRequests)
                {
                    player.Requests.Remove(expiredRequest);
                }
                context.Entry(player).Property(p => p.Requests).IsModified = true;
            }
            await context.SaveChangesAsync();
        }

        private static async Task CleanUpInactiveGames(Database context)
        {
            var activeGames = context.Games.Where(g => g.Status != Status.Finished).ToList();

            foreach (var game in activeGames)
            {
                if (game.Second != null)
                {
                    var first = GetPlayer(game.First, context);
                    var second = GetPlayer(game.Second, context);

                    if (first != null && second != null)
                    {
                        double first_timer = (DateTime.UtcNow - first.LastActivity).TotalSeconds;
                        double second_timer = (DateTime.UtcNow - second.LastActivity).TotalSeconds;

                        if (first_timer >= 65 && game.FColor == game.PlayersTurn)
                        {
                            ForfeitGame(game, game.Second, game.First, context);
                        }
                        else if (second_timer >= 65 && game.SColor == game.PlayersTurn)
                        {
                            ForfeitGame(game, game.First, game.Second, context);
                        }
                    }
                }
                else
                {
                    var first = GetPlayer(game.First, context);

                    if (first != null && first.Bot == 0)
                    {
                        double first_timer = (DateTime.UtcNow - game.Date).TotalSeconds;

                        if (first_timer >= 240)
                        {
                            DeleteGame(game, first, context);
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

        private static void DeleteGame(Game game, Player player, Database context)
        {
            context.Games.Remove(game);

            var playersWithGameRequests = context.Players.ToList();
            playersWithGameRequests = playersWithGameRequests.Where(p => p.Requests.Any(r => r.Username == player.Username && (int)r.Type == (int)Inquiry.Game)).ToList();

            foreach (var gamer in playersWithGameRequests)
            {
                var request = gamer.Requests.FirstOrDefault(r => r.Type == Inquiry.Game && r.Username == player.Username);

                if (request != null)
                {
                    gamer.Requests.Remove(request);
                    context.Entry(gamer).Property(p => p.Requests).IsModified = true;
                }
            }
        }

        private static Player? GetPlayer(string playerToken, Database context)
        {
            return context.Players.FirstOrDefault(p => p.Token == playerToken);
        }
    }
}
