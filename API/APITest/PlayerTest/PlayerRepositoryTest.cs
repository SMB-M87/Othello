using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace APITest.PlayerTest
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
            Player six = new("sixth", "six")
            {
                Friends = { "eight" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "seven"),
                    new(Inquiry.Friend, "four"),
                    new(Inquiry.Friend, "five")
                }
            };
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
            int size = _context.Players.Count();
            Player player = new("sfifth", "fivess");

            _repository.PlayerRepository.Create(player);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Players.Count(), Is.Not.EqualTo(expected: size));
                Assert.That(actual: _context.Players.Count(), Is.EqualTo(expected: size + 1));
            });
        }

        [Test]
        public void Delete_Correct()
        {
            int size = _context.Players.Count();
            Player player = new("fifth", "five");
            Assert.That(actual: _context.Players.Count(), Is.EqualTo(size));

            _repository.PlayerRepository.Delete("first");

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Players.Count(), Is.Not.EqualTo(expected: size));
                Assert.That(actual: _context.Players.Count(), Is.EqualTo(expected: size - 1));
            });
        }

        [Test]
        public void PlayerFriends_Correct()
        {
            List<string>? respons = _repository.PlayerRepository.GetFriends("sixth");

            if (respons is not null)
                Assert.That(actual: respons, Does.Contain("eight"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Player_FriendRequest_Correct()
        {
            List<string>? respons = _repository.PlayerRepository.GetFriendRequests("sixth");

            if (respons is not null)
                Assert.That(actual: respons.Any(r => r == "five"), Is.True);
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void SendFriendInvite_Correct()
        {
            Player player1 = _context.Players.First(p => p.Username == "one");
            Player player2 = _context.Players.First(p => p.Username == "two");

            _repository.PlayerRepository.FriendRequest(player1.Username, player2.Token);

            Assert.Multiple(() =>
            {
                Assert.That(player1.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.True);
            });
        }

        [Test]
        public void AcceptFriendInvite_Correct()
        {
            Player player1 = _context.Players.First(p => p.Username == "one");
            Player player2 = _context.Players.First(p => p.Username == "two");

            _repository.PlayerRepository.FriendRequest(player1.Username, player2.Token);
            _repository.PlayerRepository.AcceptFriendRequest(player2.Username, player1.Token);

            Assert.Multiple(() =>
            {
                Assert.That(player1.Friends, Does.Contain("two"));
                Assert.That(player2.Friends, Does.Contain("one"));
                Assert.That(player1.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.False);
            });
        }

        [Test]
        public void DeclineFriendInvite_Correct()
        {
            Player player1 = _context.Players.First(p => p.Username == "one");
            Player player2 = _context.Players.First(p => p.Username == "two");

            _repository.PlayerRepository.FriendRequest("one", "two");
            _repository.PlayerRepository.DeclineFriendRequest("two", "one");

            Assert.Multiple(() =>
            {
                Assert.That(player1.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.False);
            });
        }

        [Test]
        public void DeleteFriend_OK()
        {
            _repository.PlayerRepository.DeleteFriend("six", "five");

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("six"))?.Friends, Does.Not.Contain("five"));
                Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("five"))?.Friends, Does.Not.Contain("six"));
            });
        }
    }
}
