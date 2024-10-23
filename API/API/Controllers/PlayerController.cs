using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
        public ActionResult<HttpResponseMessage> Create([FromBody] GameEntrant player)
        {
            bool respons = _repository.PlayerRepository.Create(new(player.Token, player.Player));

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("activity")]
        public ActionResult<HttpResponseMessage> Activity([FromBody] PlayerActivity player)
        {
            var respons = _repository.PlayerRepository.UpdateActivity(player.Token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else            
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("request/friend")]
        public ActionResult<HttpResponseMessage> FriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.FriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("request/friend/accept")]
        public ActionResult<HttpResponseMessage> AcceptFriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.AcceptFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("request/friend/decline")]
        public ActionResult<HttpResponseMessage> DeclineFriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeclineFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("request/game")]
        public ActionResult<HttpResponseMessage> GameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.GameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("request/game/accept")]
        public ActionResult<HttpResponseMessage> AcceptGameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.AcceptGameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("request/game/decline")]
        public ActionResult<HttpResponseMessage> DeclineGameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeclineGameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("friend/delete")]
        public ActionResult<HttpResponseMessage> DeleteFriend([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeleteFriend(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("game/delete")]
        public ActionResult<HttpResponseMessage> DeleteGameInvites([FromBody] PlayerActivity player)
        {
            var respons = _repository.PlayerRepository.DeleteGameInvites(player.Token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] PlayerActivity player)
        {
            var respons = _repository.PlayerRepository.Delete(player.Token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpGet]
        public ActionResult<List<string>> OnlinePlayers()
        {
            var players = _repository.PlayerRepository.GetOnlinePlayers();

            if (players is null)
                return NotFound();

            return Ok(players);
        }

        [HttpGet("friends/{token}")]
        public ActionResult<List<string>> PlayerFriends(string token)
        {
            var friends = _repository.PlayerRepository.GetFriends(token);

            if (friends is null)
                return NotFound();

            return Ok(friends);
        }

        [HttpGet("requests/{token}")]
        public ActionResult<List<Request>> PlayerRequests(string token)
        {
            var pending = _repository.PlayerRepository.GetRequests(token);

            if (pending is null)
                return NotFound();

            return Ok(pending);
        }

        [HttpGet("requests/sent/{token}")]
        public ActionResult<List<string>> PlayerSent(string token)
        {
            var sent = _repository.PlayerRepository.GetSent(token);

            if (sent is null)
                return NotFound();

            return Ok(sent);
        }
    }
}
