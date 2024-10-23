using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace APITest.GameTest
{
    [TestFixture]
    public class RepositoryTest
    {
        private Database _context;
        private IRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new Database(options);
            _repository = new Repository(_context);

            Player one = new("first", "one");
            Player two = new("second", "two");
            Player three = new("third", "three");
            Player four = new("fourth", "four");
            Player five = new("fifth", "five");
            Player six = new("sixth", "six");
            Player seven = new("seven", "seven");
            Player eight = new("eight", "eight");
            Player nine = new("nine", "nine");
            Player ten = new("12", "12");
            Player eleven = new("13", "13");

            _repository.PlayerRepository.Create(one);
            _repository.PlayerRepository.Create(two);
            _repository.PlayerRepository.Create(three);
            _repository.PlayerRepository.Create(four);
            _repository.PlayerRepository.Create(five);
            _repository.PlayerRepository.Create(six);
            _repository.PlayerRepository.Create(seven);
            _repository.PlayerRepository.Create(eight);
            _repository.PlayerRepository.Create(nine);
            _repository.PlayerRepository.Create(ten);
            _repository.PlayerRepository.Create(eleven);

            Game game = new("null", ten.Token, Color.Black, eleven.Token, Status.Playing, Color.Black);

            Game game0 = new("zero", one.Token);

            Game game1 = new("one", two.Token, Color.Black, three.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game2 = new("two", four.Token, Color.Black);

            Game game3 = new("three", "nonexistant");

            Game game4 = new("four", six.Token, Color.Black, seven.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game5 = new("five", eight.Token, Color.Black, nine.Token, Status.Pending, Color.White, "I search an advanced player!");

            Game game6 = new("six", eight.Token, Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game7 = new("seven", "nonexistant", Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game8 = new("eight", nine.Token, Color.Black, "nonexistant", Status.Playing, Color.White, "I search an advanced player!");

            Game game9 = new("nine", eight.Token, Color.Black, nine.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game10 = new("ten", eight.Token, Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            _repository.GameRepository.Create(game0);
            _repository.GameRepository.Create(game1);
            _repository.GameRepository.Create(game2);
            _repository.GameRepository.Create(game3);
            _repository.GameRepository.Create(game4);
            _repository.GameRepository.Create(game5);
            _repository.GameRepository.Create(game6);
            _repository.GameRepository.Create(game7);
            _repository.GameRepository.Create(game8);
            _repository.GameRepository.Create(game);
            _repository.GameRepository.Create(game9);
            _repository.GameRepository.Create(game10);

            GameResult result0 = new("-3", "second", "third");
            GameResult result1 = new("-2", "third", "second");
            GameResult result2 = new("-1", "second", "third");

            _repository.ResultRepository.Create(result0);
            _repository.ResultRepository.Create(result1);
            _repository.ResultRepository.Create(result2);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void Create_Correct()
        {
            int size = _context.Games.Count();
            Player five = new("fifth", "five");
            Game game = new("three", five.Token, Color.Black);

            _repository.GameRepository.Create(game);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Games.Count(), Is.Not.EqualTo(expected: size));
                Assert.That(actual: _context.Games.Count(), Is.EqualTo(expected: size + 1));
            });
        }

        [Test]
        public void Join_Correct()
        {
             GameEntrant entry = new("two", "fifth");

            _repository.GameRepository.Join(entry);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Games.ElementAt(2).Token, Is.EqualTo(expected: "two"));
                Assert.That(actual: _context.Games.ElementAt(2).Status, Is.EqualTo(expected: Status.Playing));
                Assert.That(actual: _context.Games.ElementAt(2).First, Is.EqualTo(expected: "fourth"));
                Assert.That(actual: _context.Games.ElementAt(2).Second, Is.EqualTo(expected: "fifth"));
            });
        }

        [Test]
        public void JoinPlayer_Correct()
        {
            GameEntrant entry = new("fourth", "fifth");

            _repository.GameRepository.JoinPlayer(entry);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Games.ElementAt(2).Token, Is.EqualTo(expected: "two"));
                Assert.That(actual: _context.Games.ElementAt(2).Status, Is.EqualTo(expected: Status.Playing));
                Assert.That(actual: _context.Games.ElementAt(2).First, Is.EqualTo(expected: "fourth"));
                Assert.That(actual: _context.Games.ElementAt(2).Second, Is.EqualTo(expected: "fifth"));
            });
        }

        [Test]
        public void Update_Correct()
        {
            Assert.That(actual: _repository.GameRepository.GetPlayersTurnByPlayersToken("12"), Is.EqualTo(expected: Color.Black));

            _repository.GameRepository.Move(new("12",2,3));

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.GameRepository.GetPlayersTurnByPlayersToken("12"), Is.EqualTo(expected: Color.White));
                Assert.That(actual: _repository.GameRepository.GetBoardByPlayersToken("12")?[2,3], Is.EqualTo(expected: Color.Black));
            });
        }

        [Test]
        public void Delete_Correct()
        {
            int size = _context.Games.Count();

            _repository.GameRepository.Delete("fourth");

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Games.Count(), Is.Not.EqualTo(size));
                Assert.That(actual: _context.Games.Count(), Is.EqualTo(expected: size - 1));
            });
        }

        [Test]
        public void GetPlayersGame_Incorrect()
        {
            string playerToken = "first123456";

            var respons = _repository.GameRepository.GetGameTokenByPlayersToken(playerToken);

            Assert.That(respons, Is.Null);
        }
    }
}
