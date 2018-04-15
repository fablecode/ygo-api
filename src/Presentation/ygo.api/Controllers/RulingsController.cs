using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;
using ygo.application.Commands.UpdateRulings;

namespace ygo.api.Controllers
{
    [Route("cards/{cardId}/api/[controller]")]
    public class RulingsController : Controller
    {
        [HttpGet]
        public IActionResult Get(long cardId)
        {
            return StatusCode(501);
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        public IActionResult PutRulings([FromBody] UpdateRulingsCommand command)
        {
            return StatusCode(501);
        }
    }
}