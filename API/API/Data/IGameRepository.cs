using API.Models;

namespace API.Data
{
    public interface IGameRepository
    {
        bool Create(GameCreation game);
        void Create(Game game);
        List<GameDescription>? GetPendingGames();
        string? GetGameTokenByPlayersToken(string player_token);
        Color? GetPlayersTurnByPlayersToken(string player_token);
        Status? GetStatusByPlayersToken(string player_token);
        Color[,]? GetBoardByPlayersToken(string player_token);
        bool JoinPlayer(GameEntrant entry);
        (bool succeded, string? error) Move(GameStep action);
        bool Pass(string player_token, out string error_message);
        bool Forfeit(string player_token);
        bool Delete(string player_token);
    }
}
