using Microsoft.AspNetCore.Mvc;
using ygo.application.Commands.UpdateTrivias;

namespace ygo.api.Controllers
{
    [Route("cards/{cardId}/[controller]")]
    public class TriviasController : Controller
    {
        [HttpGet]
        public IActionResult Get(long cardId)
        {
            return StatusCode(501);
        }

        [HttpPut]
        public IActionResult PutTrivia([FromBody] UpdateTriviasCommand command)
        {
            return StatusCode(501);
        }

    }
}