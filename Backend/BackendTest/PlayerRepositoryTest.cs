using Backend.Models;
using Backend.Repositories;

namespace PlayerTest
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
            int size = _repository.Players().Count;
            Player player = new("five") { Token = "fifth" };

            _repository.PlayerRepository.Create(player);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Players(), Has.Count.Not.EqualTo(expected: size));
                Assert.That(actual: _repository.Players(), Has.Count.EqualTo(expected: size + 1));
            });
        }

        [Test]
        public void Update_Correct()
        {
            Player player = _repository.Players()[0];
            player.Username = "Umugud";
            player.Token = "Waaaah";

            _repository.PlayerRepository.Update(player);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Players()[0].Username, Is.EqualTo(expected: "Umugud"));
                Assert.That(actual: _repository.Players()[0].Token, Is.EqualTo(expected: "Waaaah"));
            });
        }

        [Test]
        public void Delete_Correct()
        {
            int size = _repository.Players().Count;
            Player player = new("five") { Token = "fifth" };

            _repository.PlayerRepository.Delete(_repository.Players()[0]);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.Players(), Has.Count.Not.EqualTo(expected: size));
                Assert.That(actual: size, Is.EqualTo(4));
                Assert.That(actual: _repository.Players(), Has.Count.EqualTo(expected: size - 1));
            });
        }

        [Test]
        public void Get_Correct()
        {
            var respons = _repository.PlayerRepository.Get("first");

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: "first"));
            else
                Assert.Fail("Respons is null.");
        }


        [Test]
        public void GetByUsername_Correct()
        {
            var respons = _repository.PlayerRepository.GetByUsername("one");

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: "first"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GetName_Correct()
        {
            var respons = _repository.PlayerRepository.GetName("first");

            if (respons is not null)
                Assert.That(actual: respons, Is.EqualTo(expected: "one"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void SendFriendInvite_Correct()
        {
            Player player1 = _repository.Players().First(p => p.Username == "one");
            Player player2 = _repository.Players().First(p => p.Username == "two");

            _repository.PlayerRepository.SendFriendInvite("one", "two");

            Assert.Multiple(() =>
            {
                Assert.That(player1.PendingFriends, Does.Contain("two"));
                Assert.That(player2.PendingFriends, Does.Contain("one"));
            });
        }

        [Test]
        public void AcceptFriendInvite_Correct()
        {
            Player player1 = _repository.Players().First(p => p.Username == "one");
            Player player2 = _repository.Players().First(p => p.Username == "two");

            _repository.PlayerRepository.SendFriendInvite("one", "two");
            _repository.PlayerRepository.AcceptFriendInvite("one", "two");

            Assert.Multiple(() =>
            {
                Assert.That(player1.Friends, Does.Contain("two"));
                Assert.That(player2.Friends, Does.Contain("one"));
                Assert.That(player1.PendingFriends, Does.Not.Contain("two"));
                Assert.That(player2.PendingFriends, Does.Not.Contain("one"));
            });
        }

        [Test]
        public void DeclineFriendInvite_Correct()
        {
            Player player1 = _repository.Players().First(p => p.Username == "one");
            Player player2 = _repository.Players().First(p => p.Username == "two");

            _repository.PlayerRepository.SendFriendInvite("one", "two");
            _repository.PlayerRepository.DeclineFriendInvite("one", "two");

            Assert.Multiple(() =>
            {
                Assert.That(player1.PendingFriends, Does.Not.Contain("two"));
                Assert.That(player2.PendingFriends, Does.Not.Contain("one"));
            });
        }
    }
}
