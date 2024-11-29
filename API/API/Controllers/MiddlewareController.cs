using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/middleware")]
    [ApiController]
    public class MiddlewareController : ControllerBase
    {
        private readonly IRepository _repository;

        public MiddlewareController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<ActionResult<List<string>>> Inactive()
        {
            var response = await _repository.PlayerRepository.GetInactivePlayers();

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
