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

            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(parts[1], name);

            if (check == false)
            {
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.GetRematch(parts[0], parts[1]);

            return Ok(response);
        }

        [HttpPost("request/friend")]
        public async Task<ActionResult<HttpResponseMessage>> FriendRequest([FromBody] PlayerRequest request)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(request.SenderToken, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:Player/FriendRequest/Check", $"Failed to pass the check before player {request.SenderToken} sent a friend request to player {request.ReceiverUsername} within the player controller.")
                );
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.FriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "Player/FriendRequest", $"Player {request.SenderToken} sent a friend request to player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Player/FriendRequest", $"Player {request.SenderToken} failed to send a friend request to player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/friend/accept")]
        public async Task<ActionResult<HttpResponseMessage>> AcceptFriendRequest([FromBody] PlayerRequest request)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(request.SenderToken, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:Player/AcceptFriend/Check", $"Failed to pass the check before player {request.SenderToken} accepted the friend request from player {request.ReceiverUsername} within the player controller.")
                );
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.AcceptFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "Player/AcceptFriendRequest", $"Player {request.SenderToken} accepted the a friend request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Player/AcceptFriendRequest", $"Player {request.SenderToken} failed to accept the a friend request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/friend/decline")]
        public async Task<ActionResult<HttpResponseMessage>> DeclineFriendRequest([FromBody] PlayerRequest request)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(request.SenderToken, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:Player/DeclineFriendRequest/Check", $"Failed to pass the check before player {request.SenderToken} declined the friend request from player {request.ReceiverUsername} within the player controller.")
                );
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.DeclineFriendRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "Player/DeclineFriendRequest", $"Player {request.SenderToken} declined the a friend request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Player/DeclineFriendRequest", $"Player {request.SenderToken} failed to decline the a friend request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/game")]
        public async Task<ActionResult<HttpResponseMessage>> GameRequest([FromBody] PlayerRequest request)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(request.SenderToken, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:Player/GameRequest/Check", $"Failed to pass the check before player {request.SenderToken} sent a game request to player {request.ReceiverUsername} within the player controller.")
                );
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.GameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "Player/GameRequest", $"Player {request.SenderToken} sent a game request to player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Player/GameRequest", $"Player {request.SenderToken} failed to send a game request to player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/game/accept")]
        public async Task<ActionResult<HttpResponseMessage>> AcceptGameRequest([FromBody] PlayerRequest request)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(request.SenderToken, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:Player/AcceptGameRequest/Check", $"Failed to pass the check before player {request.SenderToken} accepted the game request from player {request.ReceiverUsername} within the player controller.")
                );
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.AcceptGameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "Player/AcceptGameRequest", $"Player {request.SenderToken} accepted the game request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Player/AcceptGameRequest", $"Player {request.SenderToken} failed to accept the game request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("request/game/decline")]
        public async Task<ActionResult<HttpResponseMessage>> DeclineGameRequest([FromBody] PlayerRequest request)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(request.SenderToken, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:Player/DeclineGameRequest/Check", $"Failed to pass the check before player {request.SenderToken} declined the game request from player {request.ReceiverUsername} within the player controller.")
                );
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.DeclineGameRequest(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "Player/DeclineGameRequest", $"Player {request.SenderToken} declined the game request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Player/DeclineGameRequest", $"Player {request.SenderToken} failed to decline the game request of player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("friend/delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteFriend([FromBody] PlayerRequest request)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(request.SenderToken, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:Player/DeleteFriend/Check", $"Failed to pass the check before player {request.SenderToken} deleted the friendship with player {request.ReceiverUsername} within the player controller.")
                );
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.DeleteFriend(request.ReceiverUsername, request.SenderToken);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(name, "Player/DeleteFriend", $"Player {request.SenderToken} deleted the friendship with player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(name, "FAIL:Player/DeleteFriend", $"Player {request.SenderToken} failed to delete the friendship with player {request.ReceiverUsername} within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<HttpResponseMessage>> Delete([FromBody] ID id)
        {
            var name = User?.Identity?.Name ?? "Anonymous";

            var check = await _repository.PlayerRepository.PlayerChecksOut(id.Token, name);

            if (check == false)
            {
                await _repository.LogRepository.Create(
                new(name, "FAIL:Player/Delete/Check", $"Failed to pass the check before player {id.Token} deleted the account within the player controller.")
                );
                return BadRequest();
            }

            var response = await _repository.PlayerRepository.Delete(id.Token);

            if (response == true)
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "Player/Delete", $"Player {id.Token} deleted the account within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                await _repository.LogRepository.Create(
                    new(User?.Identity?.Name ?? "Anonymous", "FAIL:Player/Delete", $"Player {id.Token} failed to delete the account within the player controller.")
                );
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
