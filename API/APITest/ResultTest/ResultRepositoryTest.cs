using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace APITest.ResultTest
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

            _repository.PlayerRepository.Create(one);
            _repository.PlayerRepository.Create(two);
            _repository.PlayerRepository.Create(three);
            _repository.PlayerRepository.Create(four);
            _repository.PlayerRepository.Create(five);
            _repository.PlayerRepository.Create(six);
            _repository.PlayerRepository.Create(seven);
            _repository.PlayerRepository.Create(eight);
            _repository.PlayerRepository.Create(nine);

            Game game = new("null", one.Token, Color.Black, one.Token, Status.Finished, Color.None);

            Game game0 = new("zero", one.Token);

            Game game1 = new("one", two.Token, Color.Black, three.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game2 = new("two", four.Token, Color.Black);

            Game game3 = new("three", "nonexistant");

            Game game4 = new("four", six.Token, Color.Black, seven.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game5 = new("five", eight.Token, Color.Black, nine.Token, Status.Pending, Color.White, "I search an advanced player!");

            Game game6 = new("six", eight.Token, Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game7 = new("seven", "nonexistant", Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game8 = new("eight", nine.Token, Color.Black, "nonexistant", Status.Playing, Color.White, "I search an advanced player!");

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

            GameResult result0 = new("-3", "second", "third", game1.Board);
            GameResult result1 = new("-2", "third", "second", game1.Board);
            GameResult result2 = new("-1", "second", "third", game1.Board);

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
            Game game8 = new("eight", "bla", Color.Black, "nonexistant", Status.Playing, Color.White, "I search an advanced player!");
            int size = _context.Results.Count();
            GameResult result = new("-4", "third", "second", game8.Board);

            _repository.ResultRepository.Create(result);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Results.Count(), Is.Not.EqualTo(expected: size));
                Assert.That(actual: _context.Results.Count(), Is.EqualTo(expected: size + 1));
            });
        }

        [Test]
        public void GetPlayerStats_Correct()
        {
            var result = _repository.ResultRepository.GetPlayerStats("two");

            Assert.That(actual: result, Is.EqualTo(expected: "Wins:2\t\tLosses:1\t\tDraws:0"));
        }

        [Test]
        public void GetPlayerStats_Draw_Correct()
        {
            Game game8 = new("eight", "bla", Color.Black, "nonexistant", Status.Playing, Color.White, "I search an advanced player!");
            GameResult result = new("-4", "second", "third", game8.Board, true);
            _repository.ResultRepository.Create(result);

            var results = _repository.ResultRepository.GetPlayerStats("two");

            Assert.That(actual: results, Is.EqualTo(expected: "Wins:2\t\tLosses:1\t\tDraws:1"));
        }

        [Test]
        public void GetPlayerStats_NonExistant_Correct()
        {
            var result = _repository.ResultRepository.GetPlayerStats("zero");

            Assert.That(actual: result, Is.Null);
        }

        [Test]
        public void GetPlayerMatchHistory_Correct()
        {
            var result = _repository.ResultRepository.GetPlayersMatchHistory("two");

            if (result is not null)
                Assert.That(actual: result, Has.Count.EqualTo(expected: 3));
            else
                Assert.Fail("Result is null.");
        }

        [Test]
        public void GetPlayerMatchHistory_Empty_Correct()
        {
            var result = _repository.ResultRepository.GetPlayersMatchHistory("one");

            if (result is not null)
                Assert.That(actual: result, Has.Count.EqualTo(expected: 0));
            else
                Assert.Fail("Result is null.");
        }

        [Test]
        public void GetPlayerMatchHistory_NonExistant_Correct()
        {
            var result = _repository.ResultRepository.GetPlayersMatchHistory("zero");


            Assert.That(actual: result, Is.Null);
        }
    }
}
