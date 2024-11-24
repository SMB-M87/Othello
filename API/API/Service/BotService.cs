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
                    bool inGame = await _context.Games.AnyAsync(g => g.First == bot.Token || (g.Second != null && g.Second == bot.Token));

                    if (!inGame)
                    {
                        if (bot.Username == "Ernst" || bot.Username == "Karen" || bot.Username == "Eltjo")
                        {
                            bot.Bot = 2;
                        }
                        else if (bot.Username == "John")
                        {
                            bot.Bot = 3;
                        }
                        else if (bot.Username == "Tijn")
                        {
                            bot.Bot = 4;
                        }
                        else
                        {
                            bot.Bot = 1;
                        }
                        var game = new Game(bot.Token, GameDescriptions[_random.Next(GameDescriptions.Count)]);
                        _context.Games.Add(game);
                        _context.Entry(bot).Property(p => p.Bot).IsModified = true;
                    }
                    bot.LastActivity = DateTime.UtcNow;
                    _context.Entry(bot).Property(p => p.LastActivity).IsModified = true;
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
                var game = await _context.Games
                    .FirstOrDefaultAsync(g => g.Status == Status.Playing &&
                    ((g.PlayersTurn == g.FColor && g.First == bot.Token) ||
                    (g.PlayersTurn == g.SColor && g.Second != null && g.Second == bot.Token)));

                if (game != null && game.Second != null && !game.Finished())
                {
                    if (game.IsThereAPossibleMove(game.PlayersTurn))
                    {
                        try
                        {
                            var possibleMoves = GetPossibleMoves(game.Board);
                            var move = ChooseBotMove(possibleMoves, bot, game.Board, game.PlayersTurn);
                            game.MakeMove(move.Row, move.Column);
                            bot.LastActivity = DateTime.UtcNow;
                            _context.Entry(bot).Property(p => p.LastActivity).IsModified = true;
                        }
                        catch (Exception ex) 
                        {
                            Console.WriteLine($"Error while making a move for bot {bot.Token} in game {game.Token}: {ex.Message}");
                            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                            game.ByPass();
                            game.Pass();
                        }
                    }
                    else
                    {
                        game.Pass();
                    }
                    _context.Entry(game).Property(g => g.Board).IsModified = true;
                    _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                    _context.Entry(game).Property(g => g.Date).IsModified = true;
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
            if (bot.Bot >= 1 && bot.Bot <= 4 && possibleMoves.Count > 1)
            {
                Game clone = new("", "", colorPlayer, "", Status.Playing, colorPlayer)
                {
                    Board = (Color[,])board.Clone(),
                };
                return EvaluateMoves(clone, colorPlayer, bot.Bot);
            }
            else
            {
                return possibleMoves[_random.Next(possibleMoves.Count)];
            }
        }

        private static GameMove EvaluateMoves(Game gameState, Color bot, int searchDepth)
        {
            int bestValue = int.MinValue;
            GameMove? bestMove = null;
            List<GameMove> possibleMoves = GetPossibleMoves(gameState.Board);

            foreach (var move in possibleMoves)
            {
                Game clone = new("", "", gameState.PlayersTurn, "", Status.Playing, gameState.PlayersTurn)
                {
                    Board = (Color[,])gameState.Board.Clone(),
                };
                clone.MakeMove(move.Row, move.Column);

                int moveValue = Minimax(clone, searchDepth - 1, int.MinValue, int.MaxValue, false, bot);

                if (moveValue > bestValue)
                {
                    bestValue = moveValue;
                    bestMove = move;
                }
            }

            return bestMove ?? possibleMoves[_random.Next(possibleMoves.Count)];
        }

        private static int Minimax(Game gameState, int depth, int alpha, int beta, bool maximizingPlayer, Color bot)
        {
            if (depth == 0 || gameState.Finished() || bot == Color.None)
            {
                return EvaluateBoard(gameState.Board, gameState.PlayersTurn, bot);
            }

            List<GameMove> possibleMoves = GetPossibleMoves(gameState.Board);

            if (possibleMoves.Count == 0)
            {
                Game clone = new("", "", gameState.PlayersTurn, "", Status.Playing, gameState.PlayersTurn)
                {
                    Board = (Color[,])gameState.Board.Clone(),
                };
                clone.ByPass();
                clone.Pass();
                return Minimax(clone, depth - 1, alpha, beta, !maximizingPlayer, bot);
            }

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;
                foreach (var move in possibleMoves)
                {
                    Game clone = new("", "", gameState.PlayersTurn, "", Status.Playing, gameState.PlayersTurn)
                    {
                        Board = (Color[,])gameState.Board.Clone(),
                    };
                    clone.MakeMove(move.Row, move.Column);
                    int eval = Minimax(clone, depth - 1, alpha, beta, false, bot);
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (var move in possibleMoves)
                {
                    Game clone = new("", "", gameState.PlayersTurn, "", Status.Playing, gameState.PlayersTurn)
                    {
                        Board = (Color[,])gameState.Board.Clone(),
                    };
                    clone.MakeMove(move.Row, move.Column);
                    int eval = Minimax(clone, depth - 1, alpha, beta, true, bot);
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }

        private static int EvaluateBoard(Color[,] board, Color playerTurn, Color bot)
        {
            int score = 0;

            score += 100 * DiscDifference(board, bot);
            score += 80 * Mobility(board, playerTurn, bot);
            score += 1000 * CornerOccupancy(board, bot);
            score += 100 * Stability(board, bot);
            score += 10 * PositionalWeights(board, bot);

            return score;
        }

        private static int DiscDifference(Color[,] board, Color bot)
        {
            int botDiscs = 0;
            int oppDiscs = 0;
            Color botColor = bot;
            Color oppColor = botColor == Color.White ? Color.Black : Color.White;

            foreach (var cell in board)
            {
                if (cell == botColor)
                    botDiscs++;
                else if (cell == oppColor)
                    oppDiscs++;
            }
            return botDiscs - oppDiscs;
        }

        private static int Mobility(Color[,] board, Color playersTurn, Color bot)
        {
            int botMoves;
            int oppMoves;

            if (playersTurn == Color.None)
            {
                botMoves = 1000;
                oppMoves = 0;
            }
            else if (playersTurn == bot)
            {
                botMoves = GetPossibleMoves(board).Count;

                Game clone = new("", "", playersTurn, "", Status.Playing, playersTurn)
                {
                    Board = (Color[,])board.Clone(),
                };
                clone.ByPass();
                clone.Pass();

                oppMoves = GetPossibleMoves(board).Count;
            }
            else
            {
                oppMoves = GetPossibleMoves(board).Count;

                Game clone = new("", "", playersTurn, "", Status.Playing, playersTurn)
                {
                    Board = (Color[,])board.Clone(),
                };
                clone.ByPass();
                clone.Pass();

                botMoves = GetPossibleMoves(board).Count;
            }
            return botMoves - oppMoves;
        }

        private static int CornerOccupancy(Color[,] board, Color bot)
        {
            int score = 0;
            Color botColor = bot;
            Color oppColor = botColor == Color.White ? Color.Black : Color.White;

            int[,] corners = { { 0, 0 }, { 0, 7 }, { 7, 0 }, { 7, 7 } };
            for (int i = 0; i < 4; i++)
            {
                Color cell = board[corners[i, 0], corners[i, 1]];
                if (cell == botColor)
                    score += 1;
                else if (cell == oppColor)
                    score -= 1;
            }
            return score;
        }

        private static int Stability(Color[,] board, Color bot)
        {
            int[,] directions = {
                { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 },
                { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }
            };

            bool[,] stable = new bool[8, 8];
            Color botColor = bot;
            Color oppColor = botColor == Color.White ? Color.Black : Color.White;

            int[,] corners = { { 0, 0 }, { 0, 7 }, { 7, 0 }, { 7, 7 } };

            for (int i = 0; i < 4; i++)
            {
                MarkStableDiscs(corners[i, 0], corners[i, 1], board, stable, directions);
            }

            int botStableCount = 0;
            int oppStableCount = 0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (stable[row, col])
                    {
                        if (board[row, col] == botColor)
                            botStableCount++;
                        else if (board[row, col] == oppColor)
                            oppStableCount++;
                    }
                }
            }

            return botStableCount - oppStableCount;
        }

        private static void MarkStableDiscs(int row, int col, Color[,] board, bool[,] stable, int[,] directions)
        {
            if (row < 0 || row >= 8 || col < 0 || col >= 8 || stable[row, col] || board[row, col] == Color.None)
                return;

            stable[row, col] = true;

            for (int i = 0; i < 8; i++)
            {
                int newRow = row + directions[i, 0];
                int newCol = col + directions[i, 1];

                if (IsStableInDirection(row, col, directions[i, 0], directions[i, 1], board, stable))
                {
                    MarkStableDiscs(newRow, newCol, board, stable, directions);
                }
            }
        }

        private static bool IsStableInDirection(int row, int col, int rowDir, int colDir, Color[,] board, bool[,] stable)
        {
            Color currentColor = board[row, col];
            int newRow = row + rowDir;
            int newCol = col + colDir;

            while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                if (board[newRow, newCol] != currentColor)
                    return false;

                if (stable[newRow, newCol])
                    return true;

                newRow += rowDir;
                newCol += colDir;
            }

            return false;
        }

        private static int PositionalWeights(Color[,] board, Color bot)
        {
            int[,] weights = {
                {100, -20, 10, 5, 5, 10, -20, 100},
                {-20, -50, -2, -2, -2, -2, -50, -20},
                {10, -2, 5, 1, 1, 5, -2, 10},
                {5, -2, 1, 0, 0, 1, -2, 5},
                {5, -2, 1, 0, 0, 1, -2, 5},
                {10, -2, 5, 1, 1, 5, -2, 10},
                {-20, -50, -2, -2, -2, -2, -50, -20},
                {100, -20, 10, 5, 5, 10, -20, 100}
            };

            int score = 0;
            Color botColor = bot;
            Color oppColor = botColor == Color.White ? Color.Black : Color.White;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] == botColor)
                        score += weights[row, col];
                    else if (board[row, col] == oppColor)
                        score -= weights[row, col];
                }
            }
            return score;
        }
    }
}
