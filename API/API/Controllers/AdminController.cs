using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRepository _repository;

        public AdminController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("player")]
        public async Task<ActionResult<List<Player>>> GetPlayers()
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.PlayerRepository.GetPlayers();

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/GetPlayers", $"Tryed to fetch all players from player database through the admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Admin/GetPlayers", $"Fetched all players from player database through admin view.")
            );

            return Ok(response);
        }

        [HttpGet("player/{token}")]
        public async Task<ActionResult<List<Player>>> GetPlayer(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.PlayerRepository.Get(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/GetPlayer", $"Tryed to fetch data from player {token} out of the player database through the admin view but failed.")
                );
                return NotFound();
            }

            List<Player> result = new()
            {
                response
            };

            await _repository.LogRepository.Create(
                new(name, "Admin/GetPlayer", $"Fetch data from player {token} out of the player database through the admin view.")
            );

            return Ok(result);
        }

        [HttpGet("player/name/{username}")]
        public async Task<ActionResult<List<Player>>> GetPlayerByName(string username)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.PlayerRepository.GetByName(username);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/GetPlayerByName", $"Tryed to fetch data from player {username} out of the player database through the admin view but failed.")
                );
                return NotFound();
            }

            List<Player> result = new()
            {
                response
            };

            await _repository.LogRepository.Create(
                new(name, "Admin/GetPlayerByName", $"Fetch data from player {username} out of the player database through the admin view.")
            );

            return Ok(result);
        }

        [HttpPost("player/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeletePlayer([FromBody] ID id)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.PlayerRepository.Delete(id.Token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/DeletePlayer", $"Deleted player {id.Token} from player database through admin view.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "Admin/DeletePlayer", $"Tryed to delete player {id.Token} from player database through admin view but failed.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("game/{token}")]
        public async Task<ActionResult<List<Game>>> GetGame(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.GameRepository.Get(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/GetGame", $"Tryed to fetch data from game {token} out of the game database through the admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Admin/GetGame", $"Fetch data from game {token} out of the game database through the admin view.")
            );

            return Ok(response);
        }

        [HttpGet("game")]
        public async Task<ActionResult<List<Game>>> GetGames()
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.GameRepository.GetGames();

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/GetGames", $"Tryed to fetch all games from game database through the admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Admin/GetGames", $"Fetched all games from game database through the admin view.")
            );

            return Ok(response);
        }

        [HttpPost("game/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteGame([FromBody] ID id)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.GameRepository.PermDelete(id.Token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/DeleteGame", $"Deleted game {id.Token} from game database through the admin view.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "Admin/DeleteGame", $"Tryed to delete game {id.Token} from game database through the admin view but failed.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("result")]
        public async Task<ActionResult<List<GameResult>>> GetResults()
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.ResultRepository.GetResults();

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/GetResults", $"Tryed to fetch all game result from result database through the admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Admin/GetResults", $"Fetched all game result from result database through the admin view.")
            );

            return Ok(response);
        }

        [HttpPost("result/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteResult([FromBody] ID id)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.ResultRepository.Delete(id.Token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/DeleteResult", $"Deleted game result {id.Token} from result database through the admin view.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "Admin/DeleteResult", $"Tryed to delete game result {id.Token} from result database through the admin view but failed.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("log")]
        public async Task Log([FromBody] PlayerLog log)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null && player.Username == log.Username)
            {
                await _repository.PlayerRepository.UpdateActivity(player.Token);
                await _repository.LogRepository.Create(log);
            }
        }

        [HttpGet("logs/{token}")]
        public async Task<ActionResult<List<PlayerLog>>> GetLogs(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.LogRepository.GetLogs(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/GetLogs", $"Tryed to fetch {(token == "null" ? "all logs" : "log(s) from" + token)} out of the log log database through the admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Admin/GetLogs", $"Fetched {(token == "null" ? "all logs" : "log(s) from" + token)} out of the log database through the admin view.")
            );

            return Ok(response);
        }

        [HttpGet("log/{token}")]
        public async Task<ActionResult<PlayerLog?>> GetLog(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var response = await _repository.LogRepository.Get(token);

            var player = await _repository.PlayerRepository.GetByName(name);

            if (player is not null)
                await _repository.PlayerRepository.UpdateActivity(player.Token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Admin/GetLog", $"Tryed to fetch data from log {token} out of the log database through the admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name, "Admin/GetLog", $"Fetched data from log {token} out of the log database through the admin view.")
            );

            return Ok(response);
        }
    }
}
