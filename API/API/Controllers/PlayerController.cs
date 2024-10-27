﻿using API.Data;
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
            bool respons = _repository.PlayerRepository.Create(new(player.ReceiverUsername, player.SenderToken));

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult<List<string>> OnlinePlayers()
        {
            var players = _repository.PlayerRepository.GetOnlinePlayers();

            if (players is null)
                return NotFound();

            return Ok(players);
        }

        [HttpGet("gaming")]
        public ActionResult<List<string>> PlayersInGame()
        {
            var players = _repository.PlayerRepository.GetPlayersInGame();

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

        [HttpGet("requests/friend/{token}")]
        public ActionResult<List<string>> PlayerFriendRequests(string token)
        {
            var pending = _repository.PlayerRepository.GetFriendRequests(token);

            if (pending is null)
                return NotFound();

            return Ok(pending);
        }

        [HttpGet("requests/game/{token}")]
        public ActionResult<List<string>> PlayerGameRequests(string token)
        {
            var pending = _repository.PlayerRepository.GetGameRequests(token);

            if (pending is null)
                return NotFound();

            return Ok(pending);
        }

        [HttpGet("sent/friend/{token}")]
        public ActionResult<List<string>> PlayerSentFriendRequests(string token)
        {
            var sent = _repository.PlayerRepository.GetSentFriendRequests(token);

            if (sent is null)
                return NotFound();

            return Ok(sent);
        }

        [HttpGet("sent/game/{token}")]
        public ActionResult<List<string>> GetSentGameRequests(string token)
        {
            var sent = _repository.PlayerRepository.GetSentGameRequests(token);

            if (sent is null)
                return NotFound();

            return Ok(sent);
        }

        [HttpPost("activity")]
        public ActionResult<HttpResponseMessage> Activity([FromBody] ID id)
        {
            var respons = _repository.PlayerRepository.UpdateActivity(id.Token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend")]
        public ActionResult<HttpResponseMessage> FriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.FriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend/accept")]
        public ActionResult<HttpResponseMessage> AcceptFriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.AcceptFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend/decline")]
        public ActionResult<HttpResponseMessage> DeclineFriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeclineFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game")]
        public ActionResult<HttpResponseMessage> GameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.GameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game/accept")]
        public ActionResult<HttpResponseMessage> AcceptGameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.AcceptGameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game/decline")]
        public ActionResult<HttpResponseMessage> DeclineGameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeclineGameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("friend/delete")]
        public ActionResult<HttpResponseMessage> DeleteFriend([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeleteFriend(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("game/delete")]
        public ActionResult<HttpResponseMessage> DeleteGameInvites([FromBody] ID id)
        {
            var respons = _repository.PlayerRepository.DeleteGameInvites(id.Token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] ID id)
        {
            var respons = _repository.PlayerRepository.Delete(id.Token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
