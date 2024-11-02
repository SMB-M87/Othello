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

        public GameResult? Get(string token)
        {
            var response = _context.Results.FirstOrDefault(s => s.Token == token);

            if (response is not null)
            {
                response.Winner = GetPlayersName(response.Winner) ?? string.Empty;
                response.Loser = GetPlayersName(response.Loser) ?? string.Empty;
                return response;
            }
            return null;
        }

        public GameResult? GetLast(string player_token)
        {
            var response = GetMatchHistory(player_token);

            if (response is not null)
            {
                GameResult? result = response.OrderBy(game => Math.Abs((DateTime.UtcNow - game.Date).Ticks)).FirstOrDefault();

                if (result is not null)
                {
                    result.Winner = GetPlayersName(result.Winner) ?? string.Empty;
                    result.Loser = GetPlayersName(result.Loser) ?? string.Empty;
                }
                return result;
            }
            return null;
        }

        public string? GetPlayerStats(string username)
        {
            var token = GetPlayersToken(username);

            if (token is not null)
            {
                List<GameResult> results = GetMatchHistory(token);

                int wins = results.Count(r => r.Winner == token && r.Draw == false);
                int losses = results.Count(r => r.Loser == token && r.Draw == false);
                int draws = results.Count(r => (r.Winner == token || r.Loser == token) && r.Draw == true);

                return $"Wins:{wins}\t\tLosses:{losses}\t\tDraws:{draws}";
            }
            return null;
        }

        public List<GameResult> GetResults()
        {
            return _context.Results.ToList();
        }

        public bool Delete(string token)
        {
            var result = Get(token);

            if (result is not null)
            {
                _context.Results.Remove(result);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private string? GetPlayersToken(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username == username)?.Token;
        }

        private string? GetPlayersName(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token == token)?.Username;
        }

        private List<GameResult> GetMatchHistory(string player_token)
        {
            return _context.Results
                .Where(s => s.Winner == player_token || s.Loser == player_token)
                .ToList();
        }
    }
}
