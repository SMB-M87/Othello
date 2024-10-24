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

        [HttpPut("activity")]
        public ActionResult<HttpResponseMessage> Activity([FromBody] string token)
        {
            var respons = _repository.PlayerRepository.UpdateActivity(token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("request/friend")]
        public ActionResult<HttpResponseMessage> FriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.FriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("request/friend/accept")]
        public ActionResult<HttpResponseMessage> AcceptFriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.AcceptFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("request/friend/decline")]
        public ActionResult<HttpResponseMessage> DeclineFriendRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeclineFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("request/game")]
        public ActionResult<HttpResponseMessage> GameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.GameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("request/game/accept")]
        public ActionResult<HttpResponseMessage> AcceptGameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.AcceptGameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("request/game/decline")]
        public ActionResult<HttpResponseMessage> DeclineGameRequest([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeclineGameRequest(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("friend/delete")]
        public ActionResult<HttpResponseMessage> DeleteFriend([FromBody] PlayerRequest request)
        {
            var respons = _repository.PlayerRepository.DeleteFriend(request.ReceiverUsername, request.SenderToken);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPut("game/delete")]
        public ActionResult<HttpResponseMessage> DeleteGameInvites([FromBody] string token)
        {
            var respons = _repository.PlayerRepository.DeleteGameInvites(token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpDelete("delete")]
        public ActionResult<HttpResponseMessage> Delete([FromBody] string token)
        {
            var respons = _repository.PlayerRepository.Delete(token);

            if (respons == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
