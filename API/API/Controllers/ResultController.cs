using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.User)]
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
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.ResultRepository.Get(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Result/Get", $"Failed to fetch data from game result {token} out of the result database within the result controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Result/Get", $"Fetched data from game result {token} out of the result database within the result controller.")
            );

            return Ok(response);
        }

        [HttpGet("last/{player_token}")]
        public async Task<ActionResult<GameResult>> GetLast(string player_token)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var check = await _repository.PlayerRepository.PlayerChecksOut(player_token, name);

            if (check == false)
            {
                return BadRequest();
            }

            var response = await _repository.ResultRepository.GetLast(player_token);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
