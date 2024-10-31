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
        public ActionResult<GameResult> GetResult(string token)
        {
            var response = _repository.ResultRepository.Get(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("history/{username}")]
        public ActionResult<List<GameResult>> MatchHistory(string username)
        {
            var response = _repository.ResultRepository.GetPlayersMatchHistory(username);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("stats/{username}")]
        public ActionResult<string> PlayerStats(string username)
        {
            var response = _repository.ResultRepository.GetPlayerStats(username);

            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
