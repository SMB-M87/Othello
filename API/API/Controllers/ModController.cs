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
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Mod/GetPlayers", $"Tryed to fetch all players from player database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Mod/GetPlayers", $"Fetched all players from player database through the mod view.")
            );

            return Ok(response);
        }

        [HttpGet("player/{token}")]
        public async Task<ActionResult<List<Player>>> GetPlayer(string token)
        {
            var response = await _repository.PlayerRepository.Get(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Mod/GetPlayer", $"Tryed to fetch data from player {token} out of the player database through the mod view but failed.")
                );
                return NotFound();
            }

            List<Player> result = new()
            {
                response
            };

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Mod/GetPlayer", $"Fetched data from player {token} from player database through the mod view.")
            );

            return Ok(result);
        }

        [HttpGet("player/name/{username}")]
        public async Task<ActionResult<List<Player>>> GetPlayerByName(string username)
        {
            var response = await _repository.PlayerRepository.GetByName(username);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Mod/GetPlayerByName", $"Tryed to fetch data from player {username} out of the player database through the mod view but failed.")
                );
                return NotFound();
            }

            List<Player> result = new()
            {
                response
            };

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Mod/GetPlayerByName", $"Fetched data from player {username} out of the player database through the mod view.")
            );

            return Ok(result);
        }

        [HttpGet("game/{token}")]
        public async Task<ActionResult<List<Game>>> GetGame(string token)
        {
            var response = await _repository.GameRepository.Get(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Mod/GetGame", $"Tryed to fetch data from game {token} out of the game database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Mod/GetGame", $"Fetched data from game {token} out of the game database through the mod view.")
            );

            return Ok(response);
        }

        [HttpGet("game")]
        public async Task<ActionResult<List<Game>>> GetGames()
        {
            var response = await _repository.GameRepository.GetGames();

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Mod/GetGames", $"Tryed to fetch all games from game database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Mod/GetGames", $"Fetched all games from game database through the mod view.")
            );

            return Ok(response);
        }

        [HttpGet("result")]
        public async Task<ActionResult<List<GameResult>>> GetResults()
        {
            var response = await _repository.ResultRepository.GetResults();

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Mod/GetResults", $"Tryed to fetch all game result from result database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Mod/GetResults", $"Fetched all game result from result database through the mod view.")
            );

            return Ok(response);
        }
    }
}
