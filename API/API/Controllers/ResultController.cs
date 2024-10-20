using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
        public ActionResult<List<GameResult>> Result(string token)
        {
            var result = _repository.ResultRepository.Get(token);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("history/{token}")]
        public ActionResult<List<GameResult>> MatchHistory(string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is null)
                return NotFound();

            var results = _repository.ResultRepository.GetPlayersMatchHistory(token);
            return Ok(results);
        }

        [HttpGet("stats/{token}")]
        public ActionResult<string> PlayerStats(string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is null)
                return NotFound();

            var stats = _repository.ResultRepository.GetPlayerStats(token);
            return Ok(stats);
        }
    }
}
