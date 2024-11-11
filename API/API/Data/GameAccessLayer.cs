using API.Models;

namespace API.Data
{
    public class GameAccessLayer : IGameRepository
    {
        private readonly Database _context;

        public GameAccessLayer(Database context)
        {
            _context = context;
        }

        public Game? Get(string token)
        {
            return _context.Games.FirstOrDefault(g => g.Token.Equals(token));
        }

        public Game? GetPlayersGame(string player_token)
        {
            var games = GetGames();
            var game = games!.FirstOrDefault(s => s.First.Equals(player_token) && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second.Equals(player_token) && s.Status != Status.Finished);

            return game;
        }

        public Status? GetStatusByPlayersToken(string token)
        {
            return GetPlayersGame(token)?.Status;
        }

        public List<Game> GetGames()
        {
            return _context.Games.ToList();
        }

        public GamePlay GetView(string token)
        {
            var game = GetPlayersGame(token);

            if (game != null && game.Second != null)
            {
                var first = GetPlayer(game.First);
                var second = GetPlayer(game.Second);

                var model = new GamePlay
                {
                    Opponent = game.Second == null || first == null || second == null ? string.Empty : game.Second == token ? first.Username : second.Username,
                    Color = game.Second == null ? Color.None : game.Second == token ? game.SColor : game.FColor,
                    Partial = new GamePartial()
                    {
                        Time = GetTimerByPlayersToken(token),
                        InGame = game.Status == Status.Playing,
                        PlayersTurn = game.PlayersTurn,
                        IsPlayersTurn = token == game.First ? game.FColor == game.PlayersTurn : game.SColor == game.PlayersTurn,
                        PossibleMove = game.IsThereAPossibleMove(token == game.First ? game.FColor : game.SColor),
                        Board = game.Board
                    }
                };
                return model;
            }
            else
            {
                var model = new GamePlay
                {
                    Opponent = string.Empty,
                    Color = Color.None,
                    Partial = new GamePartial()
                    {
                        InGame = false,
                        PlayersTurn = Color.None,
                        IsPlayersTurn = false,
                        PossibleMove = false,
                        Board = new Color[8, 8],
                        Time = string.Empty
                    }
                };
                return model;
            }
        }

        public GamePartial GetPartial(string token)
        {
            var game = GetPlayersGame(token);

            if (game != null && game.Second != null)
            {
                var model = new GamePartial()
                {
                    Time = GetTimerByPlayersToken(token),
                    InGame = game.Status == Status.Playing,
                    PlayersTurn = game.PlayersTurn,
                    IsPlayersTurn = token == game.First ? game.FColor == game.PlayersTurn : game.SColor == game.PlayersTurn,
                    PossibleMove = game.IsThereAPossibleMove(token == game.First ? game.FColor : game.SColor),
                    Board = game.Board
                };

                return model;
            }
            else
            {
                var model = new GamePartial()
                {
                    InGame = false,
                    PlayersTurn = Color.None,
                    IsPlayersTurn = false,
                    PossibleMove = false,
                    Board = new Color[8, 8],
                    Time = string.Empty
                };
                return model;
            }
        }

        public bool Create(GameCreation game)
        {
            if (string.IsNullOrEmpty(game.Rematch) && PlayerExists(game.PlayerToken) && !PlayerInGame(game.PlayerToken))
            {
                _context.Games.Add(new(game.PlayerToken, game.Description));

                var player = GetPlayer(game.PlayerToken);
                if (player is not null)
                {
                    var gameRequests = player.Requests.Where(r => r.Type == Inquiry.Game).ToList();

                    foreach (var request in gameRequests)
                    {
                        player.Requests.Remove(request);
                    }
                    _context.Entry(player).Property(p => p.Requests).IsModified = true;
                }
                _context.SaveChanges();
                return true;
            }
            else if (game.Rematch != null && PlayerExists(game.PlayerToken) && !PlayerInGame(game.PlayerToken))
            {
                var player = GetPlayer(game.PlayerToken);
                var opponent = GetPlayerToken(game.Rematch);

                if (player is not null && opponent is not null)
                {
                    _context.Games.Add(new(game.PlayerToken, game.Description, opponent.Token));

                    var gameRequests = player.Requests.Where(r => r.Type == Inquiry.Game).ToList();

                    foreach (var request in gameRequests)
                    {
                        player.Requests.Remove(request);
                    }
                    _context.Entry(player).Property(p => p.Requests).IsModified = true;

                    opponent.Requests.Add(new(Inquiry.Game, player.Username));
                    _context.Entry(opponent).Property(p => p.Requests).IsModified = true;
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Create(Game game)
        {
            if (PlayerExists(game.First) && !PlayerInGame(game.First))
            {
                _context.Games.Add(game);
                _context.SaveChanges();
            }
        }

        public bool JoinPlayer(PlayerRequest request)
        {
            var player_token = GetToken(request.ReceiverUsername);

            if (player_token is null)
                return false;

            var game = GetPlayersGame(player_token);

            if (game is not null && PlayerExists(player_token) && PlayerExists(request.SenderToken) &&
                request.SenderToken != player_token && !PlayerInGame(request.SenderToken) && PlayerInPendingGame(player_token))
            {
                game.SetSecondPlayer(request.SenderToken);
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.Second).IsModified = true;
                _context.Entry(game).Property(g => g.Date).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public (bool succeded, string? error) Move(GameMove action)
        {
            var game = GetPlayersGame(action.PlayerToken);

            if (game is not null && PlayerExists(action.PlayerToken) && game.Second is not null)
            {
                string player = action.PlayerToken == game.First ? game.First : game.Second;
                string challenger = action.PlayerToken == game.First ? game.Second : game.First;
                Color turn = action.PlayerToken == game.First ? game.FColor : game.SColor;

                if (player != challenger && PlayerExists(challenger) &&
                    turn == game.PlayersTurn && game.Status == Status.Playing)
                {
                    try
                    {
                        game.MakeMove(action.Row, action.Column);
                        _context.Entry(game).Property(g => g.Board).IsModified = true;
                        _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                        _context.Entry(game).Property(g => g.Date).IsModified = true;

                        if (game.Finished())
                        {
                            game.Finish();
                            _context.Entry(game).Property(g => g.Status).IsModified = true;
                            _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;

                            Color win = game.WinningColor();
                            string winner = win == Color.None ? "" : win == game.FColor ? game.First : game.Second;
                            string loser = win == Color.None ? "" : win == game.FColor ? game.Second : game.First;
                            bool draw = win == Color.None;
                            GameResult result = new(game.Token, winner, loser, game.Board, draw)
                            {
                                Date = DateTime.UtcNow
                            };
                            _context.Results.Add(result);
                            _context.Games.Remove(game);
                        }
                        _context.SaveChanges();
                        return (true, null);
                    }
                    catch (InvalidGameOperationException ex)
                    {
                        return (false, ex.Message);
                    }
                }
            }
            return (false, "Move not possible");
        }

        public bool Pass(string token, out string error_message)
        {
            var game = GetPlayersGame(token);
            error_message = string.Empty;

            if (game is not null && PlayerExists(token) && game.Second is not null)
            {
                string player = token == game.First ? game.First : game.Second;
                string challenger = token == game.First ? game.Second : game.First;
                Color turn = token == game.First ? game.FColor : game.SColor;

                if (player != challenger && PlayerExists(challenger) &&
                    turn == game.PlayersTurn && game.Status == Status.Playing)
                {
                    if (game.Finished())
                    {
                        game.Finish();
                        _context.Entry(game).Property(g => g.Status).IsModified = true;
                        _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;

                        Color win = game.WinningColor();
                        string winner = win == Color.None ? "" : win == game.FColor ? game.First : game.Second;
                        string loser = win == Color.None ? "" : win == game.FColor ? game.Second : game.First;
                        bool draw = win == Color.None;
                        GameResult result = new(game.Token, winner, loser, game.Board, draw)
                        {
                            Date = DateTime.UtcNow
                        };
                        _context.Results.Add(result);
                        _context.Games.Remove(game);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        try
                        {
                            game.Pass();
                            _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                            _context.Entry(game).Property(g => g.Date).IsModified = true;
                            _context.SaveChanges();
                            return true;
                        }
                        catch (InvalidGameOperationException ex)
                        {
                            error_message = ex.Message;
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        public bool Forfeit(string token)
        {
            var game = GetPlayersGame(token);

            if (game is not null && PlayerExists(token) && game.Second is not null)
            {
                string player = token == game.First ? game.First : game.Second;
                string challenger = token == game.First ? game.Second : game.First;
                Color turn = token == game.First ? game.FColor : game.SColor;

                if (player != challenger && PlayerExists(challenger) &&
                    turn == game.PlayersTurn && game.Status == Status.Playing)
                {
                    string winner;
                    string loser;
                    game.Finish();

                    if (game.First == token)
                    {
                        winner = game.Second;
                        loser = token;
                    }
                    else
                    {
                        winner = game.First;
                        loser = token;
                    }
                    GameResult result = new(game.Token, winner, loser, game.Board, false, true)
                    {
                        Date = DateTime.UtcNow
                    };
                    _context.Results.Add(result);
                    _context.Games.Remove(game);

                    _context.Entry(game).Property(g => g.Status).IsModified = true;
                    _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool Delete(string token)
        {
            var game = GetPlayersGame(token);

            if (game is not null && PlayerExists(token) && PlayerInPendingGame(token) && token == game.First)
            {
                _context.Games.Remove(game);

                var player = GetPlayer(game.First);
                if (player is not null)
                {
                    var playersWithGameRequests = _context.Players.ToList().FindAll(p => p.Requests.Any(r => r.Username == player.Username && r.Type == Inquiry.Game)).ToList();

                    foreach (var gamer in playersWithGameRequests)
                    {
                        var request = gamer.Requests.FirstOrDefault(r => r.Type == Inquiry.Game && r.Username == player.Username);

                        if (request != null)
                        {
                            gamer.Requests.Remove(request);
                            _context.Entry(gamer).Property(p => p.Requests).IsModified = true;
                        }
                    }
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool PermDelete(string token)
        {
            var game = Get(token);

            if (game is not null)
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private Player? GetPlayer(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token == token);
        }

        private Player? GetPlayerToken(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username == username);
        }

        private string? GetToken(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username == username)?.Token;
        }

        private bool PlayerExists(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token == token) != null;
        }

        private string GetTimerByPlayersToken(string token)
        {
            var game = GetPlayersGame(token);
            DateTime? last = game?.Date;
            DateTime now = DateTime.UtcNow;

            if (game is not null && last.HasValue)
            {
                DateTime end = last.Value.AddSeconds(30);
                double remainingSeconds = (end - now).TotalSeconds;

                if (remainingSeconds <= 0)
                {
                    game.Pass();
                    _context.Entry(game).Property(g => g.Date).IsModified = true;
                    _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                    _context.Entry(game).Property(g => g.Board).IsModified = true;
                    _context.SaveChanges();
                }
                return $"{Math.Floor(remainingSeconds)}";
            }
            return string.Empty;
        }

        private bool PlayerInPendingGame(string token)
        {
            var games = _context.Games.ToList();
            var game = games!.FirstOrDefault(s => s.First == token && s.Second is null && s.Status == Status.Pending);
            return game is not null;
        }

        private bool PlayerInGame(string token)
        {
            var games = _context.Games.ToList();
            var game = games!.FirstOrDefault(s => s.First == token && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second == token && s.Status != Status.Finished);
            return game is not null;
        }
    }
}