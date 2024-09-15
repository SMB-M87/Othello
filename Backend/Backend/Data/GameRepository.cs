using Backend.Models;

namespace Backend.Data
{
    public class GameRepository : IGameRepository
    {
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }

        public GameRepository()
        {
            Player one = new("One") { Token = "First" };
            Game game0 = new(one, "I wanna play a game and don't have any requirements.")
            {
                Token = "0"
            };
            game0.First.Color = Color.Black;
            one.InGame = true;

            Player two = new("Two") { Token = "Second" };
            Player three = new("Three") { Token = "Third" };
            Game game1 = new(two, "I search an advanced player!")
            {
                Token = "1"
            };
            game1.First.Color = Color.Black;
            two.InGame = true;
            game1.Second = new(three.Token)
            {
                Color = Color.White
            };
            three.InGame = true;
            game1.Status = Status.Playing;

            Player four = new("Four") { Token = "Fourth" };
            Game game2 = new(four, "I want to player more than one game against the same adversary.")
            {
                Token = "2"
            };
            game2.First.Color = Color.Black;
            four.InGame = true;

            Games = new List<Game> { game0, game1, game2 };
            Players = new List<Player> { one, two, three, four };
        }

        public List<Game> GetGames()
        {
            return Games;
        }

        public Game? GetGame(string token)
        {
            Game? game = Games.Find(s => s.Token.Equals(token, StringComparison.Ordinal));

            return game;
        }

        public Game? GetPlayersGame(string token)
        {
            var game = Games.Find(s => s.First.Token.Equals(token));
            game ??= Games.Find(s => s.Second.Token.Equals(token));

            return game;
        }

        public void AddPlayer(Player player)
        {

        }

        public void AddGame(Game game)
        {
            Games.Add(game);
        }

        public void AddResult(GameResult result)
        {

        }

        public void JoinGame(GameEntrant entry)
        {
            int index = Games.FindIndex(s => s.Token.Equals(entry.Token));
            if (entry.Player.Token != Games[index].First.Token)
            {
                Games[index].Second.Token = entry.Player.Token;
                Games[index].Status = Status.Playing;
                entry.Player.InGame = true;
            }
        }

        public void UpdatePlayer(Player player)
        {

        }

        public void UpdateGame(Game game)
        {

        }

        public void DeleteGame(Game game)
        {
            Games.Remove(game);
        }
    }
}
