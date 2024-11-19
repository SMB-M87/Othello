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
        public async Task<ActionResult<GameResult>> Get(string token)
        {
            var response = await _repository.ResultRepository.Get(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("last/{player_token}")]
        public async Task<ActionResult<GameResult>> GetLast(string player_token)
        {
            var response = await _repository.ResultRepository.GetLast(player_token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
