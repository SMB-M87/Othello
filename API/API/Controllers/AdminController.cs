using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
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
        public ActionResult<List<Player>> GetPlayers()
        {
            var response = _repository.PlayerRepository.GetPlayers();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("player/{token}")]
        public ActionResult<List<Player>> GetPlayer(string token)
        {
            var response = _repository.PlayerRepository.Get(token);

            if (response is null)
                return NotFound();

            List<Player> result = new()
            {
                response
            };

            return Ok(result);
        }

        [HttpGet("player/name/{username}")]
        public ActionResult<List<Player>> GetPlayerByName(string username)
        {
            var response = _repository.PlayerRepository.GetByName(username);

            if (response is null)
                return NotFound();

            List<Player> result = new()
            {
                response
            };

            return Ok(result);
        }

        [HttpPost("player/delete")]
        public ActionResult<HttpResponseMessage> DeletePlayer([FromBody] ID id)
        {
            var response = _repository.PlayerRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet("game")]
        public ActionResult<List<Game>> GetGames()
        {
            var response = _repository.GameRepository.GetGames();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("game/delete")]
        public ActionResult<HttpResponseMessage> DeleteGame([FromBody] ID id)
        {
            var response = _repository.GameRepository.PermDelete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet("result")]
        public ActionResult<List<GameResult>> GetResults()
        {
            var response = _repository.ResultRepository.GetResults();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("result/delete")]
        public ActionResult<HttpResponseMessage> DeleteResult([FromBody] ID id)
        {
            var response = _repository.ResultRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
