using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace APITest.GameTest
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

            Player one = new("first", "one");
            Player two = new("second", "two");
            Player three = new("third", "three");
            Player four = new("fourth", "four");
            Player five = new("fifth", "five");
            Player six = new("sixth", "six");
            Player seven = new("seven", "seven");
            Player eight = new("eight", "eight");
            Player nine = new("nine", "nine");
            Player ten = new("12", "12");
            Player eleven = new("13", "13");
            Player twelve = new("21", "21");
            Player thirdteen = new("31", "31");
            Player fourteen = new("41", "41");
            Player fifteen = new("51", "51");
            Player sixteen = new("61", "61");

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

            Game game = new("null", ten.Token, Color.Black, eleven.Token, Status.Playing, Color.Black);

            Game game0 = new("zero", one.Token);

            Game game1 = new("one", two.Token, Color.Black, three.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game2 = new("two", four.Token, Color.Black);

            Game game3 = new("three", "nonexistant");

            Game game4 = new("four", six.Token, Color.Black, seven.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game5 = new("five", eight.Token, Color.Black, nine.Token, Status.Pending, Color.White, "I search an advanced player!");

            Game game6 = new("six", eight.Token, Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game7 = new("seven", "nonexistant", Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game8 = new("eight", nine.Token, Color.Black, "nonexistant", Status.Playing, Color.White, "I search an advanced player!");

            Game game9 = new("nine", eight.Token, Color.Black, nine.Token, Status.Playing, Color.Black, "I search an advanced player!");

            Game game10 = new("ten", eight.Token, Color.Black, nine.Token, Status.Playing, Color.White, "I search an advanced player!");

            Game game11 = new("11", fourteen.Token, Color.Black);

            Game game12 = new("12", fifteen.Token, Color.Black);

            _repository.GameRepository.Create(game0);
            _repository.GameRepository.Create(game1);
            _repository.GameRepository.Create(game2);
            _repository.GameRepository.Create(game3);
            _repository.GameRepository.Create(game4);
            _repository.GameRepository.Create(game5);
            _repository.GameRepository.Create(game6);
            _repository.GameRepository.Create(game7);
            _repository.GameRepository.Create(game8);
            _repository.GameRepository.Create(game);
            _repository.GameRepository.Create(game9);
            _repository.GameRepository.Create(game10);
            _repository.GameRepository.Create(game11);
            _repository.GameRepository.Create(game12);
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
            int size = _context.Games.ToList().Count;
            Game game = new("-15", "61", Color.Black);

            _repository.GameRepository.Create(game);
            var games = _context.Games.ToList();

            Assert.Multiple(() =>
            {
                Assert.That(actual: games, Has.Count.Not.EqualTo(expected: size));
                Assert.That(actual: games, Has.Count.EqualTo(expected: size + 1));
            });
        }

        [Test]
        public void JoinPlayer_Correct()
        {
            PlayerRequest request = new("four", "fifth");

            _repository.GameRepository.JoinPlayer(request);

            var games = _context.Games.ToList();

            Assert.Multiple(() =>
            {
                Assert.That(actual: games.FirstOrDefault(g => g.Token == "two")?.Status, Is.EqualTo(expected: Status.Playing));
                Assert.That(actual: games.FirstOrDefault(g => g.Token == "two")?.First, Is.EqualTo(expected: "fourth"));
                Assert.That(actual: games.FirstOrDefault(g => g.Token == "two")?.Second, Is.EqualTo(expected: "fifth"));
            });
        }

        [Test]
        public async Task Update_CorrectAsync()
        {
            var player = await _repository.GameRepository.GetPlayersGame("12");
            Assert.That(actual: player?.PlayersTurn, Is.EqualTo(expected: Color.Black));

            await _repository.GameRepository.Move(new("12", 2, 3));

            var game = await _repository.GameRepository.GetPlayersGame("12");

            Assert.Multiple(() =>
            {
                Assert.That(actual: game?.PlayersTurn, Is.EqualTo(expected: Color.White));
                Assert.That(actual: game?.Board?[2, 3], Is.EqualTo(expected: Color.Black));
            });
        }

        [Test]
        public async Task GetPlayersGame_Incorrect()
        {
            string playerToken = "first123456";

            var response = await _repository.GameRepository.GetPlayersGame(playerToken);

            Assert.That(response, Is.Null);
        }
    }
}
