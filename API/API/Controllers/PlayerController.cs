using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.User)]
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IRepository _repository;

        public PlayerController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("rematch/{token}")]
        public async Task<ActionResult<string?>> Rematch(string token)
        {
            string[] parts = token.Split(' ');
            var response = await _repository.PlayerRepository.GetRematch(parts[0], parts[1]);

            await _repository.LogRepository.Create(
                new(User?.Identity?.Name ?? "Anonymous", "Player/Rematch", $"Player {parts[1]} {(response == null ? "didn't found" : "did found")} a rematch with player {parts[0]} within the player controller.")
            );

            return Ok(response);
        }

        [HttpPost("activity")]
        public async Task<ActionResult<HttpResponseMessage>> Activity([FromBody] ID id)
        {
            var response = await _repository.PlayerRepository.UpdateActivity(id.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/Activity", $"Updated last activity from player {id.Token} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/Activity", $"Failed to update last activity from player {id.Token} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/friend")]
        public async Task<ActionResult<HttpResponseMessage>> FriendRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.FriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/FriendRequest", $"Player {request.SenderToken} sent a friend request to player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/FriendRequest", $"Player {request.SenderToken} failed to send a friend request to player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/friend/accept")]
        public async Task<ActionResult<HttpResponseMessage>> AcceptFriendRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.AcceptFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/AcceptFriendRequest", $"Player {request.SenderToken} accepted the a friend request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/AcceptFriendRequest", $"Player {request.SenderToken} failed to accept the a friend request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/friend/decline")]
        public async Task<ActionResult<HttpResponseMessage>> DeclineFriendRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.DeclineFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/DeclineFriendRequest", $"Player {request.SenderToken} declined the a friend request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/DeclineFriendRequest", $"Player {request.SenderToken} failed to decline the a friend request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/game")]
        public async Task<ActionResult<HttpResponseMessage>> GameRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.GameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/GameRequest", $"Player {request.SenderToken} sent a game request to player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/GameRequest", $"Player {request.SenderToken} failed to send a game request to player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/game/accept")]
        public async Task<ActionResult<HttpResponseMessage>> AcceptGameRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.AcceptGameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/AcceptGameRequest", $"Player {request.SenderToken} accepted the game request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/AcceptGameRequest", $"Player {request.SenderToken} failed to accept the game request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/game/decline")]
        public async Task<ActionResult<HttpResponseMessage>> DeclineGameRequest([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.DeclineGameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/DeclineGameRequest", $"Player {request.SenderToken} declined the game request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/DeclineGameRequest", $"Player {request.SenderToken} failed to decline the game request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("friend/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteFriend([FromBody] PlayerRequest request)
        {
            var response = await _repository.PlayerRepository.DeleteFriend(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/DeleteFriend", $"Player {request.SenderToken} deleted the friendship with player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/DeleteFriend", $"Player {request.SenderToken} failed to delete the friendship with player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<HttpResponseMessage>> Delete([FromBody] ID id)
        {
            var response = await _repository.PlayerRepository.Delete(id.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/DeleteFriend", $"Player {id.Token} deleted the account within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/DeleteFriend", $"Player {id.Token} failed to delete the account within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
