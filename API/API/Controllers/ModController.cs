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
            var name = User?.Identity?.Name ?? "Anonymous";

            var response = await _repository.PlayerRepository.GetPlayers();

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Mod/GetPlayers", $"Tryed to fetch all players from player database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Mod/GetPlayers", $"Fetched all players from player database through the mod view.")
            );

            return Ok(response);
        }

        [HttpGet("player/{token}")]
        public async Task<ActionResult<List<Player>>> GetPlayer(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var response = await _repository.PlayerRepository.Get(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Mod/GetPlayer", $"Tryed to fetch data from player {token} out of the player database through the mod view but failed.")
                );
                return NotFound();
            }

            List<Player> result = new()
            {
                response
            };

            await _repository.LogRepository.Create(
                new(name, "Mod/GetPlayer", $"Fetched data from player {token} from player database through the mod view.")
            );

            return Ok(result);
        }

        [HttpGet("player/name/{username}")]
        public async Task<ActionResult<List<Player>>> GetPlayerByName(string username)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var response = await _repository.PlayerRepository.GetByName(username);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Mod/GetPlayerByName", $"Tryed to fetch data from player {username} out of the player database through the mod view but failed.")
                );
                return NotFound();
            }

            List<Player> result = new()
            {
                response
            };

            await _repository.LogRepository.Create(
                new(name, "Mod/GetPlayerByName", $"Fetched data from player {username} out of the player database through the mod view.")
            );

            return Ok(result);
        }

        [HttpGet("game/{token}")]
        public async Task<ActionResult<List<Game>>> GetGame(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var response = await _repository.GameRepository.Get(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Mod/GetGame", $"Tryed to fetch data from game {token} out of the game database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Mod/GetGame", $"Fetched data from game {token} out of the game database through the mod view.")
            );

            return Ok(response);
        }

        [HttpGet("game")]
        public async Task<ActionResult<List<Game>>> GetGames()
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var response = await _repository.GameRepository.GetGames();

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Mod/GetGames", $"Tryed to fetch all games from game database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Mod/GetGames", $"Fetched all games from game database through the mod view.")
            );

            return Ok(response);
        }

        [HttpGet("result")]
        public async Task<ActionResult<List<GameResult>>> GetResults()
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var response = await _repository.ResultRepository.GetResults();

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Mod/GetResults", $"Tryed to fetch all game result from result database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Mod/GetResults", $"Fetched all game result from result database through the mod view.")
            );

            return Ok(response);
        }

        [HttpPost("log")]
        public async Task Log([FromBody] PlayerLog log)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            await _repository.LogRepository.Create(log);
        }

        [HttpGet("logs/{token}")]
        public async Task<ActionResult<List<PlayerLog>>> GetLogs(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var response = await _repository.LogRepository.GetLogs(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Mod/GetLogs", $"Tryed to fetch {(token == "null" ? "all logs" : "log(s) from" + token)} out of the log log database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Mod/GetLogs", $"Fetched {(token == "null" ? "all logs" : "log(s) from" + token)} out of the log database through the mod view.")
            );

            return Ok(response);
        }

        [HttpGet("log/{token}")]
        public async Task<ActionResult<PlayerLog?>> GetLog(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var response = await _repository.LogRepository.Get(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Mod/GetLog", $"Tryed to fetch data from log {token} out of the log database through the mod view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Mod/GetLog", $"Fetched data from log {token} out of the log database through the mod view.")
            );

            return Ok(response);
        }
    }
}
