using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("view/{token}")]
        public ActionResult<Home> View(string token)
        {
            _repository.PlayerRepository.UpdateActivity(token);
            _repository.PlayerRepository.DeleteGameInvites(token);
            var response = _repository.HomeRepository.GetView(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("partial/{token}")]
        public ActionResult<HomePartial> Partial(string token)
        {
            _repository.PlayerRepository.DeleteGameInvites(token);
            var response = _repository.HomeRepository.GetPartial(token);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("profile/{token}")]
        public ActionResult<HomeProfile> Profile(string token)
        {
            string[] parts = token.Split(' ');
            _repository.PlayerRepository.UpdateActivity(parts[1]);
            var response = _repository.HomeRepository.GetProfile(parts[0], parts[1]);

            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
