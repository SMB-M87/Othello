using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{username}")]
        public async Task<ActionResult<bool>> CheckUsername(string username)
        {
            bool response = await _repository.PlayerRepository.UsernameExists(username);

            if (response == false)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("rematch/{token}")]
        public async Task<ActionResult<string?>> Rematch(string token)
        {
            string[] parts = token.Split(' ');
            var response = await _repository.PlayerRepository.GetRematch(parts[0], parts[1]);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<HttpResponseMessage>> Create([FromBody] PlayerRequest player)
        {
            bool response = await _repository.PlayerRepository.Create(new(player.ReceiverUsername, player.SenderToken));

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("activity")]
        public async Task<ActionResult<HttpResponseMessage>> Activity([FromBody] ID id)
        {
            var response = await _repository.PlayerRepository.UpdateActivity(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend")]
        public async Task<ActionResult<HttpResponseMessage>> FriendRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.FriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend/accept")]
        public async Task<ActionResult<HttpResponseMessage>> AcceptFriendRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.AcceptFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/friend/decline")]
        public async Task<ActionResult<HttpResponseMessage>> DeclineFriendRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.DeclineFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game")]
        public async Task<ActionResult<HttpResponseMessage>> GameRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.GameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game/accept")]
        public async Task<ActionResult<HttpResponseMessage>> AcceptGameRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.AcceptGameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("request/game/decline")]
        public async Task<ActionResult<HttpResponseMessage>> DeclineGameRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.DeclineGameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("friend/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteFriend([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.DeleteFriend(request.ReceiverUsername, request.SenderToken);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<HttpResponseMessage>> Delete([FromBody] ID id)
        {
            var response = await _repository.PlayerRepository.Delete(id.Token);

            if (response == true)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
