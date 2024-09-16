using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Backend.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IRepository _repository;

        public PlayerController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public ActionResult<HttpResponseMessage> Create([FromBody] string username)
        {
            var name = _repository.PlayerRepository.GetPlayerByUsername(username);

            if (name is not null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            Player player = new(username);
            _repository.PlayerRepository.AddPlayer(player);
            var respons = _repository.PlayerRepository.GetPlayer(player.Token);

            if (respons is not null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] string token)
        {
            var player = _repository.PlayerRepository.GetPlayer(token);

            if (player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            _repository.PlayerRepository.DeletePlayer(player);
            var respons = _repository.PlayerRepository.GetPlayer(token);

            if (respons is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpGet("{token}")]
        public ActionResult<Player>? PlayerByToken([FromBody] string token)
        {
            var player = _repository.PlayerRepository.GetPlayer(token);

            if (player is not null)
                return player;
            else
                return null;
        }

        [HttpGet("{token}/username")]
        public ActionResult<Player>? PlayerByUsername([FromBody] string username)
        {
            var player = _repository.PlayerRepository.GetPlayerByUsername(username);

            if (player is not null)
                return player;
            else
                return null;
        }

        [HttpGet("{token}/name")]
        public ActionResult<string>? PlayersName([FromBody] string token)
        {
            var player = _repository.PlayerRepository.GetPlayer(token);

            if (player is not null)
                return player.Username;
            else
                return null;
        }

        [HttpPost("send")]
        public ActionResult<HttpResponseMessage> Send([FromBody] PlayerRequest request)
        {
            _repository.PlayerRepository.SendFriendInvite(request.Username, request.Sender);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost("accept")]
        public ActionResult<HttpResponseMessage> Accept([FromBody] PlayerRequest request)
        {
            _repository.PlayerRepository.AcceptFriendInvite(request.Username, request.Sender);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost("decline")]
        public ActionResult<HttpResponseMessage> Decline([FromBody] PlayerRequest request)
        {
            _repository.PlayerRepository.DeclineFriendInvite(request.Username, request.Sender);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
