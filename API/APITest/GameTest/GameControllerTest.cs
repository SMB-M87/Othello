using API.Data;
using API.Models;
using System.Net;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            GameResult result0 = new("-3", "second", "third");
            GameResult result1 = new("-2", "third", "second");
            GameResult result2 = new("-1", "second", "third");

            _repository.ResultRepository.Create(result0);
            _repository.ResultRepository.Create(result1);
            _repository.ResultRepository.Create(result2);

            _controller = new GameController(_repository);
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
            GameCreation create = new("26");

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("26"), Is.Not.Null);
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_Player_BadRequest()
        {
            GameCreation create = new("player");

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("player"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_PlayerInGame_BadRequest()
        {
            GameCreation create = new("third");

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("third"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_OK()
        {
            GameEntrant entry = new("two", "25");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("fifth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_Player_BadRequest()
        {
            GameEntrant entry = new("two", "player");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("player"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_Game_BadRequest()
        {
            GameEntrant entry = new("join", "25");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("25"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_PlayerInGame_BadRequest()
        {
            GameEntrant entry = new("zero", "fourth");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("fourth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_GameFull_BadRequest()
        {
            GameEntrant entry = new("one", "25");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("25"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_OwnGame_BadRequest()
        {
            GameEntrant entry = new("zero", "first");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_FirstPlayerNonExistant_BadRequest()
        {
            GameEntrant entry = new("three", "25");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("25"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_OK()
        {
            GameEntrant entry = new("fourth", "25");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("25"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_Player_BadRequest()
        {
            GameEntrant entry = new("fourth", "player");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("player"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_Game_BadRequest()
        {
            GameEntrant entry = new("join", "25");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("25"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_PlayerInGame_BadRequest()
        {
            GameEntrant entry = new("first", "fourth");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("fourth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_GameFull_BadRequest()
        {
            GameEntrant entry = new("second", "25");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("25"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_OwnGame_BadRequest()
        {
            GameEntrant entry = new("first", "first");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete("fourth");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetGameTokenByPlayersToken("fourth"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_Game_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(new("zero"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_Player_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(new("sixteennnn"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_Playing_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete("second");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_SecondPlayer_BadRequest()
        {
            _repository.GameRepository.Join(new("zero", "fifth"));

            ActionResult<HttpResponseMessage>? result = _controller.Delete("fifth");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void DescriptionsOfPendingGames_Correct()
        {
            var result = _controller.DescriptionsOfPendingGames();

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            var results = okResult?.Value as List<GameDescription>;

            if (results is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: results[0].Description, Is.EqualTo("I wanna play a game and don't have any requirements!"));
                    Assert.That(actual: results[1].Description, Is.EqualTo("I wanna play a game and don't have any requirements!"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GameByPlayerToken_Correct()
        {
            var result = _controller.GameTokenByPlayerToken("fourth");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            if (okResult is not null)
            {
                var results = (string?)okResult?.Value;
                if (results is not null)
                    Assert.That(actual: results, Is.EqualTo(expected: "two"));
                else
                    Assert.Fail("Respons is null.");
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GameByPlayerToken_Incorrect()
        {
            ActionResult<string>? result = _controller.GameTokenByPlayerToken("dsahg");
            string? respons = result?.Value;

            if (respons is null)
                Assert.That(actual: respons, Is.EqualTo(expected: null));
            else
                Assert.Fail("Respons is not null.");
        }

        [Test]
        public void TurnByToken_Correct()
        {
            var result = _controller.TurnByToken("fourth");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            if (okResult is not null)
                Assert.That(actual: okResult.Value, Is.EqualTo(expected: Color.Black));
        }

        [Test]
        public void TurnByToken_Incorrect()
        {

            var result = _controller.TurnByToken("nonexistent-token");

            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>(), "Expected NotFound result");

        }

        [Test]
        public void Move_OK()
        {
            GameStep action = new("second");

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
            {
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_Finished_OK()
        {
            GameStep action = new("fifth", 0,2);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.ResultRepository.GetPlayerStats("14"), Is.EqualTo("Wins:0\t\tLosses:0\t\tDraws:0"));
                Assert.That(actual: _repository.ResultRepository.GetPlayerStats("fifth"), Is.EqualTo("Wins:0\t\tLosses:0\t\tDraws:0"));
            });

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.ResultRepository.GetPlayerStats("14"), Is.EqualTo("Wins:0\t\tLosses:1\t\tDraws:0"));
                    Assert.That(actual: _repository.ResultRepository.GetPlayerStats("fifth"), Is.EqualTo("Wins:1\t\tLosses:0\t\tDraws:0"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_Game_BadRequest()
        {
            GameStep action = new(new("fourth"));

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_Player_BadRequest()
        {
            GameStep action = new(new("zero"));

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_IncorrectStatus_BadRequest()
        {
            GameStep action = new(new("nine"));

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_NotPlayersTurn_BadRequest()
        {
            GameStep action = new(new("third"));

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_NoSecondPlayer_BadRequest()
        {
            GameStep action = new("fourth");

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_NotPossible_InvalidGameOperation()
        {
            GameStep action = new("second", 0, 0);

            var result = _controller.Move(action);

            Assert.Multiple(() =>
            {
                Assert.That(result.Value.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(result.Value.Content.ReadAsStringAsync().Result, Is.EqualTo("Move (0,0) is not possible!"));  // Expected error message
            });
        }

        [Test]
        public void Pass_FirstPlayer_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("17");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_SecondPlayer_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("15");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_Game_NotFound()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass(new("fourth"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_Player_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass(new("first"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_StatusIncorrect_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass(new("third"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_PlayersturnIncorrect_NotFound()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("third");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_FirstPlayerNonExistant_NotFound()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("third");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_SecondPlayerNonExistant_NotFound()
        {
             ActionResult<HttpResponseMessage>? result = _controller.Pass("nine");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_FirstPlayerPossibleMove_NoTFound()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("second");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_SecondPlayerIncorrectColor_NotFound()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("third");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_OK()
        {
            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.ResultRepository.GetPlayerStats("19"), Is.EqualTo("Wins:0\t\tLosses:0\t\tDraws:0"));
                Assert.That(actual: _repository.ResultRepository.GetPlayerStats("20"), Is.EqualTo("Wins:0\t\tLosses:0\t\tDraws:0"));
            });

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit("19");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.ResultRepository.GetPlayerStats("20"), Is.EqualTo("Wins:1\t\tLosses:0\t\tDraws:0"));
                    Assert.That(actual: _repository.ResultRepository.GetPlayerStats("19"), Is.EqualTo("Wins:0\t\tLosses:1\t\tDraws:0"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_Game_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(new("26"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_StatusIncorrect_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Forfeit("24");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_PlayersturnIncorrect_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Forfeit("nonexistant");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_BadRequest()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Forfeit("24");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }
    }
}
