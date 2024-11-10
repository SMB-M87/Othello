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
        public ActionResult<Status> StatusByToken(string token)
        {
            var response = _repository.GameRepository.GetStatusByPlayersToken(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("view/{token}")]
        public ActionResult<GamePlay> View(string token)
        {
            var response = _repository.GameRepository.GetView(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("partial/{token}")]
        public ActionResult<GamePartial> Partial(string token)
        {
            var response = _repository.GameRepository.GetPartial(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("create")]
        public ActionResult<HttpResponseMessage> Create([FromBody] GameCreation game)
        {
            var response = _repository.GameRepository.Create(game);

            if (response == true)
            {
                _repository.PlayerRepository.UpdateActivity(game.PlayerToken);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("join")]
        public ActionResult<HttpResponseMessage> Join([FromBody] PlayerRequest request)
        {
            var response = _repository.GameRepository.JoinPlayer(request);

            if (response == true)
            {
                var player = _repository.PlayerRepository.GetByName(request.ReceiverUsername);

                if (player is not null)
                {
                    _repository.PlayerRepository.UpdateActivity(player.Token);
                    _repository.PlayerRepository.UpdateActivity(request.SenderToken);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("move")]
        public ActionResult<HttpResponseMessage> Move([FromBody] GameMove action)
        {
            var (succeded, error) = _repository.GameRepository.Move(action);

            if (succeded)
            {
                _repository.PlayerRepository.UpdateActivity(action.PlayerToken);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(error ?? "Move not possible")
                };
        }

        [HttpPost("pass")]
        public ActionResult<HttpResponseMessage> Pass([FromBody] ID id)
        {
            var response = _repository.GameRepository.Pass(id.Token, out string error_message);

            if (response)
            {
                _repository.PlayerRepository.UpdateActivity(id.Token);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else if (!string.IsNullOrEmpty(error_message))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(error_message)
                };
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }
        }

        [HttpPost("forfeit")]
        public ActionResult<HttpResponseMessage> Forfeit([FromBody] ID id)
        {
            var response = _repository.GameRepository.Forfeit(id.Token);

            if (response == true)
            {
                _repository.PlayerRepository.UpdateActivity(id.Token);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] ID id)
        {
            var response = _repository.GameRepository.Delete(id.Token);

            if (response == true)
            {
                _repository.PlayerRepository.UpdateActivity(id.Token);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
