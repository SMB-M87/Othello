/*using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class ReversiDbInitializer
    {
        private readonly ModelBuilder _builder;

        public ReversiDbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            Player one = new() { Token = "first" };
            Player two = new() { Token = "second" };
            Player three = new() { Token = "third" };
            Player four = new() { Token = "fourth" };
            Player five = new() { Token = "fifth" };

            Game game0 = new Game(one, "I wanna play a game and don't have any requirements.");
            game0.Token = "zero";
            game0.First.Color = Color.Black;
            one.InGame = true;
            game0.Second.Color = Color.White;

            Game game1 = new Game(two, "I search an advanced player!");
            game1.Token = "one";
            game1.First.Color = Color.Black;
            two.InGame = true;
            game1.Second = new(three.Token);
            game1.Second.Color = Color.White;
            three.InGame = true;
            game1.Status = Status.Playing;

            Game game2 = new Game(four, "I want to player more than one game against the same adversary.");
            game2.Token = "two";
            game2.First.Color = Color.Black;
            four.InGame = true;
            game2.Second = new(five.Token);
            game2.Second.Color = Color.White;
            five.InGame = true;
            game2.PlayersTurn = Color.White;
            game2.Board[0, 0] = Color.White;
            game2.Board[0, 1] = Color.White;
            game2.Board[0, 2] = Color.White;
            game2.Board[0, 3] = Color.White;
            game2.Board[0, 4] = Color.White;
            game2.Board[0, 5] = Color.White;
            game2.Board[0, 6] = Color.White;
            game2.Board[0, 7] = Color.White;
            game2.Board[1, 0] = Color.White;
            game2.Board[1, 1] = Color.White;
            game2.Board[1, 2] = Color.White;
            game2.Board[1, 3] = Color.White;
            game2.Board[1, 4] = Color.White;
            game2.Board[1, 5] = Color.White;
            game2.Board[1, 6] = Color.White;
            game2.Board[1, 7] = Color.White;
            game2.Board[2, 0] = Color.White;
            game2.Board[2, 1] = Color.White;
            game2.Board[2, 2] = Color.White;
            game2.Board[2, 3] = Color.White;
            game2.Board[2, 4] = Color.White;
            game2.Board[2, 5] = Color.White;
            game2.Board[2, 6] = Color.White;
            game2.Board[2, 7] = Color.White;
            game2.Board[3, 0] = Color.White;
            game2.Board[3, 1] = Color.White;
            game2.Board[3, 2] = Color.White;
            game2.Board[3, 3] = Color.White;
            game2.Board[3, 4] = Color.White;
            game2.Board[3, 5] = Color.White;
            game2.Board[3, 6] = Color.White;
            game2.Board[3, 7] = Color.White;
            game2.Board[4, 0] = Color.White;
            game2.Board[4, 1] = Color.White;
            game2.Board[4, 2] = Color.White;
            game2.Board[4, 3] = Color.White;
            game2.Board[4, 4] = Color.White;
            game2.Board[4, 5] = Color.White;
            game2.Board[4, 6] = Color.Black;
            game2.Board[4, 7] = Color.Black;
            game2.Board[5, 0] = Color.White;
            game2.Board[5, 1] = Color.White;
            game2.Board[5, 2] = Color.White;
            game2.Board[5, 3] = Color.White;
            game2.Board[5, 4] = Color.White;
            game2.Board[5, 5] = Color.White;
            game2.Board[5, 6] = Color.Black;
            game2.Board[5, 7] = Color.Black;
            game2.Board[6, 0] = Color.White;
            game2.Board[6, 1] = Color.White;
            game2.Board[6, 2] = Color.White;
            game2.Board[6, 3] = Color.White;
            game2.Board[6, 4] = Color.White;
            game2.Board[6, 5] = Color.White;
            game2.Board[6, 6] = Color.White;
            game2.Board[6, 7] = Color.Black;
            game2.Board[7, 0] = Color.White;
            game2.Board[7, 1] = Color.White;
            game2.Board[7, 2] = Color.White;
            game2.Board[7, 3] = Color.White;
            game2.Board[7, 4] = Color.White;
            game2.Board[7, 5] = Color.White;
            game2.Board[7, 6] = Color.White;
            game2.Board[7, 7] = Color.White;
            game2.Status = Status.Finished;
            game2.PlayersTurn = Color.None;
            four.InGame = false;
            five.InGame = false;

            GameResult gameResult = new GameResult(game2.Token, game2.Second.Token, game2.First.Token, game2.Board);
            four.Lost++;
            five.Won++;


            _builder.Entity<Player>().HasData(one);
            _builder.Entity<Player>().HasData(two);
            _builder.Entity<Player>().HasData(three);
            _builder.Entity<Player>().HasData(four);
            _builder.Entity<Player>().HasData(five);

            _builder.Entity<Game>().HasData(game0);
            _builder.Entity<Game>().HasData(game1);
            _builder.Entity<Game>().HasData(game2);

            _builder.Entity<GameResult>().HasData(gameResult);
        }
    }
}
*/