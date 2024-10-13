using Backend.Data;
using Backend.Models;
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

        [HttpGet("{token}")]
        public ActionResult<List<GameResult>>? MatchHistory(string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is not null)
                return _repository.ResultRepository.GetPlayersMatchHistory(token);
            else
                return null;
        }

        [HttpGet("{token}/stats")]
        public ActionResult<(int Wins, int Losses, int Draws)>? PlayerStats(string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is not null)
                return _repository.ResultRepository.GetPlayerStats(token);
            else
                return null;
        }
    }
}
