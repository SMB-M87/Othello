using API.Data;
using API.Models;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITest.ResultTest
{
    [TestFixture]
    public class ControllerTest
    {
        private Database _context;
        private IRepository _repository;
        private ResultController _controller;

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

            GameResult result0 = new("-3", "second", "third");
            GameResult result1 = new("-2", "third", "second");
            GameResult result2 = new("-1", "second", "third");

            _repository.ResultRepository.Create(result0);
            _repository.ResultRepository.Create(result1);
            _repository.ResultRepository.Create(result2);

            _controller = new ResultController(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void MatchHistory_Correct()
        {
            var result = _controller.MatchHistory("second");

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
                var okResult = result.Result as OkObjectResult;
                Assert.That(okResult, Is.Not.Null, "Result should not be null");
                var results = okResult?.Value as List<GameResult>;
                Assert.That(results, Is.Not.Null, "Results should not be null");
                Assert.That(results?.Count, Is.GreaterThan(0), "Should have at least one result");
                Assert.That(actual: results?[0], Is.EqualTo(expected: _context.Results.First(p => p.Token == "-1")));
                Assert.That(actual: results?[1], Is.EqualTo(expected: _context.Results.First(p => p.Token == "-2")));
                Assert.That(actual: results?[2], Is.EqualTo(expected: _context.Results.First(p => p.Token == "-3")));
            });
        }

        [Test]
        public void PlayerStats_Correct()
        {
            var result = _controller.PlayerStats("second");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            if (okResult is not null)
                Assert.That(actual: okResult.Value, Is.EqualTo(expected: "Wins:2\t\tLosses:1\t\tDraws:0"));
        }
    }
}
