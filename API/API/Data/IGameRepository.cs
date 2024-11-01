using API.Models;

namespace API.Data
{
    public interface IGameRepository
    {
        bool Create(GameCreation game);
        void Create(Game game);
        List<GamePending>? GetPendingGames();
        string? GetGameTokenByPlayersToken(string player_token);
        string? GetOpponentByPlayersToken(string player_token);
        Color? GetPlayersTurnByPlayersToken(string player_token);
        Status? GetStatusByPlayersToken(string player_token);
        Color? GetColorByPlayersToken(string player_token);
        Color[,]? GetBoardByPlayersToken(string player_token);
        string? GetTimerByPlayersToken(string player_token);
        List<Game> GetGames();
        bool JoinPlayer(PlayerRequest entry);
        (bool succeded, string? error) Move(GameMove action);
        bool Pass(string player_token, out string error_message);
        bool Forfeit(string player_token);
        bool Delete(string player_token);
        bool PermDelete(string token);
    }
}
