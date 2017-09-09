using Microsoft.AspNetCore.Mvc;
using ygo.application.Commands.UpdateTips;

namespace ygo.api.Controllers
{
    [Route("cards/{cardId}/[controller]")]
    public class TipsController : Controller
    {
        [HttpGet]
        public IActionResult Get(int cardId)
        {
            return StatusCode(501);
        }

        [HttpPut]
        public IActionResult PutTips([FromBody] UpdateTipsCommand command)
        {
            return StatusCode(501);
        }

    }
}