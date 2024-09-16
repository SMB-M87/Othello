using Backend.Models;
using Backend.Repositories;

namespace ResultTest
{
    [TestFixture]
    public class RepositoryTest
    {
        private IRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new Repository();

            Player one = new("one") { Token = "first" };
            Player two = new("two") { Token = "second" };
            Player three = new("three") { Token = "third" };
            Player four = new("four") { Token = "fourth" };

            _repository.PlayerRepository.AddPlayer(one);
            _repository.PlayerRepository.AddPlayer(two);
            _repository.PlayerRepository.AddPlayer(three);
            _repository.PlayerRepository.AddPlayer(four);

            Game game0 = new(one, "I wanna play a game and don't have any requirements.")
            {
                Token = "zero"
            };
            game0.First.Color = Color.Black;

            Game game1 = new(two, "I search an advanced player!")
            {
                Token = "one"
            };
            game1.First.Color = Color.Black;
            game1.Second = new(three.Token)
            {
                Color = Color.White
            };
            game1.Status = Status.Playing;

            Game game2 = new(four, "I want to player more than one game against the same adversary.")
            {
                Token = "two"
            };
            game2.First.Color = Color.Black;

            _repository.GameRepository.AddGame(game0);
            _repository.GameRepository.AddGame(game1);
            _repository.GameRepository.AddGame(game2);

            GameResult result0 = new("-3", "second", "third");
            GameResult result1 = new("-2", "third", "second");
            GameResult result2 = new("-1", "second", "third");

            _repository.ResultRepository.Create(result0);
            _repository.ResultRepository.Create(result1);
            _repository.ResultRepository.Create(result2);
        }

        [Test]
        public void Create_Correct()
        {
            int size = _repository.Results().Count;
            GameResult result = new("-4", "third", "second");

            _repository.ResultRepository.Create(result);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Results(), Has.Count.Not.EqualTo(expected: size));
                Assert.That(actual: _repository.Results(), Has.Count.EqualTo(expected: size + 1));
            });
        }

        [Test]
        public void GetPlayerStats_Correct()
        {
            var (Wins, Losses, Draws) = _repository.ResultRepository.GetPlayerStats("second");

            Assert.Multiple(() =>
            {
                Assert.That(actual: Wins, Is.EqualTo(expected: 2));
                Assert.That(actual: Losses, Is.EqualTo(expected: 1));
                Assert.That(actual: Draws, Is.EqualTo(expected: 0));
            });
        }

        [Test]
        public void GetPlayerStats_Draw_Correct()
        {
            int size = _repository.Results().Count;
            GameResult result = new("-4", "", "", "second third");
            _repository.ResultRepository.Create(result);

            var (Wins, Losses, Draws) = _repository.ResultRepository.GetPlayerStats("second");

            Assert.Multiple(() =>
            {
                Assert.That(actual: Wins, Is.EqualTo(expected: 2));
                Assert.That(actual: Losses, Is.EqualTo(expected: 1));
                Assert.That(actual: Draws, Is.EqualTo(expected: 1));
            });
        }

        [Test]
        public void GetPlayerStats_NonExistant_Correct()
        {
            var (Wins, Losses, Draws) = _repository.ResultRepository.GetPlayerStats("zero");

            Assert.Multiple(() =>
            {
                Assert.That(actual: Wins, Is.EqualTo(expected: 0));
                Assert.That(actual: Losses, Is.EqualTo(expected: 0));
                Assert.That(actual: Draws, Is.EqualTo(expected: 0));
            });
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
