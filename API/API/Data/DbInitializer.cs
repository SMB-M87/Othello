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
            Player one = new("karen", "Karen")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Ernst", "John" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Tijn"),
                    new(Inquiry.Friend, "Eltjo"),
                    new(Inquiry.Friend, "Badbux")
                }
            };
            Player two = new("ernst", "Ernst")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "John", "Karen" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Burton"),
                    new(Inquiry.Friend, "Burst"),
                    new(Inquiry.Friend, "Eltjo")
                }
            };
            Player three = new("john", "John")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Ernst", "Karen" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Tijn"),
                    new(Inquiry.Friend, "Eltjo"),
                    new(Inquiry.Friend, "Sadbux")
                }
            };
            Player four = new("eltjo", "Eltjo")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Tijn" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Karen"),
                    new(Inquiry.Friend, "Ernst"),
                    new(Inquiry.Friend, "John")
                }
            };
            Player five = new("tijn", "Tijn")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Eltjo" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Karen"),
                    new(Inquiry.Friend, "Badbux"),
                    new(Inquiry.Friend, "Sadbux")
                }
            };
            Player six = new("cena", "Cena")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "John", "Karen" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Eltjo"),
                    new(Inquiry.Friend, "Tijn"),
                    new(Inquiry.Friend, "Sadbux")
                }
            };
            Player seven = new("burst", "Burst")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Cena", "Karen" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Eltjo"),
                    new(Inquiry.Friend, "Tijn"),
                    new(Inquiry.Friend, "Sadbux")
                }
            };
            Player eight = new("burton", "Burton")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Ernst", "Karen" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Eltjo"),
                    new(Inquiry.Friend, "Tijn"),
                    new(Inquiry.Friend, "Sadbux")
                }
            };
            Player nine = new("sadbux", "Sadbux")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Eltjo", "Tijn" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Cena"),
                    new(Inquiry.Friend, "Burton"),
                    new(Inquiry.Friend, "Ernst")
                }
            };
            Player ten = new("badbux", "Badbux")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Eltjo", "Tijn" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Burton"),
                    new(Inquiry.Friend, "Badbux"),
                    new(Inquiry.Friend, "Sadbux")
                }
            };
            Player eleven = new("identity", "Identity")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Eltjo", "Tijn" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Burton"),
                    new(Inquiry.Friend, "Badbux"),
                    new(Inquiry.Friend, "Cena"),
                    new(Inquiry.Friend, "Burst"),
                    new(Inquiry.Friend, "Ernst"),
                    new(Inquiry.Friend, "Karen"),
                    new(Inquiry.Friend, "Sadbux")
                }
            };

            _builder.Entity<Player>().HasData(one, two, three, four, five, six, seven, eight, nine, ten, eleven);

            Game game0 = new("zero", one.Token, Color.Black);

            Game game1 = new("one", two.Token, Color.Black, three.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game2 = new("two", four.Token, Color.Black, five.Token, Status.Playing, Color.White, "I want to player more than one game against the same adversary.");
            game2.Board[0, 0] = Color.Black;
            game2.Board[1, 0] = Color.Black;
            game2.Board[0, 1] = Color.White;
            game2.Board[0, 2] = Color.White;
            game2.Board[3, 3] = Color.None;
            game2.Board[3, 4] = Color.None;
            game2.Board[4, 4] = Color.None;
            game2.Board[4, 3] = Color.None;

            Game game3 = new("three", six.Token, Color.Black);

            Game game4 = new("four", seven.Token, Color.Black);

            _builder.Entity<Game>().HasData(game0, game1, game2, game3, game4);

            GameResult gameResult0 = new("2", four.Token, five.Token);
            GameResult gameResult1 = new("1", five.Token, four.Token);
            GameResult gameResult2 = new("0", five.Token, four.Token);

            GameResult gameResult00 = new("-2", four.Token, five.Token);
            GameResult gameResult011 = new("-1", five.Token, four.Token);
            GameResult gameResult022 = new("-0", five.Token, four.Token);

            GameResult gameResult3 = new("-3", one.Token, two.Token);
            GameResult gameResult4 = new("-4", three.Token, one.Token);
            GameResult gameResult5 = new("-5", two.Token, three.Token);

            GameResult gameResult6 = new("-6", six.Token, one.Token);
            GameResult gameResult7 = new("-7", one.Token, seven.Token);
            GameResult gameResult8 = new("-8", two.Token, six.Token);

            GameResult gameResult9 = new("-9", five.Token, four.Token);
            GameResult gameResult10 = new("-10", four.Token, three.Token);
            GameResult gameResult11 = new("-11", three.Token, two.Token);

            GameResult gameResult12 = new("-12", six.Token, three.Token);
            GameResult gameResult13 = new("-13", seven.Token, five.Token);
            GameResult gameResult14 = new("-14", five.Token, one.Token);

            GameResult gameResult15 = new("-15", eight.Token, two.Token);
            GameResult gameResult16 = new("-16", eight.Token, three.Token);
            GameResult gameResult17 = new("-17", six.Token, two.Token);

            GameResult gameResult18 = new("-18", four.Token, one.Token);
            GameResult gameResult19 = new("-19", seven.Token, six.Token);
            GameResult gameResult20 = new("-20", two.Token, eight.Token);

            GameResult gameResult21 = new("-21", five.Token, three.Token);
            GameResult gameResult22 = new("-22", one.Token, four.Token);
            GameResult gameResult23 = new("-23", three.Token, seven.Token);

            GameResult gameResult24 = new("-24", six.Token, five.Token);
            GameResult gameResult25 = new("-25", one.Token, eight.Token);
            GameResult gameResult26 = new("-26", seven.Token, four.Token);

            _builder.Entity<GameResult>().HasData(gameResult00, gameResult011, gameResult022,
                gameResult0, gameResult1, gameResult2, gameResult3, gameResult4,
                gameResult5, gameResult6, gameResult7, gameResult8, gameResult9,
                gameResult10, gameResult11, gameResult12, gameResult13, gameResult14,
                gameResult15, gameResult16, gameResult17, gameResult18, gameResult19,
                gameResult20, gameResult21, gameResult22, gameResult23, gameResult24,
                gameResult25, gameResult26
            );
        }
    }
}