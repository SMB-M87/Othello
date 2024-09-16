using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/result")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IRepository _repository;

        public ResultController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public ActionResult<HttpResponseMessage> Create([FromBody] GameResult result)
        {
            var game = _repository.GameRepository.GetGame(result.Token);

            if (game is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (game.Status != Status.Finished || game.PlayersTurn != Color.None)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            _repository.ResultRepository.Create(result);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpGet("{token}")]
        public ActionResult<List<GameResult>>? MatchHistory([FromBody] string token)
        {
            var player = _repository.PlayerRepository.GetPlayer(token);

            if (player is not null)
                return _repository.ResultRepository.GetPlayersMatchHistory(token);
            else
                return null;
        }

        [HttpGet("{token}/stats")]
        public ActionResult<(int Wins, int Losses, int Draws)>? GameByPlayerToken([FromBody] string token)
        {
            var player = _repository.PlayerRepository.GetPlayer(token);

            if (player is not null)
                return _repository.ResultRepository.GetPlayerStats(token);
            else
                return null;
        }
    }
}
