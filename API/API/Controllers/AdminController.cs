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

        [HttpPost("player/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeletePlayer([FromBody] ID id)
        {
            var response = await _repository.PlayerRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
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

        [HttpPost("game/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteGame([FromBody] ID id)
        {
            var response = await _repository.GameRepository.PermDelete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet("result")]
        public async Task<ActionResult<List<GameResult>>> GetResults()
        {
            var response = await _repository.ResultRepository.GetResults();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("result/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteResult([FromBody] ID id)
        {
            var response = await _repository.ResultRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
