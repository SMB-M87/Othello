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

        [HttpPost("player/edit")]
        public ActionResult<HttpResponseMessage> EditPlayer([FromBody] ID id)
        {
            var response = _repository.PlayerRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
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

        [HttpPost("game/edit")]
        public ActionResult<HttpResponseMessage> EditGame([FromBody] ID id)
        {
            var response = _repository.GameRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("game/delete")]
        public ActionResult<HttpResponseMessage> DeleteGame([FromBody] ID id)
        {
            var response = _repository.GameRepository.Delete(id.Token);

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

/*        [HttpPost("result/edit")]
        public ActionResult<HttpResponseMessage> EditResult([FromBody] ID id)
        {
            var response = _repository.ResultRepository.Get(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("result/delete")]
        public ActionResult<HttpResponseMessage> DeleteResult([FromBody] ID id)
        {
            var response = _repository.ResultRepository.Get(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }*/
    }
}
