using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.User)]
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IRepository _repository;

        public GameController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{token}")]
        public async Task<ActionResult<Status>> StatusByToken(string token)
        {
            var response = await _repository.GameRepository.GetStatusByPlayersToken(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/StatusByToken", $"Failed to fetch game status from game {token} out of the game database within the game controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Game/StatusByToken", $"Fetched game status from game {token} with status {response} from the game database within the game controller.")
            );

            return Ok(response);
        }

        [HttpGet("view/{token}")]
        public async Task<ActionResult<GamePlay>> View(string token)
        {
            var response = await _repository.GameRepository.GetView(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/GetView", $"Failed to fetch game view of player {token} from game database within the game controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Game/GetView", $"Fetch game view from player {token} from the game database within the game controller.")
            );

            return Ok(response);
        }

        [HttpGet("partial/{token}")]
        public async Task<ActionResult<GamePartial>> Partial(string token)
        {
            var response = await _repository.GameRepository.GetPartial(token);

            if (response is null)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/GetPartial", $"Failed to fetch game partial of player {token} from game database within the game controller.")
                );
                return NotFound();
            }

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Game/GetPartial", $"Fetch game partial from player {token} from game database within the game controller.")
            );

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<HttpResponseMessage>> Create([FromBody] GameCreation game)
        {
            var response = await _repository.GameRepository.Create(game);

            if (response == true)
            {
                await _repository.PlayerRepository.UpdateActivity(game.PlayerToken);
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Game/Create", $"Player {game.PlayerToken} created game within the game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/Create", $"Player {game.PlayerToken} failed to creat game within the game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("join")]
        public async Task<ActionResult<HttpResponseMessage>> Join([FromBody] PlayerRequest request)
        {
            var response = await _repository.GameRepository.JoinPlayer(request);

            if (response == true)
            {
                var player = await _repository.PlayerRepository.GetByName(request.ReceiverUsername);

                if (player is not null)
                {
                    await _repository.PlayerRepository.UpdateActivity(player.Token);
                    await _repository.PlayerRepository.UpdateActivity(request.SenderToken);
                    await _repository.LogRepository.Create(
                        new(User?.Identity?.Name ?? "Anonymous", "Game/Join", $"Player {request.SenderToken} joined {request.ReceiverUsername}'s game from within the game controller.")
                    );
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
            }
            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/Join", $"Player {request.SenderToken} failed to join {request.ReceiverUsername}'s game from within the game controller.")
            );
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("move")]
        public async Task<ActionResult<HttpResponseMessage>> Move([FromBody] GameMove action)
        {
            var (succeded, error) = await _repository.GameRepository.Move(action);

            if (succeded)
            {
                await _repository.PlayerRepository.UpdateActivity(action.PlayerToken);
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Game/Move", $"Player {action.PlayerToken} made move ({action.Row},{action.Column}) from within game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/Move", $"Player {action.PlayerToken} failed to make move ({action.Row},{action.Column}) from within the game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(error ?? "Move not possible.")
                };
            }
        }

        [HttpPost("pass")]
        public async Task<ActionResult<HttpResponseMessage>> Pass([FromBody] ID id)
        {
            var (succeded, error) = await _repository.GameRepository.Pass(id.Token);

            if (succeded)
            {
                await _repository.PlayerRepository.UpdateActivity(id.Token);
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Game/Pass", $"Player {id.Token} passed his turn from within the game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/Pass", $"Player {id.Token} passed his turn from within the game controller.")
                ); 
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(error ?? "Pass not possible.")
                };
            }
        }

        [HttpPost("forfeit")]
        public async Task<ActionResult<HttpResponseMessage>> Forfeit([FromBody] ID id)
        {
            var response = await _repository.GameRepository.Forfeit(id.Token);

            if (response == true)
            {
                await _repository.PlayerRepository.UpdateActivity(id.Token);
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Game/Forfeit", $"Player {id.Token} forfeited the game from within the game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/Forfeit", $"Player {id.Token} failed to forfeit the game from within the game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<HttpResponseMessage>> Delete([FromBody] ID id)
        {
            var response = await _repository.GameRepository.Delete(id.Token);

            if (response == true)
            {
                await _repository.PlayerRepository.UpdateActivity(id.Token);
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Game/Delete", $"Player {id.Token} deleted the game from within the game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Game/Delete", $"Player {id.Token} failed to delete the game from within the game controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
