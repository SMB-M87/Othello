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
            var respons = _repository.GameRepository.Create(game);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult<List<GameDescription>> DescriptionsOfPendingGames()
        {
            var respons = _repository.GameRepository.GetPendingGames();

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpGet("{token}")]
        public ActionResult<string> GameTokenByPlayerToken(string token)
        {
            var respons = _repository.GameRepository.GetGameTokenByPlayersToken(token);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpGet("opponent/{token}")]
        public ActionResult<string> OpponentByPlayerToken(string token)
        {
            var respons = _repository.GameRepository.GetOpponentByPlayersToken(token);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpGet("turn/{token}")]
        public ActionResult<Color> TurnByToken(string token)
        {
            var respons = _repository.GameRepository.GetPlayersTurnByPlayersToken(token);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpGet("status/{token}")]
        public ActionResult<Status> StatusByToken(string token)
        {
            var respons = _repository.GameRepository.GetStatusByPlayersToken(token);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpGet("color/{token}")]
        public ActionResult<Status> ColorByToken(string token)
        {
            var respons = _repository.GameRepository.GetColorByPlayersToken(token);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpGet("board/{token}")]
        public ActionResult<Color[,]> BoardByToken(string token)
        {
            var respons = _repository.GameRepository.GetBoardByPlayersToken(token);

            if (respons is null)
                return NotFound();

            return Ok(respons);
        }

        [HttpPut("join")]
        public ActionResult<HttpResponseMessage> Join([FromBody] PlayerRequest request)
        {
            var respons = _repository.GameRepository.JoinPlayer(request);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("move")]
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

        [HttpPut("pass")]
        public ActionResult<HttpResponseMessage> Pass([FromBody] string player_token)
        {
            var respons = _repository.GameRepository.Pass(player_token, out string error_message);

            if (respons)
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

        [HttpPut("forfeit")]
        public ActionResult<HttpResponseMessage> Forfeit([FromBody] string player_token)
        {
            var respons = _repository.GameRepository.Forfeit(player_token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpDelete("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] string player_token)
        {
            var respons = _repository.GameRepository.Delete(player_token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
