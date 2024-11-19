using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
                return NotFound();

            return Ok(response);
        }

        [HttpGet("view/{token}")]
        public async Task<ActionResult<GamePlay>> View(string token)
        {
            var response = await _repository.GameRepository.GetView(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("partial/{token}")]
        public async Task<ActionResult<GamePartial>> Partial(string token)
        {
            var response = await _repository.GameRepository.GetPartial(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<HttpResponseMessage>> Create([FromBody] GameCreation game)
        {
            var response = await _repository.GameRepository.Create(game);

            if (response == true)
            {
                await _repository.PlayerRepository.UpdateActivity(game.PlayerToken);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
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
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("move")]
        public async Task<ActionResult<HttpResponseMessage>> Move([FromBody] GameMove action)
        {
            var (succeded, error) = await _repository.GameRepository.Move(action);

            if (succeded)
            {
                await _repository.PlayerRepository.UpdateActivity(action.PlayerToken);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(error ?? "Move not possible.")
                };
        }

        [HttpPost("pass")]
        public async Task<ActionResult<HttpResponseMessage>> Pass([FromBody] ID id)
        {
            var (succeded, error) = await _repository.GameRepository.Pass(id.Token);

            if (succeded)
            {
                await _repository.PlayerRepository.UpdateActivity(id.Token);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(error ?? "Pass not possible.")
                };
        }

        [HttpPost("forfeit")]
        public async Task<ActionResult<HttpResponseMessage>> Forfeit([FromBody] ID id)
        {
            var response = await _repository.GameRepository.Forfeit(id.Token);

            if (response == true)
            {
                await _repository.PlayerRepository.UpdateActivity(id.Token);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<HttpResponseMessage>> Delete([FromBody] ID id)
        {
            var response = await _repository.GameRepository.Delete(id.Token);

            if (response == true)
            {
                await _repository.PlayerRepository.UpdateActivity(id.Token);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
