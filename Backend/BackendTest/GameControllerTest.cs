using System.Net;
using Backend.Models;
using Backend.Controllers;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest
{
    [TestFixture]
    public class GameControllerTest
    {
        private IRepository _repository;
        private GameController _controller;

        [SetUp]
        public void SetUp()
        {
            _repository = new Repository();

            Player one = new("one") { Token = "first" };
            Player two = new("two") { Token = "second" };
            Player three = new("three") { Token = "third" };
            Player four = new("four") { Token = "fourth" };
            Player five = new("five") { Token = "fifth" };
            Player six = new("six") { Token = "sixth" };

            _repository.PlayerRepository.AddPlayer(one);
            _repository.PlayerRepository.AddPlayer(two);
            _repository.PlayerRepository.AddPlayer(three);
            _repository.PlayerRepository.AddPlayer(four);
            _repository.PlayerRepository.AddPlayer(five);
            _repository.PlayerRepository.AddPlayer(six);

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

            _repository.GameRepository.AddGame(game0);
            _repository.GameRepository.AddGame(game1);
            _repository.GameRepository.AddGame(game2);

            GameResult result0 = new("-3", "second", "third");
            GameResult result1 = new("-2", "third", "second");
            GameResult result2 = new("-1", "second", "third");

            _repository.ResultRepository.Create(result0);
            _repository.ResultRepository.Create(result1);
            _repository.ResultRepository.Create(result2);
            _controller = new GameController(_repository);
        }

        [Test]
        public void Create_OK()
        {
            GameCreation create = new(_repository.Players()[4]);

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_Player_NOTFOUND()
        {
            GameCreation create = new(new("player"));

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_PlayerInGame_FORBIDDEN()
        {
            GameCreation create = new(_repository.Players()[2]);

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_OK()
        {
            GameEntrant entry = new("two", _repository.Players()[4]);

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_Player_NOTFOUND()
        {
            GameEntrant entry = new("two", new("player"));

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_Game_NOTFOUND()
        {
            GameEntrant entry = new("join", _repository.Players()[4]);

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_PlayerInGame_FORBIDDEN()
        {
            GameEntrant entry = new("zero", _repository.Players()[3]);

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_GameFull_FORBIDDEN()
        {
            GameEntrant entry = new("one", _repository.Players()[4]);

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_OwnGame_FORBIDDEN()
        {
            GameEntrant entry = new("zero", _repository.Players()[0]);

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_FirstPlayerNonExistant_FORBIDDEN()
        {
            Game game = _repository.Games()[0];
            game.First.Token = "nonexistant";
            _repository.GameRepository.UpdateGame(game);
            GameEntrant entry = new("zero", _repository.Players()[5]);

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_OK()
        {
            GameEntrant entry = new("fourth", _repository.Players()[4]);

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_Player_NOTFOUND()
        {
            GameEntrant entry = new("fourth", new("player"));

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_Game_NOTFOUND()
        {
            GameEntrant entry = new("join", _repository.Players()[4]);

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_PlayerInGame_FORBIDDEN()
        {
            GameEntrant entry = new("first", _repository.Players()[3]);

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_GameFull_FORBIDDEN()
        {
            GameEntrant entry = new("second", _repository.Players()[4]);

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void JoinPlayer_OwnGame_FORBIDDEN()
        {
            GameEntrant entry = new("first", _repository.Players()[0]);

            ActionResult<HttpResponseMessage>? result = _controller.JoinPlayer(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(_repository.Games()[2].First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
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
            ActionResult<HttpResponseMessage>? result = _controller.Delete(_repository.Games()[1].First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_SecondPlayer_FORBIDDEN()
        {
            Game game = _repository.Games()[0];
            game.Second.Token = "fifth";
            _repository.GameRepository.UpdateGame(game);

            ActionResult<HttpResponseMessage>? result = _controller.Delete(_repository.Games()[0].Second);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void DescriptionsOfPendingGames_Correct()
        {
            ActionResult<List<string>>? result = _controller.DescriptionsOfPendingGames();
            List<string>? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: _repository.Games()[0].Description, Is.EqualTo(respons[0]));
                    Assert.That(actual: _repository.Games()[2].Description, Is.EqualTo(respons[1]));
                    Assert.That(respons.All(res => res != _repository.Games()[1].Description));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GameByToken_Correct()
        {
            ActionResult<Game>? result = _controller.GameByToken("two");
            Game? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: _repository.Games()[2].Token));
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
            ActionResult<Game>? result = _controller.GameByPlayerToken("fourth");
            Game? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.First.Token, Is.EqualTo(expected: _repository.Games()[2].First.Token));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GameByPlayerToken_Incorrect()
        {
            ActionResult<Game>? result = _controller.GameByPlayerToken("dsahg");
            Game? respons = result?.Value;

            if (respons is null)
                Assert.That(actual: respons, Is.EqualTo(expected: null));
            else
                Assert.Fail("Respons is not null.");
        }

        [Test]
        public void TurnByToken_Correct()
        {
            ActionResult<Color>? result = _controller.TurnByToken("two");
            Color? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons, Is.EqualTo(expected: _repository.Games()[2].PlayersTurn));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void TurnByToken_Incorrect()
        {
            ActionResult<Color>? result = _controller.TurnByToken("fifth");
            Color? respons = result?.Value;

            if (respons is null)
                Assert.That(actual: respons, Is.EqualTo(expected: null));
            else
                Assert.Fail("Respons is not null.");
        }

        [Test]
        public void Move_OK()
        {
            GameStep action = new(_repository.Games()[1].First);

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
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
            Game game = _repository.Games()[1];
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
            Game game = _repository.Games()[1];
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
        public void Move_FirstParticipantIsCheating_Forbidden()
        {
            Game game = _repository.Games()[1];
            game.PlayersTurn = Color.White;
            GameStep action = new(new("second") { Color = Color.White });

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_SecondParticipantIsCheating_Forbidden()
        {
            Game game = _repository.Games()[1];
            GameStep action = new(new("third") { Color = Color.Black });

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
            GameStep action = new(_repository.Games()[2].First);

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
            GameStep action = new(_repository.Games()[1].First, 0, 0);

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
            Player five = new("five") { Token = "fifth" };
            Game game = _repository.Games()[2];
            game.Second = new(five.Token) { Color = Color.White };
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
            Player five = new("five") { Token = "fifth" };
            Game game = _repository.Games()[2];
            game.Second = new(five.Token) { Color = Color.White };
            game.PlayersTurn = game.Second.Color;
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
            ActionResult<HttpResponseMessage>? result = _controller.Pass(new("first"));
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_FirstPlayer_BADREQUEST()
        {
            Player five = new("five") { Token = "fifth" };
            Game game = _repository.Games()[2];
            game.Second = new(five.Token) { Color = Color.White };
            game.PlayersTurn = game.First.Color;
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
            Player five = new("five") { Token = "fifth" };
            Game game = _repository.Games()[2];
            game.Second = new(five.Token) { Color = Color.White };
            game.PlayersTurn = game.Second.Color;
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
            Player five = new("five") { Token = "fifth" };
            Game game = _repository.Games()[2];
            game.Second = new(five.Token) { Color = Color.White };
            game.PlayersTurn = game.First.Color;
            game.Status = Status.Playing;

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(game.First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Forfeit_FORBIDDEN()
        {
            Player five = new("five") { Token = "fifth" };
            Game game = _repository.Games()[2];
            game.Second = new(five.Token) { Color = Color.White };
            game.PlayersTurn = game.First.Color;
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
