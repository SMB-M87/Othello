using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/check")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        public CheckController() {}

        [HttpGet()]
        public ActionResult<string> Hello()
        {
            return Ok("Okidoki");
        }
    }
}
