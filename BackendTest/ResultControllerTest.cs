﻿using Backend.Data;
using Backend.Models;
using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ResultTest
{
    [TestFixture]
    public class ControllerTest
    {
        private Database _context;
        private IRepository _repository;
        private ResultController _controller;

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
            _controller = new ResultController(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void MatchHistory_Correct()
        {
            ActionResult<List<GameResult>>? result = _controller.MatchHistory("second");
            List<GameResult>? respons = result?.Value;

            if (respons is not null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: respons[0], Is.EqualTo(expected: _context.Results.First(p => p.Token == "-3")));
                    Assert.That(actual: respons[1], Is.EqualTo(expected: _context.Results.First(p => p.Token == "-2")));
                    Assert.That(actual: respons[2], Is.EqualTo(expected: _context.Results.First(p => p.Token == "-1")));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void PlayerStats_Correct()
        {
            ActionResult<(int, int, int)>? result = _controller.PlayerStats("second");

            if (result?.Value is (int wins, int losses, int draws))
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual: wins, Is.EqualTo(expected: 2));
                    Assert.That(actual: losses, Is.EqualTo(expected: 1));
                    Assert.That(actual: draws, Is.EqualTo(expected: 0));
                });
            }
            else
                Assert.Fail("Respons is null.");
        }
    }
}
