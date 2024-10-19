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
            _context.Results.Add(result);

            var game = _context.Games.FirstOrDefault(s => s.Token.Equals(result.Token));

            if (game != null)
                _context.Games.Remove(game);

            _context.SaveChanges();
        }

        public string GetPlayerStats(string token)
        {
            var results = GetPlayersMatchHistory(token);
            int wins = results.Count(r => r.Winner == token);
            int losses = results.Count(r => r.Loser == token);
            int draws = results.Count(r => r.Draw.Contains(token));

            return $"Wins:{wins}\t\tLosses:{losses}\t\tDraws:{draws}";
        }

        public List<GameResult> GetPlayersMatchHistory(string token)
        {
            return _context.Results
                .Where(s => s.Winner == token || s.Loser == token || s.Draw.Contains(token))
                .ToList();
        }
    }
}
