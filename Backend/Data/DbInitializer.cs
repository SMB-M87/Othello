using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            Player one = new("Karen") { Token = "karen" };
            Player two = new("Ersnt") { Token = "ernst" };
            Player three = new("John") { Token = "john" };
            Player four = new("Eltjo") { Token = " eltjo" };
            Player five = new("Tijn") { Token = "tijn" };

            _builder.Entity<Player>().HasData(one, two, three, four, five);

            Game game0 = new(one.Token, "I wanna play a game and don't have any requirements.")
            {
                Token = "zero"
            };

            Game game1 = new(two.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = three.Token,
                SColor = Color.White,
                Status = Status.Playing
            };

            Game game2 = new(four.Token, "I want to player more than one game against the same adversary.")
            {
                Token = "two",
                FColor = Color.Black,
                Second = five.Token,
                SColor = Color.White,
                PlayersTurn = Color.White
            };
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

            _builder.Entity<Game>().HasData(game0, game1, game2);

            GameResult gameResult0 = new("-2", game2.First, game2.Second);
            GameResult gameResult1 = new("-1", game2.Second, game2.First);
            GameResult gameResult2 = new(game2.Token, game2.Second, game2.First);

            _builder.Entity<GameResult>().HasData(gameResult0, gameResult1, gameResult2);
        }
    }
}