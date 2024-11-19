using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class GameAccessLayer : IGameRepository
    {
        private readonly Database _context;

        public GameAccessLayer(Database context)
        {
            _context = context;
        }

        public async Task<Game?> Get(string token)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Token.Equals(token));
        }

        public async Task<Game?> GetPlayersGame(string player_token)
        {
            var games = await GetGames();
            var game = games!.FirstOrDefault(s => s.First.Equals(player_token) && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second.Equals(player_token) && s.Status != Status.Finished);

            return game;
        }

        public async Task<Status?> GetStatusByPlayersToken(string token)
        {
            var game = await GetPlayersGame(token);
            return game?.Status;
        }

        public async Task<List<Game>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<GamePlay> GetView(string token)
        {
            var game = await GetPlayersGame(token);

            if (game != null && game.Second != null)
            {
                var first = await GetPlayer(game.First);
                var second = await GetPlayer(game.Second);

                var model = new GamePlay
                {
                    Opponent = game.Second == null || first == null || second == null ? string.Empty : game.Second == token ? first.Username : second.Username,
                    Color = game.Second == null ? Color.None : game.Second == token ? game.SColor : game.FColor,
                    Partial = new GamePartial()
                    {
                        Time = await GetTimerByPlayersToken(token),
                        InGame = game.Status == Status.Playing,
                        PlayersTurn = game.PlayersTurn,
                        IsPlayersTurn = token == game.First ? game.FColor == game.PlayersTurn : game.SColor == game.PlayersTurn,
                        PossibleMove = game.IsThereAPossibleMove(token == game.First ? game.FColor : game.SColor),
                        Board = game.Board,
                        Finished = game.Status == Status.Finished
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

        public async Task<GamePartial> GetPartial(string token)
        {
            var game = await GetPlayersGame(token);

            if (game != null && game.Second != null)
            {
                var model = new GamePartial()
                {
                    Time = await GetTimerByPlayersToken(token),
                    InGame = game.Status == Status.Playing,
                    PlayersTurn = game.PlayersTurn,
                    IsPlayersTurn = token == game.First ? game.FColor == game.PlayersTurn : game.SColor == game.PlayersTurn,
                    PossibleMove = game.IsThereAPossibleMove(token == game.First ? game.FColor : game.SColor),
                    Board = game.Board,
                    Finished = game.Status == Status.Finished
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

        public async Task<bool> Create(GameCreation game)
        {
            bool player_exists = await PlayerExists(game.PlayerToken);
            bool player_not_in_game = !await PlayerInGame(game.PlayerToken);

            if (string.IsNullOrEmpty(game.Rematch) && player_exists && player_not_in_game)
            {
                await _context.Games.AddAsync(new(game.PlayerToken, game.Description));

                var player = await GetPlayer(game.PlayerToken);
                if (player is not null)
                {
                    var gameRequests = player.Requests.Where(r => r.Type == Inquiry.Game).ToList();

                    foreach (var request in gameRequests)
                    {
                        player.Requests.Remove(request);
                    }
                    _context.Entry(player).Property(p => p.Requests).IsModified = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            else if (game.Rematch != null && player_exists && player_not_in_game)
            {
                var player = await GetPlayer(game.PlayerToken);
                var opponent = await GetPlayerToken(game.Rematch);

                if (player is not null && opponent is not null)
                {
                    await _context.Games.AddAsync(new(game.PlayerToken, game.Description, opponent.Token));

                    var gameRequests = player.Requests.Where(r => r.Type == Inquiry.Game).ToList();

                    foreach (var request in gameRequests)
                    {
                        player.Requests.Remove(request);
                    }
                    _context.Entry(player).Property(p => p.Requests).IsModified = true;

                    opponent.Requests.Add(new(Inquiry.Game, player.Username));
                    _context.Entry(opponent).Property(p => p.Requests).IsModified = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task Create(Game game)
        {
            if (await PlayerExists(game.First) && !await PlayerInGame(game.First))
            {
                _context.Games.Add(game);
                _context.SaveChanges();
            }
        }

        public async Task<bool> JoinPlayer(PlayerRequest request)
        {
            var player_token = await GetToken(request.ReceiverUsername);

            if (player_token is null)
                return false;

            var game = await GetPlayersGame(player_token);

            if (game is not null && await PlayerExists(player_token) && await PlayerExists(request.SenderToken) &&
                request.SenderToken != player_token && !await PlayerInGame(request.SenderToken) && await PlayerInPendingGame(player_token))
            {
                game.SetSecondPlayer(request.SenderToken);
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.Second).IsModified = true;
                _context.Entry(game).Property(g => g.Date).IsModified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<(bool succeded, string? error)> Move(GameMove action)
        {
            var game = await GetPlayersGame(action.PlayerToken);

            if (game is not null && await PlayerExists(action.PlayerToken) && game.Second is not null)
            {
                string player = action.PlayerToken == game.First ? game.First : game.Second;
                string challenger = action.PlayerToken == game.First ? game.Second : game.First;
                Color turn = action.PlayerToken == game.First ? game.FColor : game.SColor;

                if (player != challenger && await PlayerExists(challenger) &&
                    turn == game.PlayersTurn && game.Status == Status.Playing)
                {
                    try
                    {
                        game.MakeMove(action.Row, action.Column);
                        _context.Entry(game).Property(g => g.Board).IsModified = true;
                        _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                        _context.Entry(game).Property(g => g.Status).IsModified = true;
                        _context.Entry(game).Property(g => g.Date).IsModified = true;
                        await _context.SaveChangesAsync();
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

        public async Task<(bool succeded, string? error)> Pass(string token)
        {
            var game = await GetPlayersGame(token);
            string error = string.Empty;

            if (game is not null && await PlayerExists(token) && game.Second is not null)
            {
                string player = token == game.First ? game.First : game.Second;
                string challenger = token == game.First ? game.Second : game.First;
                Color turn = token == game.First ? game.FColor : game.SColor;

                if (player != challenger && await PlayerExists(challenger) &&
                    turn == game.PlayersTurn && game.Status == Status.Playing)
                {
                    try
                    {
                        game.Pass();
                        _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                        _context.Entry(game).Property(g => g.Board).IsModified = true;
                        _context.Entry(game).Property(g => g.Status).IsModified = true;
                        _context.Entry(game).Property(g => g.Date).IsModified = true;
                        await _context.SaveChangesAsync();
                        return (true, error);
                    }
                    catch (InvalidGameOperationException ex)
                    {
                        error = ex.Message;
                        return (false, error);
                    }
                }
            }
            return (false, "Pass not possible.");
        }

        public async Task<bool> Forfeit(string token)
        {
            var game = await GetPlayersGame(token);

            if (game is not null && await PlayerExists(token) && game.Second is not null)
            {
                string player = token == game.First ? game.First : game.Second;
                string challenger = token == game.First ? game.Second : game.First;
                Color turn = token == game.First ? game.FColor : game.SColor;

                if (player != challenger && await PlayerExists(challenger) &&
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
                    await _context.Results.AddAsync(result);
                    _context.Games.Remove(game);

                    _context.Entry(game).Property(g => g.Status).IsModified = true;
                    _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Delete(string token)
        {
            var game = await GetPlayersGame(token);

            if (game is not null && await PlayerExists(token) && await PlayerInPendingGame(token) && token == game.First)
            {
                _context.Games.Remove(game);

                var player = await GetPlayer(game.First);
                if (player is not null)
                {
                    var playersWithGameRequests = await _context.Players.ToListAsync();
                    playersWithGameRequests = playersWithGameRequests.Where(p => p.Requests.Any(r => r.Username == player.Username && (int)r.Type == (int)Inquiry.Game)).ToList();

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
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> PermDelete(string token)
        {
            var game = await Get(token);

            if (game is not null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<Player?> GetPlayer(string token)
        {
            return await _context.Players.FirstOrDefaultAsync(s => s.Token == token);
        }

        private async Task<Player?> GetPlayerToken(string username)
        {
            return await _context.Players.FirstOrDefaultAsync(s => s.Username == username);
        }

        private async Task<string?> GetToken(string username)
        {
            var player = await _context.Players.FirstOrDefaultAsync(s => s.Username == username);
            return player?.Token;
        }

        private async Task<bool> PlayerExists(string token)
        {
            var player = await _context.Players.FirstOrDefaultAsync(s => s.Token == token);
            return player != null;
        }

        private async Task<string> GetTimerByPlayersToken(string token)
        {
            var game = await GetPlayersGame(token);
            DateTime? last = game?.Date;
            DateTime now = DateTime.UtcNow;

            if (game is not null && last.HasValue)
            {
                DateTime end = last.Value.AddSeconds(30);
                double remainingSeconds = (end - now).TotalSeconds;

                return $"{Math.Floor(remainingSeconds)}";
            }
            return string.Empty;
        }

        private async Task<bool> PlayerInPendingGame(string token)
        {
            var games = await _context.Games.ToListAsync();
            var game = games!.FirstOrDefault(s => s.First == token && s.Second is null && s.Status == Status.Pending);
            return game is not null;
        }

        private async Task<bool> PlayerInGame(string token)
        {
            var games = await _context.Games.ToListAsync();
            var game = games!.FirstOrDefault(s => s.First == token && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second == token && s.Status != Status.Finished);
            return game is not null;
        }
    }
}