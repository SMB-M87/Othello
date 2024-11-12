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
                        if (bot.Username == "Identity" || bot.Username == "Ernst")
                        {
                            bot.Bot = 1;
                        }
                        else if (bot.Username == "Pipo" || bot.Username == "Eltjo")
                        {
                            bot.Bot = 2;
                        }
                        else if (bot.Username == "Salie" || bot.Username == "Tijn")
                        {
                            bot.Bot = 3;
                        }
                        else
                        {
                            bot.Bot = _random.Next(1, 4);
                        }
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
                    Color player = bot.Token == game.First ? game.FColor : game.SColor;

                    if (game.IsThereAPossibleMove(player))
                    {
                        var possibleMoves = GetPossibleMoves(game.Board);

                        var move = ChooseBotMove(possibleMoves, bot, game.Board, player);

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
            if (bot.Bot == 2 || bot.Bot == 3)
            {
                return EvaluateMoves(possibleMoves, board, colorPlayer, bot.Bot);
            }
            else
            {
                return possibleMoves[_random.Next(possibleMoves.Count)];
            }
        }

        private static GameMove EvaluateMoves(List<GameMove> possibleMoves, Color[,] board, Color colorPlayer, int bot)
        {
            GameMove? bestMove = null;
            int maxScore = int.MinValue;

            foreach (var move in possibleMoves)
            {
                var simulatedBoard = (Color[,])board.Clone();

                int flipped = SimulateMoveAndCountFlipped(move, colorPlayer, simulatedBoard);
                int positionalValue = PositionalValue(move);

                Game switchMoves = new("", "", colorPlayer, "", Status.Playing, colorPlayer)
                {
                    Board = simulatedBoard
                };
                switchMoves.Pass();
                List<GameMove> opponentMoves = GetPossibleMoves(switchMoves.Board);

                int opponentPenalty = opponentMoves.Count > 0 ? EvaluateOpponentImpact(opponentMoves, switchMoves.Board, colorPlayer == Color.White ? Color.Black : Color.White, bot) : 0;

                int score = flipped + positionalValue - opponentPenalty;

                if (score > maxScore)
                {
                    maxScore = score;
                    bestMove = move;
                }
            }
            return bestMove ?? possibleMoves[_random.Next(possibleMoves.Count)];
        }

        private static int EvaluateOpponentImpact(List<GameMove> possibleMoves, Color[,] board, Color colorPlayer, int bot, int depth = 4)
        {
            int maxScore = int.MinValue;

            foreach (var move in possibleMoves)
            {
                int opponentPenalty = 0;
                var simulatedBoard = (Color[,])board.Clone();

                int flipped = SimulateMoveAndCountFlipped(move, colorPlayer, simulatedBoard);
                int positionalValue = PositionalValue(move);

                if (bot == 3 && depth > 0)
                {
                    Game switchMoves = new("", "", colorPlayer, "", Status.Playing, colorPlayer)
                    {
                        Board = simulatedBoard
                    };
                    switchMoves.Pass();

                    if (!switchMoves.Finished())
                    {
                        List<GameMove> botMoves = GetPossibleMoves(switchMoves.Board);

                        opponentPenalty = botMoves.Count > 0 ? EvaluateOpponentImpact(botMoves, switchMoves.Board, colorPlayer == Color.White ? Color.Black : Color.White, bot, depth - 1) : 0;
                    }
                }

                int score = flipped + positionalValue - opponentPenalty;

                if (score > maxScore)
                {
                    maxScore = score;
                }
            }
            return maxScore;
        }

        private static int SimulateMoveAndCountFlipped(GameMove move, Color colorPlayer, Color[,] board)
        {
            int flipped = 0;
            int[,] direction = new int[8, 2] {
                                {  0,  1 },
                                {  0, -1 },
                                {  1,  0 },
                                { -1,  0 },
                                {  1,  1 },
                                {  1, -1 },
                                { -1,  1 },
                                { -1, -1 } };
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

            if (PawnToEncloseInSpecifiedDirection(rowMove, columnMove, colorPlayer, colorOpponent, rowDirection, columnDirection, board))
            {
                row = rowMove + rowDirection;
                column = columnMove + columnDirection;

                while (row >= 0 && row < 8 && column >= 0 && column < 8 && board[row, column] == colorOpponent)
                {
                    board[row, column] = colorPlayer;
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

        private static int PositionalValue(GameMove move)
        {
            if ((move.Row == 0 && move.Column == 0) ||
                (move.Row == 0 && move.Column == 7) ||
                (move.Row == 7 && move.Column == 0) ||
                (move.Row == 7 && move.Column == 7))
            {
                return 10;
            }
            else if (move.Row == 0 || move.Row == 7 || move.Column == 0 || move.Column == 7)
            {
                return 5;
            }
            else if ((move.Row == 1 && move.Column == 1) || (move.Row == 1 && move.Column == 6) ||
                     (move.Row == 6 && move.Column == 1) || (move.Row == 6 && move.Column == 6))
            {
                return -5; // Negative value for positions adjacent to corners
            }
            else if (move.Row == 1 || move.Row == 6 || move.Column == 1 || move.Column == 6)
            {
                return -2; // Negative value for positions adjacent to outer lines
            }

            return 0;
        }
    }
}
