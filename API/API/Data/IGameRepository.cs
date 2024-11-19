using API.Models;

namespace API.Data
{
    public interface IGameRepository
    {
        Task<Game?> Get(string token);
        Task<Game?> GetPlayersGame(string player_token);
        Task<Status?> GetStatusByPlayersToken(string player_token);
        Task<List<Game>> GetGames();
        Task<GamePlay> GetView(string player_token);
        Task<GamePartial> GetPartial(string player_token);

        Task<bool> Create(GameCreation game);
        Task Create(Game game);
        Task<bool> JoinPlayer(PlayerRequest entry);
        Task<(bool succeded, string? error)> Move(GameMove action);
        Task<(bool succeded, string? error)> Pass(string player_token);
        Task<bool> Forfeit(string player_token);
        Task<bool> Delete(string player_token);
        Task<bool> PermDelete(string token);
    }
}
