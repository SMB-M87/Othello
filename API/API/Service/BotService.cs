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

        private static readonly Random _random = new();

        public BotService(Database context)
        {
            _context = context;
        }

        public async Task CreateGamesAsync()
        {
            var bots = await _context.Players.Where(p => p.Bot != 0).ToListAsync();

            foreach (var bot in bots)
            {
                if (bot.Username != "Gissa" && bot.Username != "Hidde" && bot.Username != "Pedro")
                {
                    bool hasPendingGame = await _context.Games.AnyAsync(g => g.First == bot.Token && g.Status == Status.Pending);

                    if (!hasPendingGame)
                    {
                        bot.Bot = _random.Next(1, 3);
                        var game = new Game(bot.Token, GameDescriptions[_random.Next(GameDescriptions.Count)]);

                        _context.Games.Add(game);
                        _context.Entry(bot).Property(p => p.Bot).IsModified = true;
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task AcceptFriendRequestsAsync()
        {
            var bots = await _context.Players.Where(p => p.Bot != 0).ToListAsync();

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
            var bots = await _context.Players.Where(p => p.Bot != 0).ToListAsync();

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

        public async Task MakeMovesAsync()
        {
            var bots = await _context.Players.Where(p => p.Bot != 0).ToListAsync();

            foreach (var bot in bots)
            {
                var game = await _context.Games.FirstOrDefaultAsync(g => g.PlayersTurn == g.FColor && g.Status == Status.Playing && g.First == bot.Token);

                if (game != null && game.Second != null)
                {
                    if (game.IsThereAPossibleMove(game.FColor))
                    {
                        var possibleMoves = GetPossibleMoves(game.Board);

                        var move = ChooseBotMove(possibleMoves, bot, game.Board, game.FColor);

                        game.MakeMove(move.Row, move.Column);
                    }
                    else
                    {
                        game.Pass();
                    }
                    _context.Entry(game).Property(g => g.Board).IsModified = true;
                    _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                    _context.Entry(game).Property(g => g.Date).IsModified = true;

                    bot.LastActivity = DateTime.UtcNow;
                    _context.Entry(bot).Property(p => p.LastActivity).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActivityBotAsync()
        {
            var bots = await _context.Players.Where(p => p.Bot != 0).ToListAsync();

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

        private static List<GameMove> GetPossibleMoves(Color[,] board)
        {
            var possibleMoves = new List<GameMove>();

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == Color.PossibleMove)
                    {
                        possibleMoves.Add(new("", row, col));
                    }
                }
            }
            return possibleMoves;
        }

        private static GameMove ChooseBotMove(List<GameMove> possibleMoves, Player bot, Color[,] board, Color colorPlayer)
        {
            if (bot.Bot == 2)
            {
                GameMove? bestMove = null;
                int maxFlipped = -1;

                foreach (var move in possibleMoves)
                {
                    int flipped = SimulateMoveAndCountFlipped(move, colorPlayer, board);

                    if (flipped > maxFlipped)
                    {
                        maxFlipped = flipped;
                        bestMove = move;
                    }
                }
                return bestMove ?? possibleMoves[_random.Next(possibleMoves.Count)];
            }
            else
            {
                return possibleMoves[_random.Next(possibleMoves.Count)];
            }
        }

        private static int SimulateMoveAndCountFlipped(GameMove move, Color colorPlayer, Color[,] board)
        {
            int flipped = 0;
            int[,] direction = new int[8, 2] {
                                {  0,  1 },         // right
                                {  0, -1 },         // left
                                {  1,  0 },         // bottom
                                { -1,  0 },         // top
                                {  1,  1 },         // bottom right
                                {  1, -1 },         // bottom left
                                { -1,  1 },         // top right
                                { -1, -1 } };       // top left
            Color colorOpponent = colorPlayer == Color.White ? Color.Black : Color.White;

            for (int i = 0; i < 8; i++)
            {
                flipped += FlipOpponentsPawnsInSpecifiedDirectionIfEnclosed(move.Row, move.Column, colorPlayer, colorOpponent, direction[i, 0], direction[i, 1], board);
            }
            return flipped;
        }

        private static int FlipOpponentsPawnsInSpecifiedDirectionIfEnclosed(int rowMove, int columnMove, Color colorPlayer, Color colorOpponent, int rowDirection, int columnDirection, Color[,] board)
        {
            int row, column;
            int pawnFlipped = 0;
            var simulatedBoard = (Color[,])board.Clone();

            if (PawnToEncloseInSpecifiedDirection(rowMove, columnMove, colorPlayer, colorOpponent, rowDirection, columnDirection, simulatedBoard))
            {
                row = rowMove + rowDirection;
                column = columnMove + columnDirection;

                while (row >= 0 && row < 8 && column >= 0 && column < 8 && simulatedBoard[row, column] == colorOpponent)
                {
                    simulatedBoard[row, column] = colorPlayer;
                    row += rowDirection;
                    column += columnDirection;
                    pawnFlipped++;
                }
            }
            return pawnFlipped;
        }

        private static bool PawnToEncloseInSpecifiedDirection(int rowMove, int columnMove, Color colorPlayer, Color colorOpponent, int rowDirection, int columnDirection, Color[,] board)
        {
            int row, column;

            if (!((rowMove >= 0 && rowMove < 8 && columnMove >= 0 && columnMove < 8) && (board[rowMove, columnMove] == Color.None || board[rowMove, columnMove] == Color.PossibleMove)))
                return false;

            row = rowMove + rowDirection;
            column = columnMove + columnDirection;

            int NumberOfAdjacentPawnsOfOpponent = 0;

            while ((row >= 0 && row < 8 && column >= 0 && column < 8) && board[row, column] == colorOpponent)
            {
                row += rowDirection;
                column += columnDirection;
                NumberOfAdjacentPawnsOfOpponent++;
            }
            return (row >= 0 && row < 8 && column >= 0 && column < 8) && board[row, column] == colorPlayer && NumberOfAdjacentPawnsOfOpponent > 0;
        }
    }
}
