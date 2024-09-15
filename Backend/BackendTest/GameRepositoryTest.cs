using Backend.Data;
using Backend.Models;

namespace BackendTest
{
    [TestFixture]
    public class GameRepositoryTest
    {
        private GameRepository _repository;
        private Player one;
        private Player two;
        private Player three;
        private Player four;

        private Game game0;
        private Game game1;
        private Game game2;

        [SetUp]
        public void SetUp()
        {
            _repository = new GameRepository();

            one = new("one") { Token = "first" };
            two = new("two") { Token = "second" };
            three = new("three") { Token = "third" };
            four = new("four") { Token = "fourth" };

            game0 = new(one, "I wanna play a game and don't have any requirements.")
            {
                Token = "zero"
            };
            game0.First.Color = Color.Black;
            one.InGame = true;

            game1 = new(two, "I search an advanced player!")
            {
                Token = "one"
            };
            game1.First.Color = Color.Black;
            two.InGame = true;
            game1.Second = new(three.Token)
            {
                Color = Color.White
            };
            three.InGame = true;
            game1.Status = Status.Playing;

            game2 = new(four, "I want to player more than one game against the same adversary.")
            {
                Token = "two"
            };
            game2.First.Color = Color.Black;
            four.InGame = true;

            _repository.Games = new List<Game> { game0, game1, game2 };
            _repository.Players = new List<Player> { one, two, three, four };
        }

        [Test]
        public void GetGames_Correct()
        {
            var respons = _repository.GetGames();
            Assert.Multiple(() =>
            {
                Assert.That(respons, Is.Not.Null);
                Assert.That(actual: _repository.Games[0].Description, Is.EqualTo(respons[0].Description));
                Assert.That(actual: _repository.Games[1].First.Token, Is.EqualTo(respons[1].First.Token));
                Assert.That(actual: _repository.Games[2].First.Token, Is.EqualTo(respons[2].First.Token));
            });
        }

        [Test]
        public void GetGame_Correct()
        {
            string token = "ValidTest";
            Player five = new("five") { Token = "fifth" };
            _repository.Games.Add(new Game(five, "I wanna play a game and don't have any requirements!") { Token = token });

            var respons = _repository.GetGame(token);

            if (respons is not null)
                Assert.That(actual: respons.First.Token, Is.EqualTo(expected: "fifth"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GetGame_Incorrect()
        {
            string token = "BlaBlaInvalidTestT123";

            var respons = _repository.GetGame(token);

            Assert.That(respons, Is.Null);
        }

        [Test]
        public void GetPlayersGame_Correct_FirstPlayer()
        {
            Player five = new("five") { Token = "fifth" };
            Player six = new("six") { Token = "sixth" };
            Game game = new(five, "I search an advanced player!")
            {
                Token = "three"
            };
            game.First.Color = Color.Black;
            five.InGame = true;
            game.Second = new(six.Token)
            {
                Color = Color.White
            };
            six.InGame = true;
            game.Status = Status.Playing;
            _repository.Games.Add(game);

            var respons = _repository.GetPlayersGame("fifth");

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: game.Token));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GetPlayersGame_Correct_SecondPlayer()
        {
            Player five = new("five") { Token = "fifth" };
            Player six = new("six") { Token = "sixth" };

            Game game = new(five, "I search an advanced player!")
            {
                Token = "three"
            };
            game.First.Color = Color.Black;
            five.InGame = true;
            game.Second = new(six.Token)
            {
                Color = Color.White
            };
            six.InGame = true;
            game.Status = Status.Playing;
            _repository.Games.Add(game);

            var respons = _repository.GetPlayersGame("sixth");

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: game.Token));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GetPlayersGame_Incorrect()
        {
            string playerToken = "first123456";

            var respons = _repository.GetPlayersGame(playerToken);

            Assert.That(respons, Is.Null);
        }

        [Test]
        public void AddGame_Correct()
        {
            int size = _repository.Games.Count;
            Player five = new("five") { Token = "fifth" };
            Game game = new(five, "I search an advanced player!")
            {
                Token = "three"
            };
            game.First.Color = Color.Black;
            five.InGame = true;

            _repository.AddGame(game);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games, Has.Count.Not.EqualTo(size));
                Assert.That(actual: size + 1, Is.EqualTo(_repository.Games.Count));
            });
        }

        [Test]
        public void JoinGame_Correct()
        {
            Player five = new("five") { Token = "fifth" };
            Player six = new("six") { Token = "sixth" };
            Game game = new(five, "I want to player more than one game against the same adversary.")
            {
                Token = "three"
            };
            game.First.Color = Color.Black;
            five.InGame = true;
            _repository.Games.Add(game);
            GameEntrant entry = new(game.Token, six);

            _repository.JoinGame(entry);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games[3].Token, Is.EqualTo("three"));
                Assert.That(actual: _repository.Games[3].Status, Is.EqualTo(Status.Playing));
                Assert.That(actual: _repository.Games[3].First.Token, Is.EqualTo("fifth"));
                Assert.That(actual: _repository.Games[3].Second.Token, Is.EqualTo("sixth"));
            });
        }

        [Test]
        public void JoinGame_Incorrect()
        {
            Player five = new("five") { Token = "fifth" };
            Game game = new(five, "I want to player more than one game against the same adversary.")
            {
                Token = "three"
            };
            game.First.Color = Color.Black;
            five.InGame = true;
            _repository.Games.Add(game);
            GameEntrant joiner = new(game.Token, five);

            _repository.JoinGame(joiner);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games[3].Token, Is.EqualTo("three"));
                Assert.That(actual: _repository.Games[3].Status, Is.EqualTo(Status.Pending));
                Assert.That(actual: _repository.Games[3].First.Token, Is.EqualTo("fifth"));
                Assert.That(actual: _repository.Games[3].Second.Token, Is.EqualTo(""));
            });
        }

        [Test]
        public void DeleteGame_Correct()
        {
            int size = _repository.Games.Count;
            Game game = _repository.Games[2];

            _repository.DeleteGame(game);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games, Has.Count.Not.EqualTo(size));
                Assert.That(actual: size, Is.EqualTo(3));
                Assert.That(size - 1, Is.EqualTo(_repository.Games.Count));
            });
        }
    }
}
