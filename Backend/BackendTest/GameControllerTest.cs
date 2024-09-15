using System.Net;
using Backend.Data;
using Backend.Models;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest
{
    [TestFixture]
    public class GameControllerTest
    {
        private GameRepository _repository;
        private GameController _controller;

        private Player one;
        private Player two;
        private Player three;
        private Player four;

        private Game game0;
        private Game game1;
        private Game game2;

        [SetUp]
        public void SetUp()
        {
            _repository = new GameRepository();
            _controller = new GameController(_repository);

            one = new("one") { Token = "first" };
            two = new("two") { Token = "second" };
            three = new("three") { Token = "third" };
            four = new("four") { Token = "fourth" };

            game0 = new(one, "I wanna play a game and don't have any requirements.")
            {
                Token = "zero"
            };
            game0.First.Color = Color.Black;
            one.InGame = true;

            game1 = new(two, "I search an advanced player!")
            {
                Token = "one"
            };
            game1.First.Color = Color.Black;
            two.InGame = true;
            game1.Second = new(three.Token)
            {
                Color = Color.White
            };
            three.InGame = true;
            game1.Status = Status.Playing;

            game2 = new(four, "I want to player more than one game against the same adversary.")
            {
                Token = "two"
            };
            game2.First.Color = Color.Black;
            four.InGame = true;

            _repository.Games = new List<Game> { game0, game1, game2 };
            _repository.Players = new List<Player> { one, two , three, four };
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
                    Assert.That(actual: game0.Description, Is.EqualTo(respons[0]));
                    Assert.That(actual: game2.Description, Is.EqualTo(respons[1]));
                    Assert.That(respons.All(res => res != _repository.Games[1].Description));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_OK()
        {
            GameCreation create = new(new("player"));

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Create_FORBIDDEN()
        {
            GameCreation create = new(new("player") { InGame = true });

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
            GameEntrant entry = new("two", new("player"));

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_NOTFOUND()
        {
            GameEntrant entry = new("join", new("player"));

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Join_FORBIDDEN()
        {
            GameEntrant entry = new("zero", new("player") { InGame = true });

            ActionResult<HttpResponseMessage>? result = _controller.Join(entry);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_OK()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(_repository.Games[2].First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Delete_FORBIDDEN()
        {
            ActionResult<HttpResponseMessage>? result = _controller.Delete(_repository.Games[1].Second);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GameByToken_Correct()
        {
            ActionResult<Game>? result = _controller.GameByToken("two");
            Game? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: _repository.Games[2].Token));
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
                Assert.That(actual: respons.First.Token, Is.EqualTo(expected: _repository.Games[2].First.Token));
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
                Assert.That(actual: respons, Is.EqualTo(expected: _repository.Games[2].PlayersTurn));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void TurnByToken_Incorrect()
        {
            ActionResult<Color>? result = _controller.TurnByToken("adfgdh");
            Color? respons = result?.Value;

            if (respons is null)
                Assert.That(actual: respons, Is.EqualTo(expected: null));
            else
                Assert.Fail("Respons is not null.");
        }

        [Test]
        public void Move_OK()
        {
            GameStep action = new(_repository.Games[1].First);

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_FORBIDDEN_NoSecondPlayer()
        {
            GameStep action = new(_repository.Games[2].First);

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_FORBIDDEN_IncorrectPlayer()
        {
            GameStep action = new(_repository.Games[0].First);

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Move_FORBIDDEN_IncorrectPlayerTurn()
        {
            GameStep action = new(_repository.Games[1].Second);

            ActionResult<HttpResponseMessage>? result = _controller.Move(action);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_OK_FirstPlayer()
        {
            Player five = new("five") { Token = "fifth" };
            _repository.Games[2].Second = new(five.Token) { Color = Color.White };
            _repository.Games[2].Status = Status.Playing;

            Game game = _repository.Games[2];
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

            ActionResult<HttpResponseMessage>? result = _controller.Pass(_repository.Games[2].First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_OK_SecondPlayer()
        {
            Player five = new("five") { Token = "fifth" };
            _repository.Games[2].Second = new(five.Token) { Color = Color.White };
            _repository.Games[2].PlayersTurn = _repository.Games[2].Second.Color;
            _repository.Games[2].Status = Status.Playing;

            Game game = _repository.Games[2];
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

            ActionResult<HttpResponseMessage>? result = _controller.Pass(_repository.Games[2].Second);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_BADREQUEST_FirstPlayer()
        {
            Player five = new("five") { Token = "fifth" };
            _repository.Games[2].Second = new(five.Token) { Color = Color.White };
            _repository.Games[2].PlayersTurn = _repository.Games[2].First.Color;
            _repository.Games[2].Status = Status.Playing;

            ActionResult<HttpResponseMessage>? result = _controller.Pass(_repository.Games[2].First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void Pass_BADREQUEST_SecondPlayer()
        {
            Player five = new("five") { Token = "fifth" };
            _repository.Games[2].Second = new(five.Token) { Color = Color.White };
            _repository.Games[2].PlayersTurn = _repository.Games[2].Second.Color;
            _repository.Games[2].Status = Status.Playing;

            ActionResult<HttpResponseMessage>? result = _controller.Pass(_repository.Games[2].Second);
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
            _repository.Games[2].Second = new(five.Token) { Color = Color.White };
            _repository.Games[2].PlayersTurn = _repository.Games[2].First.Color;
            _repository.Games[2].Status = Status.Playing;

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(_repository.Games[2].First);
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
            _repository.Games[2].Second = new(five.Token) { Color = Color.White };
            _repository.Games[2].PlayersTurn = _repository.Games[2].First.Color;
            _repository.Games[2].Status = Status.Pending;

            ActionResult<HttpResponseMessage>? result = _controller.Forfeit(_repository.Games[2].First);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            else
                Assert.Fail("Respons is null.");
        }
    }
}
