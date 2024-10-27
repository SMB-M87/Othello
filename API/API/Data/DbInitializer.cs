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
                    new(Inquiry.Friend, "Lambert")
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
                    new(Inquiry.Friend, "Briar")
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
                    new(Inquiry.Friend, "Lambert"),
                    new(Inquiry.Friend, "Briar")
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
                    new(Inquiry.Friend, "Briar")
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
                    new(Inquiry.Friend, "Briar")
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
                    new(Inquiry.Friend, "Briar")
                }
            };
            Player nine = new("briar", "Briar")
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
            Player ten = new("lambert", "Lambert")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Eltjo", "Tijn" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Burton"),
                    new(Inquiry.Friend, "Briar")
                }
            };
            Player eleven = new("identity", "Identity")
            {
                LastActivity = DateTime.UtcNow,
                Friends = { "Eltjo", "Tijn" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "Burton"),
                    new(Inquiry.Friend, "Lambert"),
                    new(Inquiry.Friend, "Cena"),
                    new(Inquiry.Friend, "Burst"),
                    new(Inquiry.Friend, "Ernst"),
                    new(Inquiry.Friend, "Karen"),
                    new(Inquiry.Friend, "Briar")
                }
            };
            Player t12 = new("bert", "Bert")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t13 = new("manolo", "Manolo")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t14 = new("gijs", "Gijs")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t15 = new("hidde", "Hidde")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t16 = new("nora", "Nora")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t17 = new("lea", "Léa")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t18 = new("dasha", "Dasha")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t19 = new("adrianna", "Adrianna")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t20 = new("nadege", "Nadege")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t21 = new("macron", "Macron")
            {
                LastActivity = DateTime.UtcNow
            };

            _builder.Entity<Player>().HasData(one, two, three, four, five, six, seven, eight, nine, ten, eleven, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21);

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

            Game game3 = new("three", six.Token, Color.Black, null, Status.Pending, Color.White, "I search an advanced player!");
            Game game4 = new("four", seven.Token, Color.Black, null, Status.Pending, Color.White, "میں ایک کھیل کھیلنا چاہتا ہوں اور کوئی ضرورت نہیں ہے!");
            Game game5 = new("six", eight.Token, Color.Black, null, Status.Pending, Color.White, "Θέλω να παίξω ένα παιχνίδι και δεν έχω απαιτήσεις!!!");
            Game g6 = new("seven", nine.Token, Color.Black, null, Status.Pending, Color.White, "Je veux jouer une partie contre un élite!!!");
            Game g7 = new("eight", nine.Token, Color.Black, null, Status.Pending, Color.White, "أريد أن ألعب لعبة وليس لدي أي متطلبات!");
            Game g8 = new("nine", ten.Token, Color.Black, null, Status.Pending, Color.White, "I search an advanced player to play more than one game against!");
            Game g9 = new("ten", t12.Token, Color.Black, null, Status.Pending, Color.White, "אני רוצה לשחק משחק ואין לי שום דרישות!");
            Game g10 = new("t11", t13.Token, Color.Black, null, Status.Pending, Color.White, "I want to player more than one game against the same adversary.");

            _builder.Entity<Game>().HasData(game0, game1, game2, game3, game4, game5, g6, g7, g8, g9, g10);

            GameResult gameResult0 = new("2", four.Token, five.Token, game2.Board);
            GameResult gameResult1 = new("1", five.Token, four.Token, game2.Board);
            GameResult gameResult2 = new("0", five.Token, four.Token, game2.Board);

            GameResult gameResult00 = new("-2", four.Token, five.Token, game2.Board);
            GameResult gameResult011 = new("-1", five.Token, four.Token, game2.Board);
            GameResult gameResult022 = new("-0", five.Token, four.Token, game2.Board);

            GameResult gameResult3 = new("-3", one.Token, two.Token, game2.Board);
            GameResult gameResult4 = new("-4", three.Token, one.Token, game2.Board);
            GameResult gameResult5 = new("-5", two.Token, three.Token, game2.Board);

            GameResult gameResult6 = new("-6", six.Token, one.Token, game2.Board);
            GameResult gameResult7 = new("-7", one.Token, seven.Token, game2.Board);
            GameResult gameResult8 = new("-8", two.Token, six.Token, game2.Board);

            GameResult gameResult9 = new("-9", five.Token, four.Token, game2.Board);
            GameResult gameResult10 = new("-10", four.Token, three.Token, game2.Board);
            GameResult gameResult11 = new("-11", three.Token, two.Token, game2.Board);

            GameResult gameResult12 = new("-12", six.Token, three.Token, game2.Board);
            GameResult gameResult13 = new("-13", seven.Token, five.Token, game2.Board);
            GameResult gameResult14 = new("-14", five.Token, one.Token, game2.Board);

            GameResult gameResult15 = new("-15", eight.Token, two.Token, game2.Board);
            GameResult gameResult16 = new("-16", eight.Token, three.Token, game2.Board);
            GameResult gameResult17 = new("-17", six.Token, two.Token, game2.Board);

            GameResult gameResult18 = new("-18", four.Token, one.Token, game2.Board);
            GameResult gameResult19 = new("-19", seven.Token, six.Token, game2.Board);
            GameResult gameResult20 = new("-20", two.Token, eight.Token, game2.Board);

            GameResult gameResult21 = new("-21", five.Token, three.Token, game2.Board);
            GameResult gameResult22 = new("-22", one.Token, four.Token, game2.Board);
            GameResult gameResult23 = new("-23", three.Token, seven.Token, game2.Board);

            GameResult gameResult24 = new("-24", six.Token, five.Token, game2.Board);
            GameResult gameResult25 = new("-25", one.Token, eight.Token, game2.Board);
            GameResult gameResult26 = new("-26", seven.Token, four.Token, game2.Board);

            GameResult gameResult27 = new("-27", t12.Token, t13.Token, game2.Board, draw: true);
            GameResult gameResult28 = new("-28", t13.Token, t14.Token, game2.Board, draw: true);
            GameResult gameResult29 = new("-29", t14.Token, t15.Token, game2.Board);
            GameResult gameResult30 = new("-30", t15.Token, t16.Token, game2.Board);
            GameResult gameResult31 = new("-31", t16.Token, t17.Token, game2.Board, draw: true);
            GameResult gameResult32 = new("-32", t17.Token, t18.Token, game2.Board, draw: true);

            GameResult gameResult33 = new("-33", t18.Token, t19.Token, game2.Board);
            GameResult gameResult34 = new("-34", t19.Token, t20.Token, game2.Board);
            GameResult gameResult35 = new("-35", t20.Token, t21.Token, game2.Board, draw: true);
            GameResult gameResult36 = new("-36", t21.Token, t12.Token, game2.Board, draw: true);
            GameResult gameResult37 = new("-37", t13.Token, t15.Token, game2.Board);
            GameResult gameResult38 = new("-38", t16.Token, t18.Token, game2.Board);

            GameResult gameResult39 = new("-39", t17.Token, t19.Token, game2.Board);
            GameResult gameResult40 = new("-40", t18.Token, t21.Token, game2.Board);
            GameResult gameResult41 = new("-41", t19.Token, t13.Token, game2.Board, draw: true);
            GameResult gameResult42 = new("-42", t14.Token, t17.Token, game2.Board);
            GameResult gameResult43 = new("-43", t15.Token, t19.Token, game2.Board);
            GameResult gameResult44 = new("-44", t21.Token, t16.Token, game2.Board);

            GameResult gameResult45 = new("-45", one.Token, t12.Token, game2.Board);
            GameResult gameResult46 = new("-46", two.Token, t13.Token, game2.Board, draw: true);
            GameResult gameResult47 = new("-47", three.Token, t14.Token, game2.Board);
            GameResult gameResult48 = new("-48", four.Token, t15.Token, game2.Board, draw: true);
            GameResult gameResult49 = new("-49", five.Token, t16.Token, game2.Board);
            GameResult gameResult50 = new("-50", six.Token, t17.Token, game2.Board);

            GameResult gameResult51 = new("-51", seven.Token, t18.Token, game2.Board, draw: true);
            GameResult gameResult52 = new("-52", eight.Token, t19.Token, game2.Board);
            GameResult gameResult53 = new("-53", nine.Token, t20.Token, game2.Board);
            GameResult gameResult54 = new("-54", ten.Token, t21.Token, game2.Board, draw: true);
            GameResult gameResult55 = new("-55", eleven.Token, t12.Token, game2.Board);
            GameResult gameResult56 = new("-56", t13.Token, two.Token, game2.Board);

            GameResult gameResult57 = new("-57", t14.Token, three.Token, game2.Board, draw: true);
            GameResult gameResult58 = new("-58", t15.Token, five.Token, game2.Board);
            GameResult gameResult59 = new("-59", t16.Token, seven.Token, game2.Board);
            GameResult gameResult60 = new("-60", t17.Token, eight.Token, game2.Board);
            GameResult gameResult61 = new("-61", t18.Token, nine.Token, game2.Board, draw: true);
            GameResult gameResult62 = new("-62", t19.Token, eleven.Token, game2.Board);

            GameResult gameResult63 = new("-63", t20.Token, ten.Token, game2.Board, draw: true);
            GameResult gameResult64 = new("-64", t21.Token, three.Token, game2.Board);
            GameResult gameResult65 = new("-65", one.Token, six.Token, game2.Board, draw: true);
            GameResult gameResult66 = new("-66", seven.Token, five.Token, game2.Board);
            GameResult gameResult67 = new("-67", two.Token, eight.Token, game2.Board);
            GameResult gameResult68 = new("-68", three.Token, ten.Token, game2.Board, draw: true);

            GameResult gameResult69 = new("-69", eleven.Token, t14.Token, game2.Board);
            GameResult gameResult70 = new("-70", t12.Token, two.Token, game2.Board, draw: true);
            GameResult gameResult71 = new("-71", t13.Token, three.Token, game2.Board);
            GameResult gameResult72 = new("-72", t15.Token, four.Token, game2.Board);
            GameResult gameResult73 = new("-73", t17.Token, seven.Token, game2.Board, draw: true);
            GameResult gameResult74 = new("-74", t20.Token, nine.Token, game2.Board);

            GameResult gameResult75 = new("-75", t16.Token, five.Token, game2.Board);
            GameResult gameResult76 = new("-76", t18.Token, ten.Token, game2.Board);
            GameResult gameResult77 = new("-77", t19.Token, six.Token, game2.Board, draw: true);
            GameResult gameResult78 = new("-78", t21.Token, eight.Token, game2.Board);
            GameResult gameResult79 = new("-79", five.Token, t16.Token, game2.Board, draw: true);
            GameResult gameResult80 = new("-80", eight.Token, t18.Token, game2.Board);

            _builder.Entity<GameResult>().HasData(gameResult00, gameResult011,
                gameResult0, gameResult1, gameResult2, gameResult3, gameResult4,
                gameResult5, gameResult6, gameResult7, gameResult8, gameResult9,
                gameResult10, gameResult11, gameResult12, gameResult13, gameResult14, gameResult022,
                gameResult15, gameResult16, gameResult17, gameResult18, gameResult19, gameResult32,
                gameResult20, gameResult21, gameResult22, gameResult23, gameResult24, gameResult31,
                gameResult25, gameResult26, gameResult27, gameResult28, gameResult29, gameResult30,
                gameResult33, gameResult34, gameResult35, gameResult36, gameResult37, gameResult38,
                gameResult39, gameResult40, gameResult41, gameResult42, gameResult43, gameResult44,
                gameResult45, gameResult46, gameResult47, gameResult48, gameResult49, gameResult50,
                gameResult51, gameResult52, gameResult53, gameResult54, gameResult55, gameResult56,
                gameResult57, gameResult58, gameResult59, gameResult60, gameResult61, gameResult62,
                gameResult63, gameResult64, gameResult65, gameResult66, gameResult67, gameResult68,
                gameResult69, gameResult70, gameResult71, gameResult72, gameResult73, gameResult74,
                gameResult75, gameResult76, gameResult77, gameResult78, gameResult79, gameResult80
            );
        }
    }
}