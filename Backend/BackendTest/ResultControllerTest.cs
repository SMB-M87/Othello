using System.Net;
using Backend.Models;
using Backend.Controllers;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ResultTest
{
    [TestFixture]
    public class ControllerTest
    {
        private IRepository _repository;
        private ResultController _controller;

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
            _controller = new ResultController(_repository);
        }

        [Test]
        public void Create_OK()
        {
            GameResult create = new("2", "", "", "second third");

            ActionResult<HttpResponseMessage>? result = _controller.Create(create);
            HttpResponseMessage? respons = result?.Value;

            if (respons is not null)
                Assert.That(actual: respons.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            else
                Assert.Fail("Respons is null.");
        }
    }
}
