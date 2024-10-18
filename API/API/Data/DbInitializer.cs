using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
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
            Player one = new("Karen") { Token = "karen", Friends = { "Ernst", "John" } };
            Player two = new("Ernst") { Token = "ernst", Friends = { "John", "Karen" } };
            Player three = new("John") { Token = "john", Friends = { "Ernst", "Karen" } };
            Player four = new("Eltjo") { Token = "eltjo", Friends = { "Tijn" }, PendingFriends = { "Karen", "Ernst", "John" } };
            Player five = new("Tijn") { Token = "tijn", Friends = { "Eltjo" }, PendingFriends = { "Karen", "Ernst", "John" } };

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
                PlayersTurn = Color.White,
                Status = Status.Playing
            };
            game2.Board[0, 0] = Color.Black;
            game2.Board[1, 0] = Color.Black;
            game2.Board[0, 1] = Color.White;
            game2.Board[0, 2] = Color.White;
            game2.Board[3, 3] = Color.None;
            game2.Board[3, 4] = Color.None;
            game2.Board[4, 4] = Color.None;
            game2.Board[4, 3] = Color.None;

            _builder.Entity<Game>().HasData(game0, game1, game2);

            GameResult gameResult0 = new("-2", game2.First, game2.Second);
            GameResult gameResult1 = new("-1", game2.Second, game2.First);
            GameResult gameResult2 = new("-0", game2.Second, game2.First);

            _builder.Entity<GameResult>().HasData(gameResult0, gameResult1, gameResult2);
        }
    }
}