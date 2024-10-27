using API.Models;

namespace API.Data
{
    public interface IResultRepository
    {
        bool Create(GameResult result);
        string? GetPlayerStats(string player_token);
        List<GameResultView>? GetPlayersMatchHistory(string player_token);
    }
}
