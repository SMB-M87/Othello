using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.User)]
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
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:User/View", $"Player {token} failed to fetch view from database within the user controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "User/View", $"Player {token} fetched view from player database within the user controller.")
            );

            return Ok(response);
        }

        [HttpGet("partial/{token}")]
        public async Task<ActionResult<HomePartial>> Partial(string token)
        {
            var response = await _repository.HomeRepository.GetPartial(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:User/Partial", $"Player {token} failed to fetch partial from player database within the user controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "User/Partial", $"Player {token} fetched partial from player database within the user controller.")
            );

            return Ok(response);
        }

        [HttpGet("profile/{token}")]
        public async Task<ActionResult<HomeProfile>> Profile(string token)
        {
            string[] parts = token.Split(' ');
            await _repository.PlayerRepository.UpdateActivity(parts[1]);
            var response = await _repository.HomeRepository.GetProfile(parts[0], parts[1]);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:User/Profile", $"Player {parts[1]} failed to fetch profile from player {parts[0]} within the user controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "User/Profile", $"Player {parts[1]} fetched profile from player {parts[0]} within the user controller.")
            );

            return Ok(response);
        }
    }
}
