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
            var response = await _repository.ResultRepository.Get(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Result/Get", $"Failed to fetch data from game result {token} out of the result database within the result controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Result/Get", $"Fetched data from game result {token} out of the result database within the result controller.")
            );

            return Ok(response);
        }

        [HttpGet("last/{player_token}")]
        public async Task<ActionResult<GameResult>> GetLast(string player_token)
        {
            var response = await _repository.ResultRepository.GetLast(player_token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Result/GetLast", $"Player {player_token} failed to fetch last game result data out of the result database within the result controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Result/GetLast", $"Player {player_token} fetched data from the last game result {response.Token} ouf of the result database within the result controller.")
            );

            return Ok(response);
        }
    }
}
