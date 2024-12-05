using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiKeyAuthorize]
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IRepository _repository;

        public LogController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost()]
        public async Task Log([FromBody] PlayerLog log)
        {
            await _repository.LogRepository.Create(log);
        }
    }
}
