using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.User)]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository _repository;

        public UserController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("view/{token}")]
        public async Task<ActionResult<User>> View(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            await _repository.PlayerRepository.UpdateActivity(token);
            var check = await _repository.PlayerRepository.PlayerChecksOut(token, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:User/View/Check", $"Failed to pass the check before player {name} fetched the view data with token {token} within the user controller.")
                );
                return NotFound();
            }

            var response = await _repository.HomeRepository.GetView(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name ?? "Anonymous Entity", "FAIL:User/View", $"Player {token} failed to fetch view from database within the user controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name ?? "Anonymous Entity", "User/View", $"Player {token} fetched view from player database within the user controller.")
            );

            return Ok(response);
        }

        [HttpGet("partial/{token}")]
        public async Task<ActionResult<UserPartial>> Partial(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            var check = await _repository.PlayerRepository.PlayerChecksOut(token, name);

            if (check == false)
            {
                return BadRequest();
            }

            var response = await _repository.HomeRepository.GetPartial(token);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("profile/{token}")]
        public async Task<ActionResult<HomeProfile>> Profile(string token)
        {
            var name = User?.Identity?.Name ?? "Anonymous Entity";

            string[] parts = token.Split(' ');
            await _repository.PlayerRepository.UpdateActivity(parts[1]);

            var check = await _repository.PlayerRepository.PlayerChecksOut(parts[1], name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:User/Profile/Check", $"Failed to pass the check before player {name} fetched the profile data with token {parts[0]} within the user controller.")
                );
                return NotFound();
            }

            var response = await _repository.HomeRepository.GetProfile(parts[0], parts[1]);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(name ?? "Anonymous Entity", "FAIL:User/Profile", $"Player {parts[1]} failed to fetch profile from player {parts[0]} within the user controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(name ?? "Anonymous Entity", "User/Profile", $"Player {parts[1]} fetched profile from player {parts[0]} within the user controller.")
            );

            return Ok(response);
        }
    }
}
