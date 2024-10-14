using System.Net;
using Backend.Data;
using Backend.Models;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PlayerTest
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

            _repository.PlayerRepository.SendFriendInvite("five", "six");
            _repository.PlayerRepository.SendFriendInvite("four", "six");
            _repository.PlayerRepository.AcceptFriendInvite("five", "six");

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
            ActionResult<HttpResponseMessage>? result = _controller.Create("newby");
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
            ActionResult<HttpResponseMessage>? result = _controller.Create("one");
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
            ActionResult<Player>? result = _controller.PlayerByToken("first");
            Player? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.Username, Is.EqualTo("one"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayerByUsername_Correct()
        {
            ActionResult<Player>? result = _controller.PlayerByUsername("one");
            Player? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo("first"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayersName_Correct()
        {
            ActionResult<string>? result = _controller.PlayersName("first");
            string? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons, Is.EqualTo("one"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayerFriends_Correct()
        {
            ActionResult<List<string>?>? result = _controller.PlayerFriends("sixth");
            List<string>? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons, Does.Contain("five"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayerPending_Correct()
        {
            ActionResult<List<string>?>? result = _controller.PlayerPending("fourth");
            List<string>? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons, Does.Contain("six"));
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
            ActionResult<Player>? sender = _controller.PlayerByUsername("two");

            if (respons is not null && player is not null && sender is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: player.Value?.PendingFriends, Does.Contain("two"));
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("one")?.PendingFriends, Does.Contain("two"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.PendingFriends, Does.Contain("two"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Accept_OK()
        {
            _controller.Send(new("one", "two"));
            ActionResult<HttpResponseMessage>? result = _controller.Accept(new("two","one"));
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
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("one")?.PendingFriends, Does.Not.Contain("two"));
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("two")?.PendingFriends, Does.Not.Contain("one"));
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("one")?.Friends, Does.Contain("two"));
                    Assert.That(actual: _repository.PlayerRepository.GetByUsername("two")?.Friends, Does.Contain("one"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Friends, Does.Contain("two"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.PendingFriends, Does.Not.Contain("two"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("two"))?.Friends, Does.Contain("one"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("two"))?.PendingFriends, Does.Not.Contain("one"));
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
                    Assert.That(actual: player.PendingFriends, Does.Not.Contain("two"));
                    Assert.That(actual: player.Friends, Does.Not.Contain("two"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.PendingFriends, Does.Not.Contain("two"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void DeleteFriend_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.DeleteFriend(new("six", "five"));
            HttpResponseMessage? respons = result?.Value;

            Player? player = _repository.PlayerRepository.GetByUsername("six");
            Player? sender = _repository.PlayerRepository.GetByUsername("five");

            if (respons is not null && player is not null && sender is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: sender.Friends, Does.Not.Contain("six"));
                    Assert.That(actual: player.Friends, Does.Not.Contain("five"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("six"))?.Friends, Does.Not.Contain("five"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("five"))?.Friends, Does.Not.Contain("six"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }
    }
}
