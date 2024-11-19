using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APITest.GameTest
{
    [TestFixture]
    public class ControllerTest
    {
        private Database _context;
        private IRepository _repository;
        private GameController _controller;

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
            Player seven = new("seven", "seven");
            Player eight = new("eight", "eight");
            Player nine = new("nine", "nine");
            Player ten = new("10", "10");
            Player eleven = new("11", "11");
            Player twelve = new("12", "12");
            Player thirdteen = new("13", "13");
            Player fourteen = new("14", "14");
            Player fifteen = new("15", "15");
            Player sixteen = new("16", "16");
            Player seventeen = new("17", "17");
            Player eighteen = new("18", "18");
            Player nineteen = new("19", "19");
            Player twenty = new("20", "20");
            Player twentyone = new("21", "21");
            Player twentytwo = new("22", "22");
            Player twentythree = new("23", "23");
            Player twentyfour = new("24", "24");
            Player twentyfive = new("25", "25");
            Player twentysix = new("26", "26");

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
            _repository.PlayerRepository.Create(eleven);
            _repository.PlayerRepository.Create(twelve);
            _repository.PlayerRepository.Create(thirdteen);
            _repository.PlayerRepository.Create(fourteen);
            _repository.PlayerRepository.Create(fifteen);
            _repository.PlayerRepository.Create(sixteen);
            _repository.PlayerRepository.Create(seventeen);
            _repository.PlayerRepository.Create(eighteen);
            _repository.PlayerRepository.Create(nineteen);
            _repository.PlayerRepository.Create(twenty);
            _repository.PlayerRepository.Create(twentyone);
            _repository.PlayerRepository.Create(twentytwo);
            _repository.PlayerRepository.Create(twentythree);
            _repository.PlayerRepository.Create(twentyfour);
            _repository.PlayerRepository.Create(twentyfive);
            _repository.PlayerRepository.Create(twentysix);

            Game game0 = new("zero", one.Token, Color.Black);

            Game game1 = new("one", two.Token, Color.Black, three.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game2 = new("two", four.Token, Color.Black);

            Game game3 = new("three", "nonexistant", Color.Black);

            Game game4 = new("four", six.Token, Color.Black, seven.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game5 = new("five", eight.Token, Color.Black, nine.Token, Status.Pending, Color.White, "I search an advanced player!");

            Game game6 = new("six", twentyfour.Token, Color.Black, twentythree.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game7 = new("seven", "nonexistant", Color.Black, twentytwo.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game8 = new("eight", twentyone.Token, Color.Black, "nonexistant", Status.Playing, Color.White, "I search an advanced player!");

            Game game9 = new("nine", nineteen.Token, Color.Black, twenty.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game10 = new("ten", eighteen.Token, Color.Black, seventeen.Token, Status.Playing, Color.White, "I search an advanced player!");
            game10.Board[0, 0] = Color.White;
            game10.Board[0, 1] = Color.White;
            game10.Board[0, 2] = Color.White;
            game10.Board[0, 3] = Color.White;
            game10.Board[0, 4] = Color.White;
            game10.Board[0, 5] = Color.White;
            game10.Board[0, 6] = Color.White;
            game10.Board[0, 7] = Color.White;
            game10.Board[1, 0] = Color.White;
            game10.Board[1, 1] = Color.White;
            game10.Board[1, 2] = Color.White;
            game10.Board[1, 3] = Color.White;
            game10.Board[1, 4] = Color.White;
            game10.Board[1, 5] = Color.White;
            game10.Board[1, 6] = Color.White;
            game10.Board[1, 7] = Color.White;
            game10.Board[2, 0] = Color.White;
            game10.Board[2, 1] = Color.White;
            game10.Board[2, 2] = Color.White;
            game10.Board[2, 3] = Color.White;
            game10.Board[2, 4] = Color.White;
            game10.Board[2, 5] = Color.White;
            game10.Board[2, 6] = Color.White;
            game10.Board[2, 7] = Color.White;
            game10.Board[3, 0] = Color.White;
            game10.Board[3, 1] = Color.White;
            game10.Board[3, 2] = Color.White;
            game10.Board[3, 3] = Color.White;
            game10.Board[3, 4] = Color.White;
            game10.Board[3, 5] = Color.White;
            game10.Board[3, 6] = Color.White;
            game10.Board[3, 7] = Color.None;
            game10.Board[4, 0] = Color.White;
            game10.Board[4, 1] = Color.White;
            game10.Board[4, 2] = Color.White;
            game10.Board[4, 3] = Color.White;
            game10.Board[4, 4] = Color.White;
            game10.Board[4, 5] = Color.White;
            game10.Board[4, 6] = Color.None;
            game10.Board[4, 7] = Color.None;
            game10.Board[5, 0] = Color.White;
            game10.Board[5, 1] = Color.White;
            game10.Board[5, 2] = Color.White;
            game10.Board[5, 3] = Color.White;
            game10.Board[5, 4] = Color.White;
            game10.Board[5, 5] = Color.White;
            game10.Board[5, 6] = Color.None;
            game10.Board[5, 7] = Color.Black;
            game10.Board[6, 0] = Color.White;
            game10.Board[6, 1] = Color.White;
            game10.Board[6, 2] = Color.White;
            game10.Board[6, 3] = Color.White;
            game10.Board[6, 4] = Color.White;
            game10.Board[6, 5] = Color.White;
            game10.Board[6, 6] = Color.White;
            game10.Board[6, 7] = Color.None;
            game10.Board[7, 0] = Color.White;
            game10.Board[7, 1] = Color.White;
            game10.Board[7, 2] = Color.White;
            game10.Board[7, 3] = Color.White;
            game10.Board[7, 4] = Color.White;
            game10.Board[7, 5] = Color.White;
            game10.Board[7, 6] = Color.White;
            game10.Board[7, 7] = Color.White;

            Game game11 = new("eleven", fifteen.Token, Color.Black, sixteen.Token, Status.Playing, Color.Black, "I search an advanced player!");
            game11.Board[0, 0] = Color.White;
            game11.Board[0, 1] = Color.White;
            game11.Board[0, 2] = Color.White;
            game11.Board[0, 3] = Color.White;
            game11.Board[0, 4] = Color.White;
            game11.Board[0, 5] = Color.White;
            game11.Board[0, 6] = Color.White;
            game11.Board[0, 7] = Color.White;
            game11.Board[1, 0] = Color.White;
            game11.Board[1, 1] = Color.White;
            game11.Board[1, 2] = Color.White;
            game11.Board[1, 3] = Color.White;
            game11.Board[1, 4] = Color.White;
            game11.Board[1, 5] = Color.White;
            game11.Board[1, 6] = Color.White;
            game11.Board[1, 7] = Color.White;
            game11.Board[2, 0] = Color.White;
            game11.Board[2, 1] = Color.White;
            game11.Board[2, 2] = Color.White;
            game11.Board[2, 3] = Color.White;
            game11.Board[2, 4] = Color.White;
            game11.Board[2, 5] = Color.White;
            game11.Board[2, 6] = Color.White;
            game11.Board[2, 7] = Color.White;
            game11.Board[3, 0] = Color.White;
            game11.Board[3, 1] = Color.White;
            game11.Board[3, 2] = Color.White;
            game11.Board[3, 3] = Color.White;
            game11.Board[3, 4] = Color.White;
            game11.Board[3, 5] = Color.White;
            game11.Board[3, 6] = Color.White;
            game11.Board[3, 7] = Color.None;
            game11.Board[4, 0] = Color.White;
            game11.Board[4, 1] = Color.White;
            game11.Board[4, 2] = Color.White;
            game11.Board[4, 3] = Color.White;
            game11.Board[4, 4] = Color.White;
            game11.Board[4, 5] = Color.White;
            game11.Board[4, 6] = Color.None;
            game11.Board[4, 7] = Color.None;
            game11.Board[5, 0] = Color.White;
            game11.Board[5, 1] = Color.White;
            game11.Board[5, 2] = Color.White;
            game11.Board[5, 3] = Color.White;
            game11.Board[5, 4] = Color.White;
            game11.Board[5, 5] = Color.White;
            game11.Board[5, 6] = Color.None;
            game11.Board[5, 7] = Color.Black;
            game11.Board[6, 0] = Color.White;
            game11.Board[6, 1] = Color.White;
            game11.Board[6, 2] = Color.White;
            game11.Board[6, 3] = Color.White;
            game11.Board[6, 4] = Color.White;
            game11.Board[6, 5] = Color.White;
            game11.Board[6, 6] = Color.White;
            game11.Board[6, 7] = Color.None;
            game11.Board[7, 0] = Color.White;
            game11.Board[7, 1] = Color.White;
            game11.Board[7, 2] = Color.White;
            game11.Board[7, 3] = Color.White;
            game11.Board[7, 4] = Color.White;
            game11.Board[7, 5] = Color.White;
            game11.Board[7, 6] = Color.White;
            game11.Board[7, 7] = Color.White;

            Game game12 = new("12", fourteen.Token, Color.Black, five.Token, Status.Playing, Color.White, "I search an advanced player!");
            game12.Board[0, 0] = Color.White;
            game12.Board[0, 1] = Color.Black;
            game12.Board[1, 1] = Color.White;
            game12.Board[3, 3] = Color.None;
            game12.Board[3, 4] = Color.None;
            game12.Board[4, 4] = Color.None;
            game12.Board[4, 3] = Color.None;

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
            _repository.GameRepository.Create(game11);
            _repository.GameRepository.Create(game12);

            _controller = new GameController(_repository);
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
            GameCreation create = new("26");

            ActionResult<HttpResponseMessage>? result = await _controller.Create(create);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("26"), Is.Not.Null);
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Create_Player_BadRequestAsync()
        {
            GameCreation create = new("player");

            ActionResult<HttpResponseMessage>? result = await _controller.Create(create);
            HttpResponseMessage? response = result?.Value;

            var player = await _repository.GameRepository.Get("player");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: player, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Create_PlayerInGame_BadRequestAsync()
        {
            GameCreation create = new("third");

            ActionResult<HttpResponseMessage>? result = await _controller.Create(create);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("third"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Join_Player_BadRequestAsync()
        {
            PlayerRequest entry = new("two", "player");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            var player = await _repository.GameRepository.Get("player");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: player, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Join_Game_BadRequestAsync()
        {
            PlayerRequest entry = new("join", "25");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            var game = await _repository.GameRepository.Get("25");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: game, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Join_PlayerInGame_BadRequestAsync()
        {
            PlayerRequest entry = new("zero", "fourth");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fourth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Join_GameFull_BadRequestAsync()
        {
            PlayerRequest entry = new("second", "25");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            var game = await _repository.GameRepository.Get("25");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: game, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Join_OwnGame_BadRequestAsync()
        {
            PlayerRequest entry = new("zero", "first");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Join_FirstPlayerNonExistant_BadRequestAsync()
        {
            PlayerRequest entry = new("three", "25");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            var game = await _repository.GameRepository.Get("25");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: game, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Join_OKAsync()
        {
            PlayerRequest entry = new("four", "25");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("25"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task JoinPlayer_Player_BadRequestAsync()
        {
            PlayerRequest entry = new("fourth", "player");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            var game = await _repository.GameRepository.Get("player");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: game, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task JoinPlayer_Game_BadRequestAsync()
        {
            PlayerRequest entry = new("join", "25");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            var game = await _repository.GameRepository.Get("25");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: game, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task JoinPlayer_PlayerInGame_BadRequestAsync()
        {
            PlayerRequest entry = new("first", "fourth");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fourth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task JoinPlayer_GameFull_BadRequestAsync()
        {
            PlayerRequest entry = new("second", "25");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            var game = await _repository.GameRepository.Get("25");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: game, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task JoinPlayer_OwnGame_BadRequestAsync()
        {
            PlayerRequest entry = new("first", "first");

            ActionResult<HttpResponseMessage>? result = await _controller.Join(entry);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Delete_OKAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Delete(new ID { Token = "fourth" });
            HttpResponseMessage? response = result?.Value;

            var game = await _repository.GameRepository.Get("fourth");

            if (response is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: game, Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Delete_Game_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Delete(new("zero"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Delete_Player_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Delete(new("sixteennnn"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Delete_Playing_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Delete(new ID { Token = "second" });
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Delete_SecondPlayer_BadRequestAsync()
        {
            await _repository.GameRepository.JoinPlayer(new("zero", "fifth"));

            ActionResult<HttpResponseMessage>? result = await _controller.Delete(new("fifth"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Move_OKAsync()
        {
            GameMove action = new("second");

            ActionResult<HttpResponseMessage>? result = await _controller.Move(action);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
            {
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Move_Finished_OKAsync()
        {
            GameMove action = new("fifth", 0, 2);
            ActionResult<HttpResponseMessage>? result = await _controller.Move(action);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
            {
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Move_Game_BadRequestAsync()
        {
            GameMove action = new(new("fourth"));

            ActionResult<HttpResponseMessage>? result = await _controller.Move(action);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Move_Player_BadRequestAsync()
        {
            GameMove action = new(new("zero"));

            ActionResult<HttpResponseMessage>? result = await _controller.Move(action);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Move_IncorrectStatus_BadRequestAsync()
        {
            GameMove action = new(new("nine"));

            ActionResult<HttpResponseMessage>? result = await _controller.Move(action);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Move_NotPlayersTurn_BadRequestAsync()
        {
            GameMove action = new(new("third"));

            ActionResult<HttpResponseMessage>? result = await _controller.Move(action);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Move_NoSecondPlayer_BadRequestAsync()
        {
            GameMove action = new("fourth");

            ActionResult<HttpResponseMessage>? result = await _controller.Move(action);
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Move_NotPossible_InvalidGameOperationAsync()
        {
            GameMove action = new("second", 0, 0);

            var result = await _controller.Move(action);

            Assert.Multiple(() =>
            {
                Assert.That(result.Value?.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(result.Value?.Content.ReadAsStringAsync().Result, Is.EqualTo("Move (0,0) is not possible!"));
            });
        }

        [Test]
        public async Task Pass_FirstPlayer_OKAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("17"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_SecondPlayer_OKAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("15"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_Game_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("fourth"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_Player_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("first"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_StatusIncorrect_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("third"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_PlayersturnIncorrect_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("third"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_FirstPlayerNonExistant_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("third"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_SecondPlayerNonExistant_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("nine"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_FirstPlayerPossibleMoveButTimerPassed_OKAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("second"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Pass_SecondPlayerIncorrectColor_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Pass(new("third"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Forfeit_OKAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Forfeit(new("19"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
            {
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Forfeit_Game_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Forfeit(new("26"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Forfeit_StatusIncorrect_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Forfeit(new("24"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Forfeit_PlayersturnIncorrect_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Forfeit(new("nonexistant"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public async Task Forfeit_BadRequestAsync()
        {
            ActionResult<HttpResponseMessage>? result = await _controller.Forfeit(new("24"));
            HttpResponseMessage? response = result?.Value;

            if (response is not null)
                Assert.That(actual: response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }
    }
}
