using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.Mod)]
    [Route("api/mod")]
    [ApiController]
    public class ModController : ControllerBase
    {
        private readonly IRepository _repository;

        public ModController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("player")]
        public async Task<ActionResult<List<Player>>> GetPlayers()
        {
            var response = await _repository.PlayerRepository.GetPlayers();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("player/{token}")]
        public async Task<ActionResult<List<Player>>> GetPlayer(string token)
        {
            var response = await _repository.PlayerRepository.Get(token);

            if (response is null)
                return NotFound();

            List<Player> result = new()
            {
                response
            };

            return Ok(result);
        }

        [HttpGet("player/name/{username}")]
        public async Task<ActionResult<List<Player>>> GetPlayerByName(string username)
        {
            var response = await _repository.PlayerRepository.GetByName(username);

            if (response is null)
                return NotFound();

            List<Player> result = new()
            {
                response
            };

            return Ok(result);
        }

        [HttpGet("game/{token}")]
        public async Task<ActionResult<List<Game>>> GetGame(string token)
        {
            var response = await _repository.GameRepository.Get(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("game")]
        public async Task<ActionResult<List<Game>>> GetGames()
        {
            var response = await _repository.GameRepository.GetGames();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("result")]
        public async Task<ActionResult<List<GameResult>>> GetResults()
        {
            var response = await _repository.ResultRepository.GetResults();

            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
