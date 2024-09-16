using Backend.Models;

namespace Backend.Repositories
{
    public class Repository : IRepository
    {
        private List<Game> _games;
        private List<GameResult> _results;
        private List<Player> _players;

        public Repository()
        {
            _games = new List<Game>();
            _results = new List<GameResult>();
            _players = new List<Player>();
        }

        public IGameRepository GameRepository
        {
            get { return new GameRepository(this); }
        }

        public List<Game> Games() { return _games; }

        public IResultRepository ResultRepository
        {
            get { return new ResultRepository(this); }
        }

        public List<GameResult> Results() { return _results; }

        public IPlayerRepository PlayerRepository
        {
            get { return new PlayerRepository(this); }
        }

        public List<Player> Players() { return _players; }
    }
}
