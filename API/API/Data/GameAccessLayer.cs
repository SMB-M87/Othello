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

        private bool PlayerExists(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(token)) != null;
        }

        private bool PlayerInGame(string token)
        {
            var games = _context.Games.ToList();
            var game = games!.FirstOrDefault(s => s.First.Equals(token) && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second.Equals(token) && s.Status != Status.Finished);
            return game is not null;
        }

        public bool Create(GameCreation game)
        {
            if (PlayerExists(game.Player) && !PlayerInGame(game.Player))
            {
                _context.Games.Add(new(game.Player, game.Description));
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

        private List<Game>? GetGames()
        {
            return _context.Games.ToList();
        }

        private string? GetName(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(token))?.Username;
        }

        private List<GameResult> GetMatchHistory(string token)
        {
            return _context.Results
                .Where(s => s.Winner == token || s.Loser == token)
                .ToList();
        }

        private string? GetPlayerStats(string token)
        {
            List<GameResult> results = GetMatchHistory(token);

            int wins = results.Count(r => r.Winner == token && r.Draw == false);
            int losses = results.Count(r => r.Loser == token && r.Draw == false);
            int draws = results.Count(r => (r.Winner.Contains(token) || r.Loser.Contains(token)) && r.Draw == true);

            return $"Wins:{wins}\t\tLosses:{losses}\t\tDraws:{draws}";
        }

        public List<GameDescription>? GetPendingGames()
        {
            var games = GetGames();

            if (games is not null)
            {
                var pending = games.FindAll(game => game.Status == Status.Pending && game.Second == null).ToList();

                if (pending is not null)
                {
                    List<GameDescription> result = new();

                    foreach (Game game in pending)
                    {
                        GameDescription temp = new() { Token = game.Token, Description = game.Description };
                        var player = GetName(game.First);
                        var stat = GetPlayerStats(game.First);
                        result.Add(temp);
                    }
                    return result;
                }
            }
            return null;
        }

        public string? GetGameTokenByPlayersToken(string player_token)
        {
            return GetPlayersGame(player_token)?.Token;
        }

        public Color? GetPlayersTurnByPlayersToken(string player_token)
        {
            return GetPlayersGame(player_token)?.PlayersTurn;
        }

        public Status? GetStatusByPlayersToken(string player_token)
        {
            return GetPlayersGame(player_token)?.Status;
        }

        public Color[,]? GetBoardByPlayersToken(string player_token)
        {
            return GetPlayersGame(player_token)?.Board;
        }

        private Game? Get(string token)
        {
            return _context.Games.FirstOrDefault(s => s.Token.Equals(token));
        }

        private bool PlayerInPendingGame(string token)
        {
            var games = _context.Games.ToList();
            var game = games!.FirstOrDefault(s => s.First.Equals(token) && s.Second is null && s.Status == Status.Pending);
            return game is not null;
        }

        public bool Join(GameEntrant entry)
        {
            var game = Get(entry.Token);

            if (game is not null && PlayerExists(entry.Player) && PlayerExists(game.First) &&
                entry.Player != game.First && !PlayerInGame(entry.Player) && PlayerInPendingGame(game.First))
            {
                game.SetSecondPlayer(entry.Player);
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.Second).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private Game? GetPlayersGame(string player)
        {
            var games = GetGames();
            var game = games!.FirstOrDefault(s => s.First.Equals(player) && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second.Equals(player) && s.Status != Status.Finished);

            return game;
        }

        public bool JoinPlayer(GameEntrant entry)
        {
            var game = GetPlayersGame(entry.Token);

            if (game is not null && PlayerExists(entry.Token) && PlayerExists(entry.Player) &&
                entry.Player != entry.Token && !PlayerInGame(entry.Player) && PlayerInPendingGame(entry.Token))
            {
                game.SetSecondPlayer(entry.Player);
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.Second).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public (bool succeded, string? error) Move(GameStep action)
        {
            var game = GetPlayersGame(action.Token);


            if (game is not null && PlayerExists(action.Token) && game.Second is not null)
            {
                string player = action.Token == game.First ? game.First : game.Second;
                string challenger = action.Token == game.First ? game.Second : game.First;
                Color turn = action.Token == game.First ? game.FColor : game.SColor;

                if (player != challenger && PlayerExists(challenger) &&
                    turn == game.PlayersTurn && game.Status == Status.Playing)
                {
                    try
                    {
                        game.MakeMove(action.Row, action.Column);
                        _context.Entry(game).Property(g => g.Board).IsModified = true;
                        _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;

                        if (game.Finished())
                        {
                            game.Finish();
                            _context.Entry(game).Property(g => g.Status).IsModified = true;
                            _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;

                            Color win = game.WinningColor();
                            string winner = win == Color.None ? "" : win == game.FColor ? game.First : game.Second;
                            string loser = win == Color.None ? "" : win == game.FColor ? game.Second : game.First;
                            bool draw = win == Color.None;
                            GameResult result = new(game.Token, winner, loser, draw);
                            result.Date = DateTime.UtcNow;
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

    public bool Pass(string player_token, out string error_message)
    {
        var game = GetPlayersGame(player_token);
        error_message = string.Empty;

        if (game is not null && PlayerExists(player_token) && game.Second is not null)
        {
            string player = player_token == game.First ? game.First : game.Second;
            string challenger = player_token == game.First ? game.Second : game.First;
            Color turn = player_token == game.First ? game.FColor : game.SColor;

            if (player != challenger && PlayerExists(challenger) &&
                turn == game.PlayersTurn && game.Status == Status.Playing)
            {
                try
                {
                    game.Pass();
                    _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
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
        return false;
    }

    public bool Forfeit(string player_token)
    {
        var game = GetPlayersGame(player_token);

        if (game is not null && PlayerExists(player_token) && game.Second is not null)
        {
            string player = player_token == game.First ? game.First : game.Second;
            string challenger = player_token == game.First ? game.Second : game.First;
            Color turn = player_token == game.First ? game.FColor : game.SColor;

            if (player != challenger && PlayerExists(challenger) &&
                turn == game.PlayersTurn && game.Status == Status.Playing)
            {
                string winner;
                string loser;

                if (game.First == player_token)
                {
                    winner = game.Second;
                    loser = player_token;
                }
                else
                {
                    winner = game.First;
                    loser = player_token;
                }
                GameResult result = new(game.Token, winner, loser);
                result.Date = DateTime.UtcNow;
                _context.Results.Add(result);
                _context.Games.Remove(game);

                game.Finish();
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                _context.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public bool Delete(string player_token)
    {
        var game = GetPlayersGame(player_token);

        if (game is not null && PlayerExists(player_token) &&
            PlayerInPendingGame(player_token) && player_token == game.First)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
}
