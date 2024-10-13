﻿using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace PlayerTest
{
    [TestFixture]
    public class RepositoryTest
    {
        private Database _context;
        private IRepository _repository;

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
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void Create_Correct()
        {
            int size = _context.Players.Count();
            Player player = new("fivess") { Token = "sfifth" };

            _repository.PlayerRepository.Create(player);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Players.Count(), Is.Not.EqualTo(expected: size));
                Assert.That(actual: _context.Players.Count(), Is.EqualTo(expected: size + 1));
            });
        }

        [Test]
        public void Update_Correct()
        {
            Player player = _context.Players.First(p => p.Username == "one");
            player.Username = "Umugud";
            player.Token = "Waaaah";

            _repository.PlayerRepository.Update(player);

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Players.First(p => p.Username == "one").Username, Is.EqualTo(expected: "Umugud"));
                Assert.That(actual: _context.Players.First(p => p.Username == "one").Token, Is.EqualTo(expected: "Waaaah"));
            });
        }

        [Test]
        public void Delete_Correct()
        {
            int size = _context.Players.Count();
            Player player = new("five") { Token = "fifth" };

            _repository.PlayerRepository.Delete(_context.Players.First(p => p.Username == "one"));

            Assert.Multiple(() =>
            {
                Assert.That(actual: _context.Players.Count(), Is.Not.EqualTo(expected: size));
                Assert.That(actual: size, Is.EqualTo(6));
                Assert.That(actual: _context.Players.Count(), Is.EqualTo(expected: size - 1));
            });
        }

        [Test]
        public void Get_Correct()
        {
            var respons = _repository.PlayerRepository.Get("first");

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: "first"));
            else
                Assert.Fail("Respons is null.");
        }


        [Test]
        public void GetByUsername_Correct()
        {
            var respons = _repository.PlayerRepository.GetByUsername("one");

            if (respons is not null)
                Assert.That(actual: respons.Token, Is.EqualTo(expected: "first"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void GetName_Correct()
        {
            var respons = _repository.PlayerRepository.GetName("first");

            if (respons is not null)
                Assert.That(actual: respons, Is.EqualTo(expected: "one"));
            else
                Assert.Fail("Respons is null.");
        }

        [Test]
        public void SendFriendInvite_Correct()
        {
            Player player1 = _context.Players.First(p => p.Username == "one");
            Player player2 = _context.Players.First(p => p.Username == "two");

            _repository.PlayerRepository.SendFriendInvite("one", "two");

            Assert.Multiple(() =>
            {
                Assert.That(player1.PendingFriends, Does.Contain("two"));
                Assert.That(player2.PendingFriends, Does.Contain("one"));
            });
        }

        [Test]
        public void AcceptFriendInvite_Correct()
        {
            Player player1 = _context.Players.First(p => p.Username == "one");
            Player player2 = _context.Players.First(p => p.Username == "two");

            _repository.PlayerRepository.SendFriendInvite("one", "two");
            _repository.PlayerRepository.AcceptFriendInvite("one", "two");

            Assert.Multiple(() =>
            {
                Assert.That(player1.Friends, Does.Contain("two"));
                Assert.That(player2.Friends, Does.Contain("one"));
                Assert.That(player1.PendingFriends, Does.Not.Contain("two"));
                Assert.That(player2.PendingFriends, Does.Not.Contain("one"));
            });
        }

        [Test]
        public void DeclineFriendInvite_Correct()
        {
            Player player1 = _context.Players.First(p => p.Username == "one");
            Player player2 = _context.Players.First(p => p.Username == "two");

            _repository.PlayerRepository.SendFriendInvite("one", "two");
            _repository.PlayerRepository.DeclineFriendInvite("one", "two");

            Assert.Multiple(() =>
            {
                Assert.That(player1.PendingFriends, Does.Not.Contain("two"));
                Assert.That(player2.PendingFriends, Does.Not.Contain("one"));
            });
        }
    }
}
