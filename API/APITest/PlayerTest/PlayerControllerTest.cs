using API.Data;
using API.Models;
using System.Net;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITest.PlayerTest
{
    [TestFixture]
    public class ControllerTest
    {
        private Database _context;
        private IRepository _repository;
        private PlayerController _controller;

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
                Friends = { "eight", "nine" },
                Requests = new List<Request>
                {
                    new(Inquiry.Friend, "seven"),
                    new(Inquiry.Friend, "four"),
                    new(Inquiry.Friend, "five")
                }
            };
            Player seven = new("seven", "seven");
            Player eight = new("eight", "eight") { Friends = { "six" } };
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
            _controller = new PlayerController(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void Create_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Create(new("new", "newby"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("newby"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_FORBIDDEN()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Create(new("one", "one"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete("first");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.PlayerRepository.Get("first"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_NotFound()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete("firsttt");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayerByToken_Correct()
        {
            var result = _controller.PlayerByToken("first");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            var respons = (Player?)okResult?.Value;

            if (respons is not null)
                Assert.That(actual: respons.Username, Is.EqualTo("one"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayerByUsername_Correct()
        {
            var result = _controller.PlayerByUsername("one");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            var respons = (Player?)okResult?.Value;

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo("first"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayersName_Correct()
        {
            var result = _controller.PlayersName("first");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            var respons = (string?)okResult?.Value;

            if (respons is not null)
                Assert.That(actual: respons, Is.EqualTo("one"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayerFriends_Correct()
        {
            var result = _controller.PlayerFriends("sixth");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            var respons = okResult?.Value as List<string>;

            if (respons is not null)
                Assert.That(actual: respons, Does.Contain("nine"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayerPending_Correct()
        {
            var result = _controller.PlayerPending("sixth");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            var respons = okResult?.Value as List<Request>;

            if (respons is not null)
                Assert.That(actual: respons.Any(r => r.Type == Inquiry.Friend && r.Username == "five"), Is.True);
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Send_OK()
        {
            PlayerRequest request = new("one", "two");
            ActionResult<HttpResponseMessage>? result = _controller.Send(request);
            HttpResponseMessage? respons = result?.Value;

            ActionResult<Player>? player = _controller.PlayerByUsername("one");
            Assert.That(player.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = player.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");
            var player_respons = (Player?)okResult?.Value;

            ActionResult<Player>? sender = _controller.PlayerByUsername("two");
            Assert.That(sender.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okkResult = sender.Result as OkObjectResult;
            Assert.That(okkResult, Is.Not.Null, "Result should not be null");
            var sender_respons = (Player?)okkResult?.Value;

            if (respons is not null && player_respons is not null && sender_respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: player_respons.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.True);
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("one")?.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.True);
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.True);
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Accept_OK()
        {
            _controller.Send(new("one", "two"));
            ActionResult<HttpResponseMessage>? result = _controller.Accept(new("two", "one"));
            HttpResponseMessage? respons = result?.Value;

            Player? player = _repository.PlayerRepository.GetByUsername("one");
            Player? sender = _repository.PlayerRepository.GetByUsername("two");

            if (respons is not null && player is not null && sender is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: player.Friends, Does.Contain("two"));
                    Assert.That(actual: sender.Friends, Does.Contain("one"));
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("one")?.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.False);
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("one")?.Friends, Does.Contain("two"));
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("two")?.Friends, Does.Contain("one"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Friends, Does.Contain("two"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.False);
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("two"))?.Friends, Does.Contain("one"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Decline_OK()
        {
            PlayerRequest request = new("one", "two");
            _controller.Send(request);
            ActionResult<HttpResponseMessage>? result = _controller.Decline(new("two", "one"));
            HttpResponseMessage? respons = result?.Value;

            Player? player = _repository.PlayerRepository.GetByUsername("one");
            Player? sender = _repository.PlayerRepository.GetByUsername("two");

            if (respons is not null && player is not null && sender is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: player.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.False);
                    Assert.That(actual: player.Friends, Does.Not.Contain("two"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.False);
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void DeleteFriend_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.DeleteFriend(new("eight", "six"));
            HttpResponseMessage? respons = result?.Value;

            Player? player = _repository.PlayerRepository.GetByUsername("eight");
            Player? sender = _repository.PlayerRepository.GetByUsername("six");

            if (respons is not null && player is not null && sender is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: sender.Friends, Does.Not.Contain("eight"));
                    Assert.That(actual: player.Friends, Does.Not.Contain("six"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("six"))?.Friends, Does.Not.Contain("eight"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("eight"))?.Friends, Does.Not.Contain("six"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }
    }
}
