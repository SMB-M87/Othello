using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
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

            _repository.PlayerRepository.Create(one);
            _repository.PlayerRepository.Create(two);
            _repository.PlayerRepository.Create(three);
            _repository.PlayerRepository.Create(four);
            _repository.PlayerRepository.Create(five);
            _repository.PlayerRepository.Create(six);

            Game game0 = new(one.Token, "I wanna play a game and don't have any requirements.")
            {
                Token = "zero",
                FColor = Color.Black
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
                FColor = Color.Black
            };

            _repository.GameRepository.Create(game0);
            _repository.GameRepository.Create(game1);
            _repository.GameRepository.Create(game2);

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
            int size = _context.Results.Count();
            GameResult result = new("-4", "third", "second");

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
            var result = _repository.ResultRepository.GetPlayerStats("second");

            Assert.That(actual: result, Is.EqualTo(expected: "Wins:2\t\tLosses:1\t\tDraws:0"));
        }

        [Test]
        public void GetPlayerStats_Draw_Correct()
        {
            GameResult result = new("-4", "", "", "second third");
            _repository.ResultRepository.Create(result);

            var results = _repository.ResultRepository.GetPlayerStats("second");

            Assert.That(actual: results, Is.EqualTo(expected: "Wins:2\t\tLosses:1\t\tDraws:1"));
        }

        [Test]
        public void GetPlayerStats_NonExistant_Correct()
        {
            var result = _repository.ResultRepository.GetPlayerStats("zero");

            Assert.That(actual: result, Is.EqualTo(expected: "Wins:0\t\tLosses:0\t\tDraws:0"));
        }

        [Test]
        public void GetPlayerMatchHistory_Correct()
        {
            var result = _repository.ResultRepository.GetPlayersMatchHistory("second");

            if (result is not null)
                Assert.That(actual: result, Has.Count.EqualTo(expected: 3));
            else
                Assert.Fail("Result is null.");
        }

        [Test]
        public void GetPlayerMatchHistory_Empty_Correct()
        {
            var result = _repository.ResultRepository.GetPlayersMatchHistory("first");

            if (result is not null)
                Assert.That(actual: result, Has.Count.EqualTo(expected: 0));
            else
                Assert.Fail("Result is null.");
        }

        [Test]
        public void GetPlayerMatchHistory_NonExistant_Correct()
        {
            var result = _repository.ResultRepository.GetPlayersMatchHistory("zero");

            if (result is not null)
                Assert.That(actual: result, Has.Count.EqualTo(expected: 0));
            else
                Assert.Fail("Result is null.");
        }
    }
}
