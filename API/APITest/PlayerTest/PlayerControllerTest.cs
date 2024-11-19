using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
                    new(Inquiry.Friend, "five"),
                    new(Inquiry.Game, "one")
                }
            };
            Player seven = new("seven", "seven");
            Player eight = new("eight", "eight") { Friends = { "six" } };
            Player nine = new("nine", "nine");
            Player ten = new("10", "10")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t11 = new("11", "11")
            {
                LastActivity = DateTime.UtcNow
            };
            Player t12 = new("12", "12")
            {
                LastActivity = DateTime.UtcNow,
                Requests = new List<Request>
                {
                    new(Inquiry.Game, "10")
                }
            };
            Player t13 = new("13", "13")
            {
                LastActivity = DateTime.UtcNow,
                Requests = new List<Request>
                {
                    new(Inquiry.Game, "10")
                    {
                        Date = DateTime.UtcNow.AddMinutes(2),
                    }
                }
            };

            Player delete = new("deleted", "Deleted");

            _repository.PlayerRepository.Create(one);
            _repository.PlayerRepository.Create(two);
            _repository.PlayerRepository.Create(three);
            _repository.PlayerRepository.Create(four);
            _repository.PlayerRepository.Create(five);
            _repository.PlayerRepository.Create(six);
            _repository.PlayerRepository.Create(seven);
            _repository.PlayerRepository.Create(eight);
            _repository.PlayerRepository.Create(nine);
            _repository.PlayerRepository.Create(ten);
            _repository.PlayerRepository.Create(t11);
            _repository.PlayerRepository.Create(t12);
            _repository.PlayerRepository.Create(t13);
            _repository.PlayerRepository.Create(delete);


            Game game0 = new("zero", one.Token);

            Game game1 = new("one", two.Token, Color.Black, three.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game2 = new("two", four.Token, Color.Black);

            Game game3 = new("three", "nonexistant");

            Game game4 = new("four", six.Token, Color.Black, seven.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game5 = new("five", eight.Token, Color.Black, nine.Token, Status.Pending, Color.White, "I search an advanced player!");

            Game game6 = new("six", eight.Token, Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game7 = new("seven", "nonexistant", Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game8 = new("eight", nine.Token, Color.Black, "nonexistant", Status.Playing, Color.White, "I search an advanced player!");

            Game game9 = new("null", one.Token, Color.Black, one.Token, Status.Finished, Color.None);

            Game game10 = new("10", ten.Token, Color.Black);

            _repository.GameRepository.Create(game0);
            _repository.GameRepository.Create(game1);
            _repository.GameRepository.Create(game2);
            _repository.GameRepository.Create(game3);
            _repository.GameRepository.Create(game4);
            _repository.GameRepository.Create(game5);
            _repository.GameRepository.Create(game6);
            _repository.GameRepository.Create(game7);
            _repository.GameRepository.Create(game8);
            _repository.GameRepository.Create(game9);
            _repository.GameRepository.Create(game10);

            _controller = new PlayerController(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_OKAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Create(new("new", "newby"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Create_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Create(new("one", "one"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Delete_OKAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Delete(new("first"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Delete_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Delete(new("firsttt"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Send_OKAsync()
        {
            PlayerRequest request = new("one", "second");
            ActionResult<HttpResponseMessage>? result = await _controller.FriendRequest(request);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.True);
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Accept_OKAsync()
        {
            await _controller.FriendRequest(new("one", "second"));
            ActionResult<HttpResponseMessage>? result = await _controller.AcceptFriendRequest(new("two", "first"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Friends, Does.Contain("two"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.False);
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("two"))?.Friends, Does.Contain("one"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Decline_OKAsync()
        {
            PlayerRequest request = new("one", "second");
            await _controller.FriendRequest(request);
            ActionResult<HttpResponseMessage>? result = await _controller.DeclineFriendRequest(new("two", "first"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("one"))?.Requests.Any(r => r.Username == "two" && r.Type == Inquiry.Friend), Is.False);
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task DeleteFriend_OKAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.DeleteFriend(new("eight", "sixth"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("six"))?.Friends, Does.Not.Contain("eight"));
                    Assert.That(actual: _context.Players.FirstOrDefault(p => p.Username.Equals("eight"))?.Friends, Does.Not.Contain("six"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task GameRequest_OKAsync()
        {
            var request = new PlayerRequest { ReceiverUsername = "11", SenderToken = "10" };
            var result = await _controller.GameRequest(request);

            Assert.Multiple(() =>
            {
                var httpResponse = result.Value;
                Assert.That(httpResponse, Is.Not.Null, "Expected HttpResponseMessage not to be null");
                Assert.That(httpResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected OK status");
            });
        }

        [Test]
        public async Task GameRequest_BadRequestAsync()
        {
            var request = new PlayerRequest { ReceiverUsername = "nonexistent", SenderToken = "second" };
            var result = await _controller.GameRequest(request);

            Assert.Multiple(() =>
            {
                var httpResponse = result.Value;
                Assert.That(httpResponse, Is.Not.Null, "Expected HttpResponseMessage not to be null");
                Assert.That(httpResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "Expected BadRequest status");
            });
        }

        [Test]
        public async Task AcceptGameRequest_OKAsync()
        {
            var request = new PlayerRequest { ReceiverUsername = "10", SenderToken = "12" };
            var result = await _controller.AcceptGameRequest(request);

            Assert.Multiple(() =>
            {
                var httpResponse = result.Value;
                Assert.That(httpResponse, Is.Not.Null, "Expected HttpResponseMessage not to be null");
                Assert.That(httpResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected OK status");
            });
        }

        [Test]
        public async Task AcceptGameRequest_BadRequestAsync()
        {
            var request = new PlayerRequest { ReceiverUsername = "nonexistent", SenderToken = "second" };
            var result = await _controller.AcceptGameRequest(request);

            Assert.Multiple(() =>
            {
                var httpResponse = result.Value;
                Assert.That(httpResponse, Is.Not.Null, "Expected HttpResponseMessage not to be null");
                Assert.That(httpResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "Expected BadRequest status");
            });
        }

        [Test]
        public async Task DeclineGameRequest_OKAsync()
        {
            var request = new PlayerRequest { ReceiverUsername = "10", SenderToken = "12" };
            var result = await _controller.DeclineGameRequest(request);

            Assert.Multiple(() =>
            {
                var httpResponse = result.Value;
                Assert.That(httpResponse, Is.Not.Null, "Expected HttpResponseMessage not to be null");
                Assert.That(httpResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected OK status");
            });
        }

        [Test]
        public async Task DeclineGameRequest_BadRequestAsync()
        {
            var request = new PlayerRequest { ReceiverUsername = "nonexistent", SenderToken = "two" };
            var result = await _controller.DeclineGameRequest(request);

            Assert.Multiple(() =>
            {
                var httpResponse = result.Value;
                Assert.That(httpResponse, Is.Not.Null, "Expected HttpResponseMessage not to be null");
                Assert.That(httpResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "Expected BadRequest status");
            });
        }

        [Test]
        public async Task DeletePlayer_OKAsync()
        {
            var result = await _controller.Delete(new("13"));

            Assert.Multiple(() =>
            {
                var httpResponse = result.Value;
                Assert.That(httpResponse, Is.Not.Null, "Expected HttpResponseMessage not to be null");
                Assert.That(httpResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected OK status");
            });
        }

        [Test]
        public async Task DeletePlayer_BadRequestAsync()
        {
            var result = await _controller.Delete(new("nonexistent"));

            Assert.Multiple(() =>
            {
                var httpResponse = result.Value;
                Assert.That(httpResponse, Is.Not.Null, "Expected HttpResponseMessage not to be null");
                Assert.That(httpResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "Expected BadRequest status");
            });
        }
    }
}
