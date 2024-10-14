﻿using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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
            var name = _repository.PlayerRepository.GetByUsername(username);

            if (name is not null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            Player player = new(username);
            _repository.PlayerRepository.Create(player);
            var respons = _repository.PlayerRepository.Get(player.Token);

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

        [HttpGet("{token}")]
        public ActionResult<Player>? PlayerByToken(string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is not null)
                return player;
            else
                return null;
        }

        [HttpGet("{username}/username")]
        public ActionResult<Player>? PlayerByUsername(string username)
        {
            var player = _repository.PlayerRepository.GetByUsername(username);

            if (player is not null)
                return player;
            else
                return null;
        }

        [HttpGet("{token}/name")]
        public ActionResult<string>? PlayersName(string token)
        {
            var player = _repository.PlayerRepository.Get(token);

            if (player is not null)
                return player.Username;
            else
                return null;
        }

        [HttpGet("{token}/friends")]
        public ActionResult<List<string>?>? PlayerFriends(string token)
        {
            return _repository.PlayerRepository.GetFriends(token);
        }

        [HttpGet("{token}/pending")]
        public ActionResult<List<string>?>? PlayerPending(string token)
        {
            return _repository.PlayerRepository.GetPending(token);
        }

        [HttpPost("send")]
        public ActionResult<HttpResponseMessage> Send([FromBody] PlayerRequest request)
        {
            var receiver = _repository.PlayerRepository.GetByUsername(request.Receiver);
            var sender = _repository.PlayerRepository.GetByUsername(request.Sender);

            if (receiver is null || sender is null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            if (receiver.Friends.Contains(request.Sender) || receiver.PendingFriends.Contains(request.Sender))
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            _repository.PlayerRepository.SendFriendInvite(request.Receiver, request.Sender);
            _repository.PlayerRepository.Update(receiver);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost("accept")]
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

        [HttpPost("decline")]
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

        [HttpPost("delete")]
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