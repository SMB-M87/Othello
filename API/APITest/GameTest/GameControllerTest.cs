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
            GameCreation create = new("fifth");

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fifth"), Is.Not.Null);
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_Player_NOTFOUND()
        {
            GameCreation create = new("player");

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("player"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_PlayerInGame_FORBIDDEN()
        {
            GameCreation create = new("third");

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("third"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_OK()
        {
            GameEntrant entry = new("two", "fifth");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fifth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_Player_NOTFOUND()
        {
            GameEntrant entry = new("two", "player");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("player"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_Game_NOTFOUND()
        {
            GameEntrant entry = new("join", "fifth");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fifth"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_PlayerInGame_FORBIDDEN()
        {
            GameEntrant entry = new("zero", "fourth");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fourth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_GameFull_FORBIDDEN()
        {
            GameEntrant entry = new("one", "fifth");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fifth"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_OwnGame_FORBIDDEN()
        {
            GameEntrant entry = new("zero", "first");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("first")?.Second, Is.EqualTo(string.Empty));
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_FirstPlayerNonExistant_FORBIDDEN()
        {
            Game game = _repository.GameRepository.Get("zero") ?? new();
            game.First = "nonexistant";
            _repository.GameRepository.Update(game);
            GameEntrant entry = new("zero", "sixth");

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("sixth"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_OK()
        {
            GameEntrant entry = new("fourth", "fifth");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fifth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_Player_NOTFOUND()
        {
            GameEntrant entry = new("fourth", "player");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("player"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_Game_NOTFOUND()
        {
            GameEntrant entry = new("join", "fifth");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fifth"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_PlayerInGame_FORBIDDEN()
        {
            GameEntrant entry = new("first", "fourth");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fourth"), Is.Not.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_GameFull_FORBIDDEN()
        {
            GameEntrant entry = new("second", "fifth");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fifth"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_OwnGame_FORBIDDEN()
        {
            GameEntrant entry = new("first", "first");

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("first")?.Second, Is.EqualTo(string.Empty));
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(_repository.GameRepository.GetGames()?[2].First ?? "");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetPlayersGame("fourth"), Is.Null);
                });
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_Game_NOTFOUND()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(new("fifth"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_Player_NOTFOUND()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(new("sixteennnn"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_Playing_FORBIDDEN()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(_repository.GameRepository.GetGames()?[1].First ?? "");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_SecondPlayer_FORBIDDEN()
        {
            Game game = _repository.GameRepository.GetGames()?[0] ?? new();
            game.Second = "fifth";
            _repository.GameRepository.Update(game);

            ActionResult<HttpResponseMessage>? result = _controller.Delete(_repository.GameRepository.GetGames()?[0].Second ?? "");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
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
                    Assert.That(actual: _repository.GameRepository.GetGames()?[0].Description, Is.EqualTo(results[0].Description));
                    Assert.That(actual: _repository.GameRepository.GetGames()?[2].Description, Is.EqualTo(results[1].Description));
                    Assert.That(results.All(res => res.Description != _repository.GameRepository.GetGames()?[1].Description));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GameByToken_Correct()
        {
            var result = _controller.GameByToken("two");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            var respons = (Game?)okResult?.Value;

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: _repository.GameRepository.GetGames()?[2].Token));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GameByToken_Incorrect()
        {
            ActionResult<Game>? result = _controller.GameByToken("three");
            Game? respons = result?.Value;

            if (respons is null)
                Assert.That(actual: respons, Is.EqualTo(expected: null));
            else
                Assert.Fail("Respons is not null.");
        }

        [Test]
        public void GameByPlayerToken_Correct()
        {
            var result = _controller.GameByPlayerToken("fourth");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            if (okResult is not null)
            {
                var results = (string?)okResult?.Value;
                if (results is not null)
                    Assert.That(actual: results, Is.EqualTo(expected: _repository.GameRepository.GetGames()?[2].Token));
                else
                    Assert.Fail("Respons is null.");
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GameByPlayerToken_Incorrect()
        {
            ActionResult<string>? result = _controller.GameByPlayerToken("dsahg");
            string? respons = result?.Value;

            if (respons is null)
                Assert.That(actual: respons, Is.EqualTo(expected: null));
            else
                Assert.Fail("Respons is not null.");
        }

        [Test]
        public void TurnByToken_Correct()
        {
            var result = _controller.TurnByToken("two");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected OK result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            if (okResult is not null)
                Assert.That(actual: okResult.Value, Is.EqualTo(expected: _repository.GameRepository.GetGames()?[2].PlayersTurn));
        }

        [Test]
        public void TurnByToken_Incorrect()
        {
            var result = _controller.TurnByToken("two");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected Ok result");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should not be null");

            if (okResult is null)
                Assert.That(actual: okResult.Value, Is.EqualTo(null));
        }

        [Test]
        public void Move_OK()
        {
            GameStep action = new(_repository.GameRepository.GetGames()?[1].First ?? "");

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.GameRepository.GetGames()?[1].Board[3, 2], Is.EqualTo(Color.Black));
                    Assert.That(actual: _repository.GameRepository.GetGames()?[1].Board[3, 3], Is.EqualTo(Color.Black));
                    Assert.That(actual: _repository.GameRepository.GetGames()?[1].Board[3, 4], Is.EqualTo(Color.Black));
                    Assert.That(actual: _repository.GameRepository.GetGames()?[1].Board[4, 3], Is.EqualTo(Color.Black));
                    Assert.That(actual: _repository.GameRepository.GetGames()?[1].Board[4, 4], Is.EqualTo(Color.White));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_Finished_OK()
        {
            GameStep action = new(_repository.GameRepository.GetGames()?[1].Second ?? "");
            Game game = _repository.GameRepository.GetGames()?[1] ?? new();
            game.PlayersTurn = Color.White;
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            game.PlayersTurn = Color.White;

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.ResultRepository.GetPlayerStats("second"), Is.EqualTo("W:2 L:1 D:0"));
                Assert.That(actual: _repository.ResultRepository.GetPlayerStats("third"), Is.EqualTo("W:1 L:2 D:0"));
            });

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.ResultRepository.GetPlayerStats("second"), Is.EqualTo("W:2 L:2 D:0"));
                    Assert.That(actual: _repository.ResultRepository.GetPlayerStats("third"), Is.EqualTo("W:2 L:2 D:0"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_Game_NOTFOUND()
        {
            GameStep action = new(new("fifth"));

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_Player_NOTFOUND()
        {
            GameStep action = new(new("zero"));

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_IncorrectStatus_Forbidden()
        {
            Game game = _repository.GameRepository.GetGames()?[1] ?? new();
            game.Status = Status.Pending;
            GameStep action = new(new("second"));

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_NotPlayersTurn_Forbidden()
        {
            Game game = _repository.GameRepository.GetGames()?[1] ?? new();
            game.PlayersTurn = Color.White;
            GameStep action = new(new("second"));

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_FirstParticipantIncorrectColor_Forbidden()
        {
            Game game = _repository.GameRepository.GetGames()?[1] ?? new();
            game.PlayersTurn = Color.White;
            GameStep action = new("second");

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_SecondParticipantIncorrectColor_Forbidden()
        {
            GameStep action = new("third");

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_NoSecondPlayer_FORBIDDEN()
        {
            GameStep action = new(_repository.GameRepository.GetGames()?[2].First ?? "");

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_NotPossible_BADREQUEST()
        {
            GameStep action = new(_repository.GameRepository.GetGames()?[1].First ?? "", 0, 0);

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_FirstPlayer_OK()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = new(five.Token);
            game.SColor = Color.White;
            game.Status = Status.Playing;
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            _repository.GameRepository.Update(game);

            ActionResult<HttpResponseMessage>? result = _controller.Pass(game.First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_SecondPlayer_OK()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = new(five.Token);
            game.SColor = Color.White;
            game.PlayersTurn = game.SColor;
            game.Status = Status.Playing;
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            _repository.GameRepository.Update(game);

            ActionResult<HttpResponseMessage>? result = _controller.Pass(game.Second);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_Game_NOTFOUND()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass(new("fifth"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_Player_NOTFOUND()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass(new("zero"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_StatusIncorrect_FORBIDDEN()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass(new("first"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_PlayersturnIncorrect_FORBIDDEN()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("third");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_FirstPlayerNonExistant_FORBIDDEN()
        {
            Game game = _repository.GameRepository.GetGames()?[1] ?? new();
            game.First = "nonexistant";
            game.Second = "third";
            game.SColor = Color.White;
            game.PlayersTurn = Color.White;
            game.Status = Status.Playing;
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            _repository.GameRepository.Update(game);

            ActionResult<HttpResponseMessage>? result = _controller.Pass("third");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_SecondPlayerNonExistant_FORBIDDEN()
        {
            Game game = _repository.GameRepository.GetGames()?[1] ?? new();
            game.Second = "nonexistant";
            game.First = "second";
            game.FColor = Color.Black;
            game.PlayersTurn = Color.Black;
            game.Status = Status.Playing;
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            _repository.GameRepository.Update(game);

            ActionResult<HttpResponseMessage>? result = _controller.Pass("second");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_FirstPlayerPossibleMove_BADREQUEST()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("second");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_SecondPlayerIncorrectColor_FORBIDDEN()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Pass("third");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_FirstPlayer_BADREQUEST()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = five.Token;
            game.SColor = Color.White;
            game.PlayersTurn = game.FColor;
            game.Status = Status.Playing;

            ActionResult<HttpResponseMessage>? result = _controller.Pass(game.First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_SecondPlayer_BADREQUEST()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = five.Token;
            game.SColor = Color.White;
            game.PlayersTurn = game.SColor;
            game.Status = Status.Playing;

            ActionResult<HttpResponseMessage>? result = _controller.Pass(game.Second);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_OK()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = five.Token;
            game.SColor = Color.White;
            game.PlayersTurn = game.FColor;
            game.Status = Status.Playing;

            Assert.Multiple(() =>
            {
                Assert.That(actual: _repository.ResultRepository.GetPlayerStats(five.Token), Is.EqualTo("W:0 L:0 D:0"));
                Assert.That(actual: _repository.ResultRepository.GetPlayerStats(game.First), Is.EqualTo("W:0 L:0 D:0"));
            });

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(game.First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(actual: _repository.ResultRepository.GetPlayerStats(five.Token), Is.EqualTo("W:1 L:0 D:0"));
                    Assert.That(actual: _repository.ResultRepository.GetPlayerStats(game.First), Is.EqualTo("W:0 L:1 D:0"));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_Game_NOTFOUND()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(new("fifth"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_StatusIncorrect_FORBIDDEN()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = five.Token;
            game.SColor = Color.White;
            game.PlayersTurn = game.FColor;
            game.Status = Status.Pending;

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(game.First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_PlayersturnIncorrect_FORBIDDEN()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = five.Token;
            game.SColor = Color.White;
            game.PlayersTurn = game.FColor;
            game.Status = Status.Pending;

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(game.Second);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_FirstPlayerIncorrectColor_FORBIDDEN()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = five.Token;
            game.SColor = Color.White;
            game.PlayersTurn = Color.White;
            game.Status = Status.Pending;

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit("fourth");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_SecondPlayerIncorrectColor_FORBIDDEN()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = five.Token;
            game.SColor = Color.White;
            game.PlayersTurn = Color.Black;
            game.Status = Status.Pending;

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit("fifth");
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_FORBIDDEN()
        {
            Player five = new("fifth", "five");
            Game game = _repository.GameRepository.GetGames()?[2] ?? new();
            game.Second = five.Token;
            game.SColor = Color.White;
            game.PlayersTurn = game.FColor;
            game.Status = Status.Pending;

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(game.First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }
    }
}
