/*using Backend.Models;

namespace Backend.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly IRepository _repository;

        public GameRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void Create(Game game)
        {
            _repository.Games().Add(game);
        }

        public void Join(GameEntrant entry)
        {
            int index = _repository.Games().FindIndex(s => s.Token.Equals(entry.Token));

            if (index != -1)
            {
                _repository.Games()[index].Second = entry.Player;
                _repository.Games()[index].Status = Status.Playing;
            }
        }

        public void JoinPlayer(GameEntrant entry)
        {
            int index = _repository.Games().FindIndex(s => s.First.Equals(entry.Token));

            if (index != -1)
            {
                _repository.Games()[index].Second = entry.Player;
                _repository.Games()[index].Status = Status.Playing;
            }
        }

        public void Update(Game game)
        {
            int index = _repository.Games().FindIndex(s => s.Token.Equals(game.Token));

            if (index != -1)
                _repository.Games()[index] = game;
        }

        public void Delete(Game game)
        {
            _repository.Games().Remove(game);
        }

        public Game? Get(string token)
        {
            return _repository.Games().Find(s => s.Token.Equals(token, StringComparison.Ordinal));
        }

        public Game? GetPlayersGame(string token)
        {
            return _repository.Games().Find(s => s.First.Equals(token)) ??
                    _repository.Games().Find(s => s.Second.Equals(token));
        }

        public List<Game> GetGames()
        {
            return _repository.Games();
        }
    }
}
*/