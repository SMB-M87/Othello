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
        public ActionResult<GameResult> Result(string token)
        {
            var respons = _repository.ResultRepository.Get(token);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpGet("history/{username}")]
        public ActionResult<List<GameResult>> MatchHistory(string username)
        {
            var respons = _repository.ResultRepository.GetPlayersMatchHistory(username);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpGet("stats/{username}")]
        public ActionResult<string> PlayerStats(string username)
        {
            var respons = _repository.ResultRepository.GetPlayerStats(username);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }
    }
}
