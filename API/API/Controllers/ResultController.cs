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
        public ActionResult<GameResult> Get(string token)
        {
            var response = _repository.ResultRepository.Get(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("last/{player_token}")]
        public ActionResult<GameResult> GetLast(string player_token)
        {
            var response = _repository.ResultRepository.GetLast(player_token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
