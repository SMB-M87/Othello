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

        [HttpPost("create")]
        public ActionResult<HttpResponseMessage> Create([FromBody] GameCreation game)
        {
            var response = _repository.GameRepository.Create(game);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult<List<GamePending>> DescriptionsOfPendingGames()
        {
            var response = _repository.GameRepository.GetPendingGames();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{token}")]
        public ActionResult<string> GameTokenByPlayerToken(string token)
        {
            var response = _repository.GameRepository.GetGameTokenByPlayersToken(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("opponent/{token}")]
        public ActionResult<string> OpponentByPlayerToken(string token)
        {
            var response = _repository.GameRepository.GetOpponentByPlayersToken(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("turn/{token}")]
        public ActionResult<Color> TurnByToken(string token)
        {
            var response = _repository.GameRepository.GetPlayersTurnByPlayersToken(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("status/{token}")]
        public ActionResult<Status> StatusByToken(string token)
        {
            var response = _repository.GameRepository.GetStatusByPlayersToken(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("color/{token}")]
        public ActionResult<Status> ColorByToken(string token)
        {
            var response = _repository.GameRepository.GetColorByPlayersToken(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("board/{token}")]
        public ActionResult<Color[,]> BoardByToken(string token)
        {
            var response = _repository.GameRepository.GetBoardByPlayersToken(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("join")]
        public ActionResult<HttpResponseMessage> Join([FromBody] PlayerRequest request)
        {
            var response = _repository.GameRepository.JoinPlayer(request);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("move")]
        public ActionResult<HttpResponseMessage> Move([FromBody] GameMove action)
        {
            var (succeded, error) = _repository.GameRepository.Move(action);

            if (succeded)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
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
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] ID id)
        {
            var response = _repository.GameRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
