using API.Models;

namespace API.Data
{
    public class ResultAccessLayer : IResultRepository
    {
        private readonly Database _context;

        public ResultAccessLayer(Database context)
        {
            _context = context;
        }

        public bool Create(GameResult result)
        {
            var request = Get(result.Token);

            if (request is null)
            {
                result.Date = DateTime.UtcNow;
                _context.Results.Add(result);

                var game = _context.Games.FirstOrDefault(s => s.Token.Equals(result.Token));
                if (game != null)
                    _context.Games.Remove(game);

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public GameResultView? Get(string token)
        {
            var respons = _context.Results.FirstOrDefault(s => s.Token == token);

            if (respons is not null)
            {
                GameResultView result = new(GetName(respons.Winner) ?? string.Empty, GetName(respons.Loser) ?? string.Empty, respons.Draw, respons.Date);
                return result;
            }
            return null;
        }

        private string? GetToken(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(username))?.Token;
        }

        private List<GameResult> GetMatchHistory(string player_token)
        {
            return _context.Results
                .Where(s => s.Winner == player_token || s.Loser == player_token)
                .ToList();
        }

        public string? GetPlayerStats(string username)
        {
            var token = GetToken(username);

            if (token is not null)
            {
                List<GameResult> results = GetMatchHistory(token);

                int wins = results.Count(r => r.Winner == token && r.Draw == false);
                int losses = results.Count(r => r.Loser == token && r.Draw == false);
                int draws = results.Count(r => (r.Winner.Contains(token) || r.Loser.Contains(token)) && r.Draw == true);

                return $"Wins:{wins}\t\tLosses:{losses}\t\tDraws:{draws}";
            }
            return null;
        }

        private string? GetName(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(token))?.Username;
        }

        public List<GameResultView>? GetPlayersMatchHistory(string username)
        {
            var token = GetToken(username);

            if (token is not null)
            {
                var respons = GetMatchHistory(token);
                List<GameResultView> results = new();

                if (respons.Count > 0)
                {
                    respons = respons.OrderByDescending(r => r.Date).ToList();

                    foreach (var game in respons)
                    {
                        GameResultView adding = new(GetName(game.Winner) ?? string.Empty, GetName(game.Loser) ?? string.Empty, game.Draw, game.Date);
                        results.Add(adding);
                    }
                }
                return results;
            }
            return null;
        }
    }
}
