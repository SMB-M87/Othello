using API.Models;

namespace API.Data
{
    public interface IGameRepository
    {
        Game? Get(string token);
        Game? GetPlayersGame(string player_token);
        Status? GetStatusByPlayersToken(string player_token);
        List<Game> GetGames();
        GamePlay GetView(string player_token);
        GamePartial GetPartial(string player_token);

        bool Create(GameCreation game);
        void Create(Game game);
        bool JoinPlayer(PlayerRequest entry);
        (bool succeded, string? error) Move(GameMove action);
        bool Pass(string player_token, out string error_message);
        bool Forfeit(string player_token);
        bool Delete(string player_token);
        bool PermDelete(string token);
    }
}
