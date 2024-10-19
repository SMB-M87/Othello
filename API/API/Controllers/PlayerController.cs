using API.Data;
using API.Models;
using System.Data;
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
            bool exists = _repository.PlayerRepository.Exists(player.Player);

            if (exists == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            Player create = new(player.Token, player.Player);
            _repository.PlayerRepository.Create(create);
            var respons = _repository.PlayerRepository.Get(create.Token);

            if (respons is not null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            _repository.PlayerRepository.Delete(player);
            var respons = _repository.PlayerRepository.Get(token);

            if (respons is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost("login")]
        public ActionResult<HttpResponseMessage> Login([FromBody] string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            player.IsOnline = true;
            _repository.PlayerRepository.Update(player);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost("logout")]
        public ActionResult<HttpResponseMessage> Logout([FromBody] string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            player.IsOnline = false;
            _repository.PlayerRepository.Update(player);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult<List<string>> OnlinePlayers()
        {
            var players = _repository.PlayerRepository.GetPlayers();

            if (players is null)
                return NotFound();

            var results = players.FindAll(player => player.IsOnline == true).Select(player => player.Username).ToList();
            return Ok(results);
        }

        [HttpGet("invitable")]
        public ActionResult<List<string>> InvitablePlayers()
        {
            var players = _repository.PlayerRepository.GetPlayers();

            if (players is null)
                return NotFound();

            var results = players.FindAll(player => player.IsOnline == true && _repository.GameRepository.GetPlayersGame(player.Token) is null).Select(player => player.Username).ToList();
            return Ok(results);
        }

        [HttpGet("{token}")]
        public ActionResult<Player> PlayerByToken(string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is null)
                return NotFound();

            return Ok(player);
        }

        [HttpGet("username/{username}")]
        public ActionResult<Player> PlayerByUsername(string username)
        {
            var player = _repository.PlayerRepository.GetByUsername(username);

            if (player is null)
                return NotFound();

            return Ok(player);
        }

        [HttpGet("name/{token}")]
        public ActionResult<string> PlayersName(string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is null)
                return NotFound();

            return Ok(player.Username);
        }

        [HttpGet("token/{username}")]
        public ActionResult<string> PlayersToken(string username)
        {
            var player = _repository.PlayerRepository.Get(username);

            if (player is null)
                return NotFound();

            return Ok(player.Token);
        }

        [HttpGet("check/{username}")]
        public ActionResult<bool> PlayerExists(string username)
        {
            bool player = _repository.PlayerRepository.Exists(username);

            if (!player)
                return NotFound();

            return Ok(player);
        }

        [HttpGet("friends/{token}")]
        public ActionResult<List<string>> PlayerFriends(string token)
        {
            var friends = _repository.PlayerRepository.GetFriends(token);

            if (friends is null)
                return NotFound();

            return Ok(friends);
        }

        [HttpGet("pending/{token}")]
        public ActionResult<List<string>> PlayerPending(string token)
        {
            var pending = _repository.PlayerRepository.GetPending(token);

            if (pending is null)
                return NotFound();

            return Ok(pending);
        }

        [HttpGet("sent/{username}")]
        public ActionResult<List<string>> PlayerSent(string username)
        {
            var sent = _repository.PlayerRepository.GetPlayers();

            if (sent is null)
                return NotFound();

            return Ok(sent.FindAll(p => p.PendingFriends.Contains(username)).Select(p => p.Username).ToList());
        }

        [HttpPost("friend/send")]
        public ActionResult<HttpResponseMessage> Send([FromBody] PlayerRequest request)
        {
            var receiver = _repository.PlayerRepository.GetByUsername(request.Receiver);
            var sender = _repository.PlayerRepository.GetByUsername(request.Sender);

            if (receiver is null || sender is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (receiver.Friends.Contains(request.Sender) || sender.Friends.Contains(request.Receiver) || 
                receiver.PendingFriends.Contains(request.Sender) || sender.PendingFriends.Contains(request.Receiver))
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            _repository.PlayerRepository.SendFriendInvite(request.Receiver, request.Sender);
            _repository.PlayerRepository.Update(receiver);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost("friend/accept")]
        public ActionResult<HttpResponseMessage> Accept([FromBody] PlayerRequest request)
        {
            var receiver = _repository.PlayerRepository.GetByUsername(request.Receiver);
            var sender = _repository.PlayerRepository.GetByUsername(request.Sender);

            if (receiver is null || sender is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (receiver.Friends.Contains(request.Sender) || !sender.PendingFriends.Contains(request.Receiver))
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            _repository.PlayerRepository.AcceptFriendInvite(request.Receiver, request.Sender);
            _repository.PlayerRepository.Update(receiver);
            _repository.PlayerRepository.Update(sender);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost("friend/decline")]
        public ActionResult<HttpResponseMessage> Decline([FromBody] PlayerRequest request)
        {
            var receiver = _repository.PlayerRepository.GetByUsername(request.Receiver);
            var sender = _repository.PlayerRepository.GetByUsername(request.Sender);

            if (receiver is null || sender is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (!sender.PendingFriends.Contains(request.Receiver))
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            _repository.PlayerRepository.DeclineFriendInvite(request.Receiver, request.Sender);
            _repository.PlayerRepository.Update(sender);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost("friend/delete")]
        public ActionResult<HttpResponseMessage> DeleteFriend([FromBody] PlayerRequest request)
        {
            var receiver = _repository.PlayerRepository.GetByUsername(request.Receiver);
            var sender = _repository.PlayerRepository.GetByUsername(request.Sender);

            if (receiver is null || sender is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (!receiver.Friends.Contains(request.Sender) || !sender.Friends.Contains(request.Receiver))
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            _repository.PlayerRepository.DeleteFriend(request.Receiver, request.Sender);
            _repository.PlayerRepository.Update(sender);
            _repository.PlayerRepository.Update(receiver);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
