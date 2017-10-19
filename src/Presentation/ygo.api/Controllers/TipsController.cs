using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;
using ygo.application.Commands.UpdateTips;

namespace ygo.api.Controllers
{
    [Route("api/cards/{cardId}/[controller]")]
    public class TipsController : Controller
    {
        [HttpGet]
        public IActionResult Get(int cardId)
        {
            return StatusCode(501);
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        public IActionResult PutTips([FromBody] UpdateTipsCommand command)
        {
            return StatusCode(501);
        }

    }
}