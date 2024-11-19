using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ResultAccessLayer : IResultRepository
    {
        private readonly Database _context;

        public ResultAccessLayer(Database context)
        {
            _context = context;
        }

        public async Task<GameResult?> Get(string token)
        {
            var response = await _context.Results.FirstOrDefaultAsync(s => s.Token == token);

            if (response is not null)
            {
                response.Winner = await GetPlayersName(response.Winner) ?? string.Empty;
                response.Loser = await GetPlayersName(response.Loser) ?? string.Empty;
                return response;
            }
            return null;
        }

        public async Task<GameResult?> GetLast(string player_token)
        {
            var response = await GetMatchHistory(player_token);

            if (response is not null)
            {
                GameResult? result = response.OrderBy(game => Math.Abs((DateTime.UtcNow - game.Date).Ticks)).FirstOrDefault();

                if (result is not null)
                {
                    result.Winner = await GetPlayersName(result.Winner) ?? string.Empty;
                    result.Loser = await GetPlayersName(result.Loser) ?? string.Empty;
                }
                return result;
            }
            return null;
        }

        public async Task<string?> GetPlayerStats(string username)
        {
            var token = await GetPlayersToken(username);

            if (token is not null)
            {
                List<GameResult> results = await GetMatchHistory(token);

                int wins = results.Count(r => r.Winner == token && r.Draw == false);
                int losses = results.Count(r => r.Loser == token && r.Draw == false);
                int draws = results.Count(r => (r.Winner == token || r.Loser == token) && r.Draw == true);

                return $"Wins:{wins}\t\tLosses:{losses}\t\tDraws:{draws}";
            }
            return null;
        }

        public async Task<List<GameResult>> GetResults()
        {
            return await _context.Results.ToListAsync();
        }

        public async Task<bool> Delete(string token)
        {
            var result = await Get(token);

            if (result is not null)
            {
                _context.Results.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<string?> GetPlayersToken(string username)
        {
            var player = await _context.Players.FirstOrDefaultAsync(s => s.Username == username);
            return player?.Token;
        }

        private async Task<string?> GetPlayersName(string token)
        {
            var player = await _context.Players.FirstOrDefaultAsync(s => s.Token == token);
            return player?.Username;
        }

        private async Task<List<GameResult>> GetMatchHistory(string player_token)
        {
            return await _context.Results
                .Where(s => s.Winner == player_token || s.Loser == player_token)
                .ToListAsync();
        }
    }
}
