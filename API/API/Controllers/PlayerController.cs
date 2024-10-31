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
        public ActionResult<HttpResponseMessage> Create([FromBody] PlayerRequest player)
        {
            bool response = _repository.PlayerRepository.Create(new(player.ReceiverUsername, player.SenderToken));

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult<List<string>> OnlinePlayers()
        {
            var response = _repository.PlayerRepository.GetOnlinePlayers();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("gaming")]
        public ActionResult<List<string>> PlayersInGame()
        {
            var response = _repository.PlayerRepository.GetPlayersInGame();

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("check/{username}")]
        public ActionResult<bool> CheckUsername(string username)
        {
            bool response = _repository.PlayerRepository.UsernameExists(username);

            if (response == false)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("friends/{token}")]
        public ActionResult<List<string>> PlayerFriends(string token)
        {
            var response = _repository.PlayerRepository.GetFriends(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("requests/friend/{token}")]
        public ActionResult<List<string>> PlayerFriendRequests(string token)
        {
            var response = _repository.PlayerRepository.GetFriendRequests(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("requests/game/{token}")]
        public ActionResult<List<string>> PlayerGameRequests(string token)
        {
            var response = _repository.PlayerRepository.GetGameRequests(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("sent/friend/{token}")]
        public ActionResult<List<string>> PlayerSentFriendRequests(string token)
        {
            var response = _repository.PlayerRepository.GetSentFriendRequests(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("sent/game/{token}")]
        public ActionResult<List<string>> GetSentGameRequests(string token)
        {
            var response = _repository.PlayerRepository.GetSentGameRequests(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("last/seen/{username}")]
        public ActionResult<string> GetLastActivity(string username)
        {
            var response = _repository.PlayerRepository.GetLastActivity(username);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("is/friend/{token}")]
        public ActionResult<bool> IsFriend(string token)
        {
            string[] parts = token.Split(' ');
            var response = _repository.PlayerRepository.IsFriend(parts[0], parts[1]);
            return Ok(response);
        }

        [HttpGet("has/received/friend/{token}")]
        public ActionResult<bool> HasReceivedRequest(string token)
        {
            string[] parts = token.Split(' ');
            var response = _repository.PlayerRepository.HasFriendRequest(parts[0], parts[1]);
            return Ok(response);
        }

        [HttpGet("has/sent/friend/{token}")]
        public ActionResult<bool> HasSentRequest(string token)
        {
            string[] parts = token.Split(' ');
            var response = _repository.PlayerRepository.HasSentFriendRequest(parts[0], parts[1]);
            return Ok(response);
        }

        [HttpPost("activity")]
        public ActionResult<HttpResponseMessage> Activity([FromBody] ID id)
        {
            var response = _repository.PlayerRepository.UpdateActivity(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend")]
        public ActionResult<HttpResponseMessage> FriendRequest([FromBody] PlayerRequest request)
        {
            var response = _repository.PlayerRepository.FriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend/accept")]
        public ActionResult<HttpResponseMessage> AcceptFriendRequest([FromBody] PlayerRequest request)
        {
            var response = _repository.PlayerRepository.AcceptFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend/decline")]
        public ActionResult<HttpResponseMessage> DeclineFriendRequest([FromBody] PlayerRequest request)
        {
            var response = _repository.PlayerRepository.DeclineFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game")]
        public ActionResult<HttpResponseMessage> GameRequest([FromBody] PlayerRequest request)
        {
            var response = _repository.PlayerRepository.GameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game/accept")]
        public ActionResult<HttpResponseMessage> AcceptGameRequest([FromBody] PlayerRequest request)
        {
            var response = _repository.PlayerRepository.AcceptGameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game/decline")]
        public ActionResult<HttpResponseMessage> DeclineGameRequest([FromBody] PlayerRequest request)
        {
            var response = _repository.PlayerRepository.DeclineGameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("friend/delete")]
        public ActionResult<HttpResponseMessage> DeleteFriend([FromBody] PlayerRequest request)
        {
            var response = _repository.PlayerRepository.DeleteFriend(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("game/delete")]
        public ActionResult<HttpResponseMessage> DeleteGameInvites([FromBody] ID id)
        {
            var response = _repository.PlayerRepository.DeleteGameInvites(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] ID id)
        {
            var response = _repository.PlayerRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
