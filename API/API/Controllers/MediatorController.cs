using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.Mediator)]
    [Route("api/mediator")]
    [ApiController]
    public class MediatorController : ControllerBase
    {
        private readonly IRepository _repository;

        public MediatorController(IRepository repository)
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

        [HttpGet("game")]
        public ActionResult<List<Game>> GetGames()
        {
            var response = _repository.GameRepository.GetGames();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("result")]
        public ActionResult<List<GameResult>> GetResults()
        {
            var response = _repository.ResultRepository.GetResults();

            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
