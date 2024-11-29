using API.Data;
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
        public async Task<ActionResult> Check(string username)
        {
            bool response = await _repository.PlayerRepository.UsernameExists(username);

            if (response == false)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
