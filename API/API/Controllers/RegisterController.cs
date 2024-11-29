using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRepository _repository;

        public RegisterController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] PlayerRequest player)
        {
            bool response = await _repository.PlayerRepository.Create(new(player.ReceiverUsername, player.SenderToken));

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new("Anonymous", "Register/Create", $"Player {player.ReceiverUsername} with username {player.SenderToken} created in the player database within the register controller.")
                );
                return Ok();
            }
            else
            {
                await _repository.LogRepository.Create(
                    new("Anonymous", "FAIL:Register/Create", $"Failed to create player {player.ReceiverUsername} with username {player.SenderToken} in the player database within the register controller.")
                );
                return BadRequest();
            }
        }
    }
}
