using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;
using ygo.application.Commands.UpdateCardImage;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        [HttpGet("{id}/image")]
        public IActionResult GetImage()
        {
            return StatusCode(501);
        }

        [HttpPost("{id}/image")]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        public IActionResult PostImage([FromBody] UpdateCardImageCommand command)
        {
            return StatusCode(501);
        }


        [HttpPut("{id}/image")]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        public IActionResult PutImage([FromBody] UpdateCardImageCommand command)
        {
            return StatusCode(501);
        }
    }
}