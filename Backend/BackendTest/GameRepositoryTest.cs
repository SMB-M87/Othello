using Backend.Models;
using Backend.Repositories;

namespace GameTest
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

            _repository.PlayerRepository.Create(one);
            _repository.PlayerRepository.Create(two);
            _repository.PlayerRepository.Create(three);
            _repository.PlayerRepository.Create(four);

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
            int size = _repository.Games().Count;
            Player five = new("five") { Token = "fifth" };
            Game game = new(five, "I search an advanced player!")
            {
                Token = "three"
            };
            game.First.Color = Color.Black;

            _repository.GameRepository.Create(game);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games(), Has.Count.Not.EqualTo(expected: size));
                Assert.That(actual: _repository.Games(), Has.Count.EqualTo(expected: size + 1));
            });
        }

        [Test]
        public void Join_Correct()
        {
             GameEntrant entry = new("two", new("five") { Token = "fifth" });

            _repository.GameRepository.Join(entry);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games()[2].Token, Is.EqualTo(expected: "two"));
                Assert.That(actual: _repository.Games()[2].Status, Is.EqualTo(expected: Status.Playing));
                Assert.That(actual: _repository.Games()[2].First.Token, Is.EqualTo(expected: "fourth"));
                Assert.That(actual: _repository.Games()[2].Second.Token, Is.EqualTo(expected: "fifth"));
            });
        }

        [Test]
        public void JoinPlayer_Correct()
        {
            GameEntrant entry = new("fourth", new("five") { Token = "fifth" });

            _repository.GameRepository.JoinPlayer(entry);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games()[2].Token, Is.EqualTo(expected: "two"));
                Assert.That(actual: _repository.Games()[2].Status, Is.EqualTo(expected: Status.Playing));
                Assert.That(actual: _repository.Games()[2].First.Token, Is.EqualTo(expected: "fourth"));
                Assert.That(actual: _repository.Games()[2].Second.Token, Is.EqualTo(expected: "fifth"));
            });
        }

        [Test]
        public void Update_Correct()
        {
            Game game = new(new("five") { Token = "five" })
            {
                Token = "zero",
                Status = Status.Finished,
                Second = new("six") { Token = "six" },
                PlayersTurn = Color.None
            };

            _repository.GameRepository.Update(game);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games()[0].Token, Is.EqualTo(expected: "zero"));
                Assert.That(actual: _repository.Games()[0].Status, Is.EqualTo(expected: Status.Finished));
                Assert.That(actual: _repository.Games()[0].First.Token, Is.EqualTo(expected: "five"));
                Assert.That(actual: _repository.Games()[0].Second.Token, Is.EqualTo(expected: "six"));
                Assert.That(actual: _repository.Games()[0].PlayersTurn, Is.EqualTo(expected: Color.None));
            });
        }

        [Test]
        public void Delete_Correct()
        {
            int size = _repository.Games().Count;
            Game game = _repository.Games()[2];

            _repository.GameRepository.Delete(game);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Games(), Has.Count.Not.EqualTo(size));
                Assert.That(actual: size, Is.EqualTo(3));
                Assert.That(actual: _repository.Games(), Has.Count.EqualTo(expected: size - 1));
            });
        }

        [Test]
        public void Get_Correct()
        {
            string token = "ValidTest";
            Player five = new("five") { Token = "fifth" };
            _repository.Games().Add(new Game(five, "I wanna play a game and don't have any requirements!") { Token = token });

            var respons = _repository.GameRepository.Get(token);

            if (respons is not null)
                Assert.That(actual: respons.First.Token, Is.EqualTo(expected: "fifth"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Get_Incorrect()
        {
            string token = "BlaBlaInvalidTestT123";

            var respons = _repository.GameRepository.Get(token);

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
            game.Second = new(six.Token)
            {
                Color = Color.White
            };
            game.Status = Status.Playing;
            _repository.Games().Add(game);

            var respons = _repository.GameRepository.GetPlayersGame("fifth");

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
            game.Second = new(six.Token)
            {
                Color = Color.White
            };
            game.Status = Status.Playing;
            _repository.Games().Add(game);

            var respons = _repository.GameRepository.GetPlayersGame("sixth");

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: game.Token));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GetPlayersGame_Incorrect()
        {
            string playerToken = "first123456";

            var respons = _repository.GameRepository.GetPlayersGame(playerToken);

            Assert.That(respons, Is.Null);
        }

        [Test]
        public void GetGames_Correct()
        {
            var respons = _repository.GameRepository.GetGames();

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: _repository.Games()[0].Description, Is.EqualTo(respons[0].Description));
                    Assert.That(actual: _repository.Games()[1].First.Token, Is.EqualTo(respons[1].First.Token));
                    Assert.That(actual: _repository.Games()[2].First.Token, Is.EqualTo(respons[2].First.Token));
                });
            }
            else
                Assert.Fail("Respons was null.");
        }
    }
}
