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

        public void Create(GameResult result)
        {
            result.Date = DateTime.UtcNow;
            _context.Results.Add(result);

            var game = _context.Games.FirstOrDefault(s => s.Token.Equals(result.Token));

            if (game != null)
                _context.Games.Remove(game);

            _context.SaveChanges();
        }

        public GameResult? Get(string token)
        {
            return _context.Results.FirstOrDefault(s => s.Token == token);
        }

        public string GetPlayerStats(string token)
        {
            var results = GetPlayersMatchHistory(token);
            int wins = results.Count(r => r.Winner == token && r.Draw == false);
            int losses = results.Count(r => r.Loser == token && r.Draw == false);
            int draws = results.Count(r => (r.Winner.Contains(token) || r.Loser.Contains(token)) && r.Draw == true);

            return $"Wins:{wins}\t\tLosses:{losses}\t\tDraws:{draws}";
        }

        public List<GameResult> GetPlayersMatchHistory(string token)
        {
            return _context.Results
                .Where(s => s.Winner == token || s.Loser == token)
                .ToList();
        }
    }
}
