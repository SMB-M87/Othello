using Backend.Models;

namespace Backend.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IRepository _repository;

        public GameRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void AddGame(Game game)
        {
            _repository.Games().Add(game);
        }

        public void JoinGame(GameEntrant entry)
        {
            int index = _repository.Games().FindIndex(s => s.Token.Equals(entry.Token));

            if (index != -1)
            {
                _repository.Games()[index].Second.Token = entry.Player.Token;
                _repository.Games()[index].Status = Status.Playing;
            }
        }

        public void JoinPlayer(GameEntrant entry)
        {
            int index = _repository.Games().FindIndex(s => s.First.Token.Equals(entry.Token));

            if (index != -1)
            {
                _repository.Games()[index].Second.Token = entry.Player.Token;
                _repository.Games()[index].Status = Status.Playing;
            }
        }

        public void UpdateGame(Game game)
        {
            int index = _repository.Games().FindIndex(s => s.Token.Equals(game.Token));

            if (index != -1)
                _repository.Games()[index] = game;
        }

        public void DeleteGame(Game game)
        {
            _repository.Games().Remove(game);
        }

        public Game? GetGame(string token)
        {
            return _repository.Games().Find(s => s.Token.Equals(token, StringComparison.Ordinal));
        }

        public Game? GetPlayersGame(string token)
        {
            return  _repository.Games().Find(s => s.First.Token.Equals(token)) ??
                    _repository.Games().Find(s => s.Second.Token.Equals(token));
        }

        public List<Game> GetGames()
        {
            return _repository.Games();
        }
    }
}
