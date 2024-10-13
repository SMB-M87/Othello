using Backend.Models;

namespace Backend.Data
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

        public (int Wins, int Losses, int Draws) GetPlayerStats(string token)
        {
            var results = GetPlayersMatchHistory(token);
            int wins = 0, losses = 0, draws = 0;

            if (results is not null)
            {
                wins = results.Count(r => r.Winner.Equals(token, StringComparison.Ordinal));
                losses = results.Count(r => r.Loser.Equals(token, StringComparison.Ordinal));
                draws = results.Count(r => r.Draw.Contains(token));
            }

            return (wins, losses, draws);
        }

        public List<GameResult> GetPlayersMatchHistory(string token)
        {
            return _context.Results.Where(s =>
                s.Winner.Equals(token, StringComparison.Ordinal) ||
                s.Loser.Equals(token, StringComparison.Ordinal) ||
                s.Draw.Contains(token)).ToList()
                ??
                new List<GameResult>();
        }
    }
}
