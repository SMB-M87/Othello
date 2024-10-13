using Backend.Data;
using Backend.Models;

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
            Player five = new("five") { Token = "fifth" };
            Player six = new("six") { Token = "sixth" };

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
