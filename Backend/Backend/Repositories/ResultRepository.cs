using Backend.Models;
using Microsoft.AspNetCore.Http;

namespace Backend.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly IRepository _repository;

        public ResultRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void Create(GameResult result)
        {
            _repository.Results().Add(result);
        }

        public (int Wins, int Losses, int Draws) GetPlayerStats(string token)
        {
            var results = _repository.ResultRepository.GetPlayersMatchHistory(token);
            int wins = -1, losses = -1, draws = -1;

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
            return _repository.Results().Where(s =>
                s.Winner.Equals(token, StringComparison.Ordinal) ||
                s.Loser.Equals(token, StringComparison.Ordinal) ||
                s.Draw.Contains(token)).ToList() 
                ??
                new List<GameResult>();
        }
    }
}
