using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository _repository;

        public UserController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("view/{token}")]
        public async Task<ActionResult<Home>> View(string token)
        {
            await _repository.PlayerRepository.UpdateActivity(token);
            var response = await _repository.HomeRepository.GetView(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("partial/{token}")]
        public async Task<ActionResult<HomePartial>> Partial(string token)
        {
            var response = await _repository.HomeRepository.GetPartial(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("profile/{token}")]
        public async Task<ActionResult<HomeProfile>> Profile(string token)
        {
            string[] parts = token.Split(' ');
            await _repository.PlayerRepository.UpdateActivity(parts[1]);
            var response = await _repository.HomeRepository.GetProfile(parts[0], parts[1]);

            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
