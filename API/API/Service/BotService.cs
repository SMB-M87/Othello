using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class BotService
    {
        private readonly Database _context;

        private static readonly List<string> GameDescriptions = new()
        {
            "I search an advanced player!",
            "I want to play more than one game against the same adversary.",
            "I search an advanced player that likes to chain games!",
            "میں ایک کھیل کھیلنا چاہتا ہوں اور کوئی ضرورت نہیں ہے!",
            "Θέλω να παίξω ένα παιχνίδι και δεν έχω απαιτήσεις!!!",
            "Je veux jouer une partie contre un élite!!!",
            "أريد أن ألعب لعبة وليس لدي أي متطلبات!",
            "I search an advanced player to play more than one game against!",
            "אני רוצה לשחק משחק ואין לי שום דרישות!",
            "I want to play more than one game against the same adversary.",
            "Looking for a player with good strategy skills!",
            "I want to practice my moves in a casual game.",
            "Join me for a game with no strings attached!",
            "Ready to challenge anyone who loves strategy!",
            "Play a quick game to warm up!",
            "Anyone up for a friendly match?",
            "Searching for someone who enjoys competitive games!",
            "Let’s play a game without strict rules!",
            "In the mood for a strategic duel!",
            "Looking for a serious player to improve my skills."
        };

        public BotService(Database context)
        {
            _context = context;
        }

        public async Task CreateGamesAsync()
        {
            var bots = await _context.Players.Where(p => p.IsBot).ToListAsync();
            Random random = new();

            foreach (var bot in bots)
            {
                if (bot.Username != "Gissa" && bot.Username != "Hidde" && bot.Username != "Pedro")
                {
                    bool hasPendingGame = await _context.Games.AnyAsync(g => g.First == bot.Token && g.Status == Status.Pending);

                    if (!hasPendingGame)
                    {
                        var game = new Game(bot.Token, GameDescriptions[random.Next(GameDescriptions.Count)]);

                        _context.Games.Add(game);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task AcceptFriendRequestsAsync()
        {
            var bots = await _context.Players.Where(p => p.IsBot).ToListAsync();

            foreach (var bot in bots)
            {
                var pendingRequests = bot.Requests
                    .Where(r => r.Type == Inquiry.Friend && !bot.Friends.Contains(r.Username))
                    .ToList();

                foreach (var request in pendingRequests)
                {
                    var requester = await _context.Players.FirstOrDefaultAsync(p => p.Username == request.Username);

                    if (requester != null)
                    {
                        bot.Friends.Add(request.Username);
                        bot.Requests.Remove(request);
                        _context.Entry(bot).Property(p => p.Friends).IsModified = true;
                        _context.Entry(bot).Property(p => p.Requests).IsModified = true;

                        requester.Friends.Add(bot.Username);
                        _context.Entry(requester).Property(p => p.Friends).IsModified = true;
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task SendGameRequestsAsync()
        {
            var bots = await _context.Players.Where(p => p.IsBot).ToListAsync();

            foreach (var bot in bots)
            {
                bool hasPendingGame = await _context.Games.AnyAsync(g => g.First == bot.Token && g.Status == Status.Pending);

                if (hasPendingGame)
                {
                    DateTime now = DateTime.UtcNow;

                    foreach (var username in bot.Friends)
                    {
                        var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == username);

                        if (player is not null)
                        {
                            var playerInGame = await _context.Games.FirstOrDefaultAsync(g => g.First == player.Token || (g.Second != null && g.Second == player.Token));

                            if (playerInGame is null && (now - player.LastActivity).TotalSeconds <= 240 &&
                                !player.Requests.Any(p => p.Username == bot.Username && p.Type == Inquiry.Game))
                            {
                                player.Requests.Add(new(Inquiry.Game, bot.Username));
                                _context.Entry(player).Property(p => p.Requests).IsModified = true;
                            }
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActivityBotAsync()
        {
            var bots = await _context.Players.Where(p => p.IsBot).ToListAsync();

            foreach (var bot in bots)
            {
                if (bot.Username != "Gissa" && bot.Username != "Hidde" && bot.Username != "Pedro")
                {
                    bot.LastActivity = DateTime.UtcNow;
                    _context.Entry(bot).Property(p => p.LastActivity).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
