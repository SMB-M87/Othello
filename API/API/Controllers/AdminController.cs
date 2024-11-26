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
            var response = await _repository.PlayerRepository.GetPlayers();

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/GetPlayers", $"Tryed to fetch all players from player database through the admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Admin/GetPlayers", $"Fetched all players from player database through admin view.")
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
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/GetPlayer", $"Tryed to fetch player {token} from player database through admin view but failed.")
                );
                return NotFound();
            }

            List<Player> result = new()
            {
                response
            };

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Admin/GetPlayer", $"Fetch player {token} from player database through admin view.")
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
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/GetPlayerByName", $"Tryed to fetch player {username} from player database through admin view but failed.")
                );
                return NotFound();
            }

            List<Player> result = new()
            {
                response
            };

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Admin/GetPlayerByName", $"Fetch player {username} from player database through admin view.")
            );

            return Ok(result);
        }

        [HttpPost("player/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeletePlayer([FromBody] ID id)
        {
            var response = await _repository.PlayerRepository.Delete(id.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/DeletePlayer", $"Delete player {id.Token} from player database through admin view.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/DeletePlayer", $"Tryed to delete player {id.Token} from player database through admin view but failed.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("game/{token}")]
        public async Task<ActionResult<List<Game>>> GetGame(string token)
        {
            var response = await _repository.GameRepository.Get(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/GetGame", $"Tryed to fetch game {token} from game database through admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Admin/GetGame", $"Fetch game {token} from game database through admin view.")
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
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/GetGames", $"Tryed to fetch all games from game database through admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Admin/GetGames", $"Fetch all games from game database through admin view.")
            );

            return Ok(response);
        }

        [HttpPost("game/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteGame([FromBody] ID id)
        {
            var response = await _repository.GameRepository.PermDelete(id.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/DeleteGame", $"Delete game {id.Token} from game database through admin view.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/DeleteGame", $"Tryed to delete game {id.Token} from game database through admin view but failed.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("result")]
        public async Task<ActionResult<List<GameResult>>> GetResults()
        {
            var response = await _repository.ResultRepository.GetResults();

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/GetResults", $"Tryed to fetch all game result from result database through admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Admin/GetResults", $"Fetch all game result from result database through admin view.")
            );

            return Ok(response);
        }

        [HttpPost("result/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteResult([FromBody] ID id)
        {
            var response = await _repository.ResultRepository.Delete(id.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/DeleteResult", $"Delete game result {id.Token} from result database through admin view.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/DeleteResult", $"Tryed to delete game result {id.Token} from result database through admin view but failed.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("logs/{token}")]
        public async Task<ActionResult<List<PlayerLog>>> GetLogs(string token)
        {
            var response = await _repository.LogRepository.GetLogs(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/GetLogs", $"Tryed to fetch {(token == "null" ? "all logs" : "logs from" + token)} from log database through admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Admin/GetLogs", $"Fetch {(token == "null" ? "all logs" : "logs from" + token)} from log database through admin view.")
            );

            return Ok(response);
        }

        [HttpGet("log/{token}")]
        public async Task<ActionResult<PlayerLog?>> GetLog(string token)
        {
            var response = await _repository.LogRepository.Get(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Admin/GetLog", $"Tryed to fetch log {token} from log database through admin view but failed.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Admin/GetLog", $"Fetch log {token} from log database through admin view.")
            );

            return Ok(response);
        }
    }
}
