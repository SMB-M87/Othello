using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/check")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly IRepository _repository;

        public CheckController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<Home>> Check(string username)
        {
            bool response = await _repository.PlayerRepository.UsernameExists(username);

            if (response == false)
            {
                await _repository.LogRepository.Create(
                    new("Anonymous", "FAIL:Check/Check", $"Failed to find player with username {username} from player database within the check controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new("Anonymous", "Check/Check", $"Found player with username {username} from player database within the check controller.")
            );

            return Ok(response);
        }
    }
}
