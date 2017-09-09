using Microsoft.AspNetCore.Mvc;
using ygo.application.Commands.UpdateRulings;

namespace ygo.api.Controllers
{
    [Route("cards/{cardId}/[controller]")]
    public class RulingsController : Controller
    {
        [HttpGet]
        public IActionResult Get(long cardId)
        {
            return StatusCode(501);
        }

        [HttpPut]
        public IActionResult PutRulings([FromBody] UpdateRulingsCommand command)
        {
            return StatusCode(501);
        }

    }
}