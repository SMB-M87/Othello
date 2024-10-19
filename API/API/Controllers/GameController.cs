using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IRepository _repository;

        public GameController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public ActionResult<HttpResponseMessage> Create([FromBody] GameCreation creater)
        {
            var player = _repository.PlayerRepository.Get(creater.Player);

            if (player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var inGame = _repository.GameRepository.GetPlayersGame(creater.Player);

            if (inGame is not null && inGame.Status != Status.Finished)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            Game game = new(creater.Player, creater.Description);
            _repository.GameRepository.Create(game);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is not null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("join")]
        public ActionResult<HttpResponseMessage> Join([FromBody] GameEntrant entrant)
        {
            var game = _repository.GameRepository.Get(entrant.Token);
            var player = _repository.PlayerRepository.Get(entrant.Player);

            if (game is null || player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var inGame = _repository.GameRepository.GetPlayersGame(entrant.Player);
            var challenger = _repository.PlayerRepository.Get(game.First);

            if ((inGame is not null && inGame.Status != Status.Finished) || game.Status is not Status.Pending || game.First == entrant.Player || challenger is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            _repository.GameRepository.Join(entrant);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is not null && respons.Status is Status.Playing && respons.Second == entrant.Player)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("join/player")]
        public ActionResult<HttpResponseMessage> JoinPlayer([FromBody] GameEntrant entry)
        {
            var game = _repository.GameRepository.GetPlayersGame(entry.Token);
            var player = _repository.PlayerRepository.Get(entry.Token);
            var entrant = _repository.PlayerRepository.Get(entry.Player);

            if (game is null || player is null || entrant is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var inGame = _repository.GameRepository.GetPlayersGame(entry.Player);

            if ((inGame is not null && inGame.Status != Status.Finished) || game.Status is not Status.Pending || game.First == entry.Player)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            _repository.GameRepository.JoinPlayer(entry);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is not null && respons.Status is Status.Playing && respons.Second == entry.Player)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] string player)
        {
            var game = _repository.GameRepository.GetPlayersGame(player);
            var participant = _repository.PlayerRepository.Get(player);

            if (game is null || participant is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (game.Status != Status.Pending || game.First != player)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            _repository.GameRepository.Delete(game);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpGet]
        public ActionResult<List<GameDescription>> DescriptionsOfPendingGames()
        {
            var games = _repository.GameRepository.GetGames();

            if (games is null)
                return NotFound();

            var pending = games.FindAll(game => game.Status == Status.Pending).ToList();
            if (pending is null)
                return NotFound();

            List<GameDescription> result = new();

            foreach(Game game in pending)
            {
                GameDescription temp = new() { Token = game.Token, Description = game.Description };

                var player = _repository.PlayerRepository.Get(game.First);
                if (player is not null)
                    temp.Player = player.Username;

                var stat = _repository.ResultRepository.GetPlayerStats(game.First);
                if (stat is not null)
                    temp.Stats = stat;

                result.Add(temp);
            }

            return Ok(result);
        }

        [HttpGet("{token}")]
        public ActionResult<Game> GameByToken(string token)
        {
            var game = _repository.GameRepository.Get(token);

            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpGet("from/{token}")]
        public ActionResult<string> GameByPlayerToken(string token)
        {
            var game = _repository.GameRepository.GetPlayersGame(token);

            if (game is null)
                return NotFound();

            return Ok(game.Token);
        }

        [HttpGet("turn/{token}")]
        public ActionResult<Color> TurnByToken(string token)
        {
            var game = _repository.GameRepository.Get(token);

            if (game is null)
                return NotFound();

            return Ok(game.PlayersTurn);
        }

        [HttpPut("move")]
        public ActionResult<HttpResponseMessage> Move([FromBody] GameStep action)
        {
            var game = _repository.GameRepository.GetPlayersGame(action.Player);
            var player = _repository.PlayerRepository.Get(action.Player);

            if (game is null || player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var challenger = _repository.PlayerRepository.Get(game.First == action.Player ? game.Second : game.First);
            Color turn = game.First == action.Player ? game.FColor : game.SColor;

            if (game.Status != Status.Playing || game.PlayersTurn != turn || challenger is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            if (game.Finished())
            {
                game.Status = Status.Finished;
                _repository.GameRepository.Update(game);

                Color win = game.WinningColor();
                string winner = win == Color.None ? "" : win == game.FColor ? game.First : game.Second;
                string loser = win == Color.None ? "" : win == game.FColor ? game.Second : game.First;
                string draw = win == Color.None ? $"{game.First} {game.Second}" : "";
                GameResult result = new(game.Token, winner, loser, draw);
                _repository.ResultRepository.Create(result);

                var res = _repository.GameRepository.Get(game.Token);

                if (res is null)
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                else
                    return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
            }

            if (!game.PossibleMove(action.Y, action.X))
                 return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            game.MakeMove(action.Y, action.X);
            _repository.GameRepository.Update(game);

            if (game.PlayersTurn != turn)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPut("pass")]
        public ActionResult<HttpResponseMessage> Pass([FromBody] string player)
        {
            var game = _repository.GameRepository.GetPlayersGame(player);
            var participant = _repository.PlayerRepository.Get(player);

            if (game is null || participant is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var challenger = _repository.PlayerRepository.Get(game.First == participant.Token ? game.Second : game.First);
            Color turn = game.First == player ? game.FColor : game.SColor;

            if (game.Status != Status.Playing || game.PlayersTurn != turn || challenger is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            try
            {
                game.Pass();
            }
            catch (Exception)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Invalid game operation")
                };
            }

            _repository.GameRepository.Update(game);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is not null && respons.PlayersTurn != turn)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPut("forfeit")]
        public ActionResult<HttpResponseMessage> Forfeit([FromBody] string player)
        {
            var game = _repository.GameRepository.GetPlayersGame(player);

            if (game is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            Color turn = game.First == player ? game.FColor : game.SColor;

            if (game.Status != Status.Playing || game.PlayersTurn != turn)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            string winner;
            string loser;
            if (game.First == player)
            {
                winner = game.Second;
                loser = player;
            }
            else
            {
                winner = game.First;
                loser = player;
            }
            game.Status = Status.Finished;
            game.PlayersTurn = Color.None;
            _repository.GameRepository.Update(game);
            GameResult result = new(game.Token, winner, loser);
            _repository.ResultRepository.Create(result);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }
    }
}
