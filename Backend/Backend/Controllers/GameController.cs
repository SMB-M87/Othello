using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace Backend.Controllers
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
        public ActionResult<HttpResponseMessage> Create([FromBody] GameCreation builder)
        {
            var player = _repository.PlayerRepository.Get(builder.Player.Token);

            if (player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var inGame = _repository.GameRepository.GetPlayersGame(builder.Player.Token);

            if (inGame is not null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            Game game = new(builder.Player, builder.Description);
            _repository.GameRepository.Create(game);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is not null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("join")]
        public ActionResult<HttpResponseMessage> Join([FromBody] GameEntrant entry)
        {
            var game = _repository.GameRepository.Get(entry.Token);
            var player = _repository.PlayerRepository.Get(entry.Player.Token);

            if (game is null || player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var inGame = _repository.GameRepository.GetPlayersGame(entry.Player.Token);
            var challenger = _repository.PlayerRepository.Get(game.First.Token);

            if (inGame is not null || game.Status is not Status.Pending || game.First.Token == entry.Player.Token || challenger is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            _repository.GameRepository.Join(entry);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is not null && respons.Status is Status.Playing && respons.Second.Token == entry.Player.Token)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("join-player")]
        public ActionResult<HttpResponseMessage> JoinPlayer([FromBody] GameEntrant entry)
        {
            var game = _repository.GameRepository.GetPlayersGame(entry.Token);
            var player = _repository.PlayerRepository.Get(entry.Token);
            var entrant = _repository.PlayerRepository.Get(entry.Player.Token);

            if (game is null || player is null || entrant is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var inGame = _repository.GameRepository.GetPlayersGame(entry.Player.Token);

            if (inGame is not null || game.Status is not Status.Pending || game.First.Token == entry.Player.Token)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            _repository.GameRepository.JoinPlayer(entry);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is not null && respons.Status is Status.Playing && respons.Second.Token == entry.Player.Token)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] GameParticipant player)
        {
            var game = _repository.GameRepository.GetPlayersGame(player.Token);
            var participant = _repository.PlayerRepository.Get(player.Token);

            if (game is null || participant is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (game.Status != Status.Pending || game.First.Token != player.Token)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            _repository.GameRepository.Delete(game);
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpGet]
        public ActionResult<List<string>>? DescriptionsOfPendingGames()
        {
            var pendingGames = _repository.GameRepository.GetGames();

            if (pendingGames is not null)
                return pendingGames.FindAll(game => game.Status == Status.Pending).Select(game => game.Description).ToList();
            else
                return null;
        }

        [HttpGet("{token}")]
        public ActionResult<Game>? GameByToken(string token)
        {
            var game = _repository.GameRepository.Get(token);

            if (game is not null)
                return game;
            else
                return null;
        }

        [HttpGet("{token}/player")]
        public ActionResult<Game>? GameByPlayerToken(string token)
        {
            var game = _repository.GameRepository.GetPlayersGame(token);

            if (game is not null)
                return game;
            else
                return null;
        }

        [HttpGet("{token}/turn")]
        public ActionResult<Color>? TurnByToken(string token)
        {
            var game = _repository.GameRepository.Get(token);

            if (game is not null)
                return game.PlayersTurn;
            else
                return null;
        }

        [HttpPut("{token}/move")]
        public ActionResult<HttpResponseMessage> Move([FromBody] GameStep action)
        {
            var game = _repository.GameRepository.GetPlayersGame(action.Participant.Token);
            var player = _repository.PlayerRepository.Get(action.Participant.Token);

            if (game is null || player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var challenger = _repository.PlayerRepository.Get(game.First.Token == action.Participant.Token ? game.Second.Token : game.First.Token);

            if (game.Status != Status.Playing || game.PlayersTurn != action.Participant.Color || challenger is null ||
               (game.First.Token == action.Participant.Token && game.First.Color != action.Participant.Color) ||
               (game.Second.Token == action.Participant.Token && game.Second.Color != action.Participant.Color))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            if (game.Finished())
            {
                game.Status = Status.Finished;
                _repository.GameRepository.Update(game);

                Color win = game.WinningColor();
                string winner = win == Color.None ? "" : win == game.First.Color ? game.First.Token : game.Second.Token;
                string loser = win == Color.None ? "" : win == game.First.Color ? game.Second.Token : game.First.Token;
                string draw = win == Color.None ? $"{game.First.Token} {game.Second.Token}" : "";
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
            var respons = _repository.GameRepository.Get(game.Token);

            if (respons is not null && respons.PlayersTurn != action.Participant.Color)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPut("pass")]
        public ActionResult<HttpResponseMessage> Pass([FromBody] GameParticipant player)
        {
            var game = _repository.GameRepository.GetPlayersGame(player.Token);
            var participant = _repository.PlayerRepository.Get(player.Token);

            if (game is null || participant is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            var challenger = _repository.PlayerRepository.Get(game.First.Token == participant.Token ? game.Second.Token : game.First.Token);

            if (game.Status != Status.Playing || game.PlayersTurn != player.Color || challenger is null ||
               (game.First.Token == player.Token && game.First.Color != player.Color) ||
               (game.Second.Token == player.Token && game.Second.Color != player.Color))
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

            if (respons is not null && respons.PlayersTurn != player.Color)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPut("forfeit")]
        public ActionResult<HttpResponseMessage> Forfeit([FromBody] GameParticipant player)
        {
            var game = _repository.GameRepository.GetPlayersGame(player.Token);

            if (game is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (game.Status != Status.Playing || game.PlayersTurn != player.Color ||
               (game.First.Token == player.Token && game.First.Color != player.Color) ||
               (game.Second.Token == player.Token && game.Second.Color != player.Color))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            string winner;
            string loser;
            if (game.First.Token == player.Token)
            {
                winner = game.Second.Token;
                loser = player.Token;
            }
            else
            {
                winner = game.First.Token;
                loser = player.Token;
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
