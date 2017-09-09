using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;
using ygo.application.Commands.UpdateTrivias;

namespace ygo.api.Controllers
{
    [Route("cards/{cardId}/api/[controller]")]
    public class TriviasController : Controller
    {
        [HttpGet]
        public IActionResult Get(long cardId)
        {
            return StatusCode(501);
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        public IActionResult PutTrivia([FromBody] UpdateTriviasCommand command)
        {
            return StatusCode(501);
        }

    }
}