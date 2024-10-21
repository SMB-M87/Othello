using API.Models;

namespace API.Data
{
    public class GameAccessLayer : IGameRepository
    {
        private readonly Database _context;

        public GameAccessLayer(Database context)
        {
            _context = context;     
        }

        public void Create(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public void Join(GameEntrant entrant)
        {
            var game = Get(entrant.Token);

            if (game is not null)
            {
                game.SetSecondPlayer(entrant.Player);
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.Second).IsModified = true;
                _context.SaveChanges();
            }
        }

        public void JoinPlayer(GameEntrant entrant)
        {
            var game = GetPlayersGame(entrant.Token);

            if (game is not null)
            {
                game.SetSecondPlayer(entrant.Player);
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.Second).IsModified = true;
                _context.SaveChanges();
            }
        }

        public void Update(Game game)
        {
            var update = Get(game.Token);

            if (update is not null)
            {
                _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                update.Board = game.Board;
                _context.Entry(update).Property(g => g.Board).IsModified = true;
                _context.SaveChanges();
            }
        }

        public void Finish(Game game)
        {
            var update = Get(game.Token);

            if (update is not null)
            {
                game.Finish();
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.PlayersTurn).IsModified = true;
                _context.SaveChanges();
            }
        }

        public void Delete(Game game)
        {
            var remove = Get(game.Token);

            if (remove is not null)
            {
                _context.Games.Remove(remove);
                _context.SaveChanges();
            }
        }

        public Game? Get(string token)
        {
            return _context.Games.FirstOrDefault(s => s.Token.Equals(token));
        }

        public Game? GetPlayersGame(string player)
        {
            var games = GetGames();
            var game = games!.FirstOrDefault(s => s.First.Equals(player) && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second.Equals(player) && s.Status != Status.Finished);

            return game;
        }

        public List<Game>? GetGames()
        {
            return _context.Games.ToList();
        }
    }
}
