using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository iRepository;

        public GameController(IGameRepository repository)
        {
            iRepository = repository;
        }

        [HttpGet]
        public ActionResult<List<string>>? DescriptionsOfPendingGames()
        {
            var pendingGames = iRepository.GetGames();

            if (pendingGames is not null)
                return pendingGames.FindAll(game => string.IsNullOrEmpty(game.Second.Token)).Select(game => game.Description).ToList();
            else
                return null;
        }

        [HttpPost("create")]
        public ActionResult<HttpResponseMessage> Create([FromBody] GameCreation builder)
        {
            if (builder.Player.InGame == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            Game game = new(builder.Player, builder.Description);
            iRepository.AddGame(game);
            var respons = iRepository.GetGame(game.Token);

            if (respons is not null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
        }

        [HttpGet("{token}")]
        public ActionResult<Game>? GameByToken(string token)
        {
            var game = iRepository.GetGame(token);

            if (game is not null)
                return game;
            else
                return null;
        }

        [HttpGet("{token}/player")]
        public ActionResult<Game>? GameByPlayerToken(string token)
        {
            var game = iRepository.GetPlayersGame(token);

            if (game is not null)
                return game;
            else
                return null;
        }

        [HttpPost("{token}/join")]
        public ActionResult<HttpResponseMessage> Join([FromRoute] string token, [FromBody] GameEntrant entry)
        {
            var game = iRepository.GetGame(token);

            if (game is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            else
                entry.Token = token;

            if (entry.Player.InGame == true || game.Status == Status.Playing)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            iRepository.JoinGame(entry);
            var respons = iRepository.GetGame(entry.Token);

            if (respons is not null && Status.Playing == respons.Status)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
        }

        [HttpGet("{token}/turn")]
        public ActionResult<Color>? TurnByToken(string token)
        {
            var game = iRepository.GetGame(token);

            if (game is not null)
                return game.PlayersTurn;
            else
                return null;
        }

        [HttpPut("{token}/move")]
        public ActionResult<HttpResponseMessage> Move([FromBody] GameStep action)
        {
            var game = iRepository.GetPlayersGame(action.Participant.Token);

            if (game is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (game.Status != Status.Playing || game.PlayersTurn != action.Participant.Color ||
               (game.First.Token == action.Participant.Token && game.First.Color != action.Participant.Color) ||
               (game.Second.Token == action.Participant.Token && game.Second.Color != action.Participant.Color))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            if (!game.PossibleMove(action.Y, action.X))
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

           game.MakeMove(action.Y, action.X);
           iRepository.UpdateGame(game);
           return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPut("{token}/pass")]
        public ActionResult<HttpResponseMessage> Pass([FromBody] GameParticipant player)
        {
            var game = iRepository.GetPlayersGame(player.Token);

            if (game is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (game.Status != Status.Playing || game.PlayersTurn != player.Color ||
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

            iRepository.UpdateGame(game);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPut("{token}/giveup")]
        public ActionResult<HttpResponseMessage> Forfeit([FromBody] GameParticipant player)
        {
            var game = iRepository.GetPlayersGame(player.Token);

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
            iRepository.UpdateGame(game);
            GameResult result = new(game.Token, winner, loser);
            iRepository.AddResult(result);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost("{token}/delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] GameParticipant player)
        {
            var game = iRepository.GetPlayersGame(player.Token);

            if (game is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (game.Status != Status.Pending || game.First.Token != player.Token)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            iRepository.DeleteGame(game);
            var respons = iRepository.GetGame(game.Token);

            if (respons == null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
