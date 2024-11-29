using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository _repository;

        public LoginController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{token}")]
        public async Task<ActionResult<bool>> Login(string token)
        {
            var response = await _repository.PlayerRepository.UpdateActivity(token);

            if (response == false)
            {
                await _repository.LogRepository.Create(
                    new(token, "FAIL:Login/Login", $"Player {token} didn't updated the last activity from the player controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(token, "Login/Login", $"Player {token} updated the last activity from the player controller.")
            );

            return Ok(response);
        }
    }
}